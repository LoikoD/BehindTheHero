using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Audio;
using Unity.Collections.LowLevel.Unsafe;

public class SoundSlider : MonoBehaviour
{
    [SerializeField] private AudioMixerGroup _mixerGroup;
    [SerializeField] private Slider _music;
    [SerializeField] private Slider _volume;
    [SerializeField] private Slider _ambient;

    private void Start()
    {

        _music.onValueChanged.AddListener(SliderMusicChange);
        _volume.onValueChanged.AddListener(SliderVolumeChange);
        _ambient.onValueChanged.AddListener(SliderAmbientChange);
        SliderMusicChange(_music.value);
        SliderVolumeChange(_volume.value);
        SliderAmbientChange(_ambient.value);

    }
    public void SliderMusicChange(float newValue)
    {
        ChangeVolume("Music", newValue);
    }
    public void SliderVolumeChange(float newValue)
    {
        ChangeVolume("Effects", newValue);
    }
    public void SliderAmbientChange(float newValue)
    {
        ChangeVolume("Ambient", newValue);
    }

    private void ChangeVolume(string mixerGroupName, float linearValue)
    {
        _mixerGroup.audioMixer.SetFloat(mixerGroupName, Mathf.Log10(linearValue) * 20);
    }
}
