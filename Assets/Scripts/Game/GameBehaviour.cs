using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine;

public class GameBehaviour : MonoBehaviour {
	public int mc;
	public int gold;

	public TowerBuild tb_script;
	public MonsterGenerator mg_script;

	public Text mc_text;
	public Text prep_text;
	public Text gold_text;
	public Text error_text;
	public Image menu;
	public Button menu_exit;

	private bool game_started;
	private bool menu_active;

	// Use this for initialization
	void Start () {
		menu_exit.onClick.AddListener (MenuExit);
	}
	
	// Update is called once per frame
	void Update () {

		/* Game Start on Key.Enter */
		if (Input.GetKeyDown (KeyCode.Return) && !game_started) {
			game_started = true;
			mg_script.SpawnWave (mc);
			GameObject.Destroy (prep_text);
		}

		/* Game Menu on Key.Escape */
		if (!tb_script.building && Input.GetKeyDown (KeyCode.Escape)) {
			if (!menu_active) {
				/* Freeze Game */
				menu_active = true;
				Time.timeScale = 0.0f;
				menu.gameObject.SetActive (true);
				// Menu activate
			} else {
				menu_active = false;
				Time.timeScale = 1.0f;
				menu.gameObject.SetActive (false);
				// Menu deactivate
			}
		}

		/* Update text boxes */
		mc_text.text = mg_script.monster_count.ToString () + " / " + mc.ToString ();
		gold_text.text = gold.ToString ();
	}

	public void SendError(string msg) {
		error_text.gameObject.SetActive (true);
		error_text.text = msg;
		StartCoroutine ("DisableError");
	}

	IEnumerator DisableError() {
		yield return new WaitForSeconds (5.0f);
		error_text.gameObject.SetActive (false);
	}

	void MenuExit() {
		Application.Quit ();
	}
}
