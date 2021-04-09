using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.SocialPlatforms.Impl;
using JetBrains.Annotations;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using UnityEngine.Audio;

public class GameManager : MonoBehaviour
{

    public int money;
    public float musicVol;
    public float soundVol;

    public TextMeshProUGUI scoreText;
    public GameObject SettingsWin;
    public SellingController sellingController;

    public Button soundOn;
    public Button soundOff;
    public Button musicOn;
    public Button musicOff;

    private AudioSource playAudio;
    public AudioSource playMusic;

    //Sound FX
    public AudioClip clickSound; // звук нажатия на кнопки.

    public bool gameIsPaused;

    void Start()
    {
        sellingController = GameObject.Find("CashDesk").GetComponent<SellingController>();
        playAudio = GetComponent<AudioSource>();
        playMusic = GameObject.Find("Main Camera").GetComponent<AudioSource>();

        money = PlayerPrefs.GetInt("MoneyScore", 0);
        scoreText.text = "$ " + money;
        musicVol = PlayerPrefs.GetFloat("MusicPlay", 1);
        soundVol = PlayerPrefs.GetFloat("SoundPlay", 1);
        SetMusVolume(musicVol);
        SetMusVolume(soundVol);
    }

    public void SetMusVolume(float val)
    {
        playMusic.GetComponent<AudioSource>().volume = val;

        if (musicVol == 0)
        {
            musicOff.gameObject.SetActive(false);
            musicOn.gameObject.SetActive(true);
        }
        else
        {
            musicOff.gameObject.SetActive(true);
            musicOn.gameObject.SetActive(false);
        }
    }


    public void SetVolume(float val)
    {
        sellingController.SetVolume(val);

        if (soundVol == 0)
        {
            soundOff.gameObject.SetActive(false);
            soundOn.gameObject.SetActive(true);
        }
        else
        {
            soundOff.gameObject.SetActive(true);
            soundOn.gameObject.SetActive(false); 
        }

    }


    public void UpdateScore(int scoreToAdd)
    {
        money += scoreToAdd;
        scoreText.text = "$ " + money;
        PlayerPrefs.SetInt("MoneyScore", money);

    }


    public void SoundsOn()
    {
        soundVol = 1;
        SetVolume(soundVol);
        soundOff.gameObject.SetActive(false);
        soundOn.gameObject.SetActive(true);
        playAudio.PlayOneShot(clickSound, soundVol);
    }

    public void SoundsOff()
    {
        soundVol = 0;
        SetVolume(soundVol);
        soundOff.gameObject.SetActive(true);
        soundOn.gameObject.SetActive(false);
        playAudio.PlayOneShot(clickSound, soundVol);
    }


    public void MusicOn()
    {
        musicVol = 1;
        SetMusVolume(musicVol);
        musicOff.gameObject.SetActive(false);
        musicOn.gameObject.SetActive(true);
        playAudio.PlayOneShot(clickSound, soundVol);
    }

    public void MusicOff ()
    {
        musicVol = 0;
        SetMusVolume(musicVol);
        musicOff.gameObject.SetActive(true);
        musicOn.gameObject.SetActive(false);
        playAudio.PlayOneShot(clickSound, soundVol);
    }


    public void OpenSettings()
    {
        gameIsPaused = true;
        Time.timeScale = 0f;
        SettingsWin.gameObject.SetActive(true);
        playAudio.PlayOneShot(clickSound, soundVol);
    }

    public void SaveSettings()
    {
        PlayerPrefs.SetFloat("MusicPlay", musicVol);
        PlayerPrefs.SetFloat("SoundPlay", soundVol);
        SettingsWin.gameObject.SetActive(false);
        gameIsPaused = false;
        Time.timeScale = 1.0f;
        playAudio.PlayOneShot(clickSound, soundVol);
    }



}
