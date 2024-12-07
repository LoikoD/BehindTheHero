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
    [SerializeField] private Slider _effects;
    [SerializeField] private Slider _ambient;

    private void Start()
    {

        _music.onValueChanged.AddListener(SliderMusicChange);
        _effects.onValueChanged.AddListener(SliderVolumeChange);
        _ambient.onValueChanged.AddListener(SliderAmbientChange);

        _music.value = GetVolume("Music");
        _effects.value = GetVolume("Effects");
        _ambient.value = GetVolume("Ambient");
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

    private float GetVolume(string mixerGroupName)
    {
        _mixerGroup.audioMixer.GetFloat(mixerGroupName, out float value);
        return Mathf.Pow(10, value / 20);
    }
}
