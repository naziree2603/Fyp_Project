using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Audio;

public class AudioManager : MonoBehaviour
{
    [SerializeField] Slider soundSlider;
    [SerializeField] AudioMixer masterMixter;
    private int Length;

    void Awake()
    {
        if (FindFirstObjectByType<AudioManager>().Length > 1)
        {
            Destroy(gameObject);
            return;
        }

        DontDestroyOnLoad(gameObject);
    }

    private void Start()
    {
        SetVolume(PlayerPrefs.GetFloat("SavedMasterVolume", 100f));
    }

    public void SetVolume(float _value)
    {
        if(_value < 1)
        {
            _value = 0.0001f;
        }

        RefreshSlider(_value);
        PlayerPrefs.SetFloat("SavedMasterVolume", _value);
        masterMixter.SetFloat("MasterVolume", Mathf.Log10(_value / 100 ) * 20f);
    }

    public void SetVolumeFromSlider()
    {
        SetVolume(soundSlider.value);
    }

    public void RefreshSlider(float _value)
    {
        soundSlider.value = _value; 
    }
}