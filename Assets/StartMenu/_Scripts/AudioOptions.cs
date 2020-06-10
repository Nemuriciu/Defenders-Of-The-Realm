using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.UI;

public class AudioOptions : MonoBehaviour {
    public Slider master;
    public Slider sound;
    public Slider music;
    [Space(10)] 
    public AudioMixer masterMixer;

    private float _masterVol, _soundVol, _musicVol;

    private void Start() {
         masterMixer.GetFloat("masterVolume", out _masterVol);
         masterMixer.GetFloat("soundVolume", out _soundVol);
         masterMixer.GetFloat("musicVolume", out _musicVol);

         master.value = _masterVol;
         sound.value = _soundVol;
         music.value = _musicVol;
         
         master.onValueChanged.AddListener(delegate {SetMasterVolume(master.value); });
         sound.onValueChanged.AddListener(delegate {SetSoundVolume(sound.value); });
         music.onValueChanged.AddListener(delegate {SetMusicVolume(music.value); });
    }

    private void SetMasterVolume(float value) {
        if (value <= -40)
            masterMixer.SetFloat("masterVolume", -80);
        else
            masterMixer.SetFloat("masterVolume", value);
    }
    
    private void SetSoundVolume(float value) {
        if (value <= -40)
            masterMixer.SetFloat("soundVolume", -80);
        else
            masterMixer.SetFloat("soundVolume", value);
    }
    
    private void SetMusicVolume(float value) {
        if (value <= -40)
            masterMixer.SetFloat("musicVolume", -80);
        else
            masterMixer.SetFloat("musicVolume", value);
    }
}
