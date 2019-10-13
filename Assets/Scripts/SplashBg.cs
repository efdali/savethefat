using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using GoogleMobileAds.Api;

public class SplashBg : MonoBehaviour
{
    public Text scoreText;
    public Toggle soundToggle;
    void Start()
    {
        PlayerPrefs.SetInt("GameScore", 0);
        PlayerPrefs.SetInt("GameResume", 0);
        PlayerPrefs.SetInt("foodSpeed", 1);
        scoreText.text = "Best Score : " + PlayerPrefs.GetInt("score");
        soundToggle.isOn = PlayerPrefs.GetInt("SoundOn") == 0 ? false : true;
        MobileAds.Initialize(initStatus => { });
    }

}
