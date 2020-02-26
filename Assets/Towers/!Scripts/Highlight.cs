using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

[DisallowMultipleComponent]
public class Highlight : MonoBehaviour {
    private static HashSet<Mesh> _registeredMeshes = new HashSet<Mesh>();

    [Serializable]
    private class ListVector3 {
        public List<Vector3> data = new List<Vector3>();
    }
    
    [SerializeField] private Color outlineColor = Color.white;

    [SerializeField, Range(0f, 10f)] private float outlineWidth = 2f;

    [SerializeField, HideInInspector] private List<Mesh> bakeKeys = new List<Mesh>();

    [SerializeField, HideInInspector] private List<ListVector3> bakeValues = new List<ListVector3>();

    private Renderer[] _renderers;
    private Material _outlineMaskMaterial;
    private Material _outlineFillMaterial;
    private bool _needsUpdate;

    private void Awake() {
        // Cache renderers
        _renderers = GetComponentsInChildren<Renderer>();

        // Instantiate outline materials
        _outlineMaskMaterial = Instantiate(Resources.Load<Material>(@"Materials/OutlineMask"));
        _outlineFillMaterial = Instantiate(Resources.Load<Material>(@"Materials/OutlineFill"));

        _outlineMaskMaterial.name = "OutlineMask (Instance)";
        _outlineFillMaterial.name = "OutlineFill (Instance)";

        // Retrieve or generate smooth normals
        LoadSmoothNormals();

        // Apply material properties immediately
        _needsUpdate = true;
    }

    private void OnEnable() {
        foreach (Renderer r in _renderers) {
            if (r.gameObject.name.Equals("Aura")) continue;
            
            // Append outline shaders
            var materials = r.sharedMaterials.ToList();

            materials.Add(_outlineMaskMaterial);
            materials.Add(_outlineFillMaterial);

            r.materials = materials.ToArray();
        }
    }

    private void OnValidate() {
        // Update material properties
        _needsUpdate = true;

        // Clear cache when baking is disabled or corrupted
        if (bakeKeys.Count != 0 || bakeKeys.Count != bakeValues.Count) {
            bakeKeys.Clear();
            bakeValues.Clear();
        }
    }

    private void Update() {
        if (_needsUpdate) {
            _needsUpdate = false;

            UpdateMaterialProperties();
        }
    }

    private void OnDisable() {
        foreach (Renderer r in _renderers) {
            // Remove outline shaders
            var materials = r.sharedMaterials.ToList();

            materials.Remove(_outlineMaskMaterial);
            materials.Remove(_outlineFillMaterial);

            r.materials = materials.ToArray();
        }
    }

    private void OnDestroy() {
        // Destroy material instances
        Destroy(_outlineMaskMaterial);
        Destroy(_outlineFillMaterial);
    }

    void LoadSmoothNormals() {
        // Retrieve or generate smooth normals
        foreach (var meshFilter in GetComponentsInChildren<MeshFilter>()) {
            // Skip if smooth normals have already been adopted
            if (!_registeredMeshes.Add(meshFilter.sharedMesh)) {
                continue;
            }

            // Retrieve or generate smooth normals
            var index = bakeKeys.IndexOf(meshFilter.sharedMesh);
            var smoothNormals = (index >= 0) ? bakeValues[index].data : SmoothNormals(meshFilter.sharedMesh);

            // Store smooth normals in UV3
            meshFilter.sharedMesh.SetUVs(3, smoothNormals);
        }

        // Clear UV3 on skinned mesh renderers
        foreach (SkinnedMeshRenderer skinnedMeshRenderer in GetComponentsInChildren<SkinnedMeshRenderer>()) {
            if (_registeredMeshes.Add(skinnedMeshRenderer.sharedMesh)) {
                skinnedMeshRenderer.sharedMesh.uv4 = new Vector2[skinnedMeshRenderer.sharedMesh.vertexCount];
            }
        }
    }

    List<Vector3> SmoothNormals(Mesh mesh) {
        // Group vertices by location
        var groups = mesh.vertices.Select((vertex, index) => new KeyValuePair<Vector3, int>(vertex, index))
            .GroupBy(pair => pair.Key);

        // Copy normals to a new list
        var smoothNormals = new List<Vector3>(mesh.normals);

        // Average normals for grouped vertices
        foreach (var group in groups) {
            // Skip single vertices
            if (group.Count() == 1) {
                continue;
            }

            // Calculate the average normal
            var smoothNormal = Vector3.zero;

            foreach (var pair in group) {
                smoothNormal += mesh.normals[pair.Value];
            }

            smoothNormal.Normalize();

            // Assign smooth normal to each vertex
            foreach (var pair in group) {
                smoothNormals[pair.Value] = smoothNormal;
            }
        }

        return smoothNormals;
    }

    private void UpdateMaterialProperties() {
        // Apply properties according to mode
        _outlineFillMaterial.SetColor("_OutlineColor", outlineColor);
        _outlineMaskMaterial.SetFloat("_ZTest", (float) UnityEngine.Rendering.CompareFunction.Always);
        _outlineFillMaterial.SetFloat("_ZTest", (float) UnityEngine.Rendering.CompareFunction.LessEqual);
        _outlineFillMaterial.SetFloat("_OutlineWidth", outlineWidth);
    }
}