using UnityEngine;
using UnityEngine.UI;

public class SliderController : MonoBehaviour
{
    [SerializeField] private Slider masterSlider;
    [SerializeField] private Slider sfxSlider;
    [SerializeField] private Slider musicSlider;

    private void Start()
    {
        masterSlider.value = AudioManagerSingleton.Instance.GetMasterVolume();
        sfxSlider.value = AudioManagerSingleton.Instance.GetSFXVolume();
        musicSlider.value = AudioManagerSingleton.Instance.GetMusicVolume();

        masterSlider.onValueChanged.AddListener(OnMasterVolumeChange);
        sfxSlider.onValueChanged.AddListener(OnSFXVolumeChange);
        musicSlider.onValueChanged.AddListener(OnMusicVolumeChange);
    }

    private void OnMasterVolumeChange(float value)
    {
        AudioManagerSingleton.Instance.UpdateMasterVolume(value);
    }

    private void OnSFXVolumeChange(float value)
    {
        AudioManagerSingleton.Instance.UpdateSFXVolume(value);
    }

    private void OnMusicVolumeChange(float value)
    {
        AudioManagerSingleton.Instance.UpdateMusicVolume(value);
    }
}
