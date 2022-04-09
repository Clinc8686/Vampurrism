using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Audio;

public class SoundManager : MonoBehaviour
{
    [SerializeField] private Slider musicSlider;
    [SerializeField] private Slider soundSlider;
    [SerializeField] private Slider masterSlider;
    [SerializeField] private Text volumeText;
    [SerializeField] private Text soundText;
    [SerializeField] private Text masterText;
    [SerializeField] private AudioMixer audioMixer;

    // Start is called before the first frame update
    void Start()
    {
        if (!PlayerPrefs.HasKey("soundVolume"))
        {
            PlayerPrefs.SetFloat("soundVolume", 1);
            LoadSound();
        }
        else
        {
            LoadSound();
        }
        if (!PlayerPrefs.HasKey("masterVolume"))
        {
            PlayerPrefs.SetFloat("masterVolume", 1);
            LoadMaster();
        }
        else
        {
            LoadMaster();
        }
        if (!PlayerPrefs.HasKey("musicVolume"))
        {
            PlayerPrefs.SetFloat("musicVolume", 1);
            LoadMusic();
        }
        else
        {
            LoadMusic();
        }
    }

    public void SetMasterVolume()
    {
        audioMixer.SetFloat("masterVolume", masterSlider.value - 80);
        masterText.text = masterSlider.value.ToString();
        PlayerPrefs.SetFloat("masterVolume", masterSlider.value);
    }

    public void SetMusicVolume()
    {
        audioMixer.SetFloat("musicVolume", musicSlider.value-80);
        volumeText.text = musicSlider.value.ToString();
        PlayerPrefs.SetFloat("musicVolume", musicSlider.value);
    }

    public void SetSoundVolume()
    {
        audioMixer.SetFloat("soundVolume", soundSlider.value - 80);
        soundText.text = soundSlider.value.ToString();
        PlayerPrefs.SetFloat("soundVolume", soundSlider.value);
    }

    private void LoadMaster()
    {
        masterSlider.value = PlayerPrefs.GetFloat("masterVolume");
        masterText.text = masterSlider.value.ToString();
        audioMixer.SetFloat("masterVolume", masterSlider.value - 80);
    }

    private void LoadMusic()
    {
        musicSlider.value = PlayerPrefs.GetFloat("musicVolume");
        volumeText.text = musicSlider.value.ToString();
        audioMixer.SetFloat("musicVolume", musicSlider.value - 80);
    }

    private void LoadSound()
    {
        soundSlider.value = PlayerPrefs.GetFloat("soundVolume");
        soundText.text = soundSlider.value.ToString();
        audioMixer.SetFloat("soundVolume", soundSlider.value - 80);
    }
}
