using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using GoogleMobileAds.Api;
using System;

public class BtnClickEvent : MonoBehaviour
{
    public GameObject EntryPanel;
    public GameObject CreditsPanel;
    AudioSource btn_start_audio;
    private RewardBasedVideoAd reklamObjesi;
    private string adsId = "ca-app-pub-1387518596299983/7430882820";

    public Button resumeBtn;

    public void Resume_Game()
    {
        MobileAds.Initialize("ca-app-pub-1387518596299983~7726041072");
        resumeBtn.GetComponentInChildren<Text>().text = "Loading...";
        GameObject knife = GameObject.FindGameObjectWithTag("knife");
        KnifeWalk knifeScript = knife.GetComponent<KnifeWalk>();
        reklamObjesi = RewardBasedVideoAd.Instance;
        AdRequest reklamIstegi = new AdRequest.Builder().Build();
        reklamObjesi.LoadAd(reklamIstegi,adsId);
        PlayerPrefs.SetInt("GameScore", knifeScript.score);
        PlayerPrefs.SetInt("foodSpeed", knifeScript.foodSpeed);
        PlayerPrefs.SetInt("GameResume", 1);

        if (reklamObjesi.IsLoaded())
        {
            reklamObjesi.Show();
            reklamObjesi.OnAdRewarded += HandleVideoReward;
        }
        else
        {
            reklamObjesi.OnAdLoaded += HandleVideoLoaded;

        }

    }

    public void HandleVideoLoaded(object sender, EventArgs args)
    {
        reklamObjesi.Show();
        reklamObjesi.OnAdRewarded += HandleVideoReward;
    }

    public void HandleVideoReward(object sender, Reward args)
    {
        resumeBtn.GetComponentInChildren<Text>().text = "Resume";
        SceneManager.LoadScene("GameScene");
        reklamObjesi.OnAdRewarded -= HandleVideoReward;
    }

    public void Start_Btn()
    {
        StartCoroutine(Play_Sound("GameScene"));
    }

    IEnumerator Play_Sound(string sceneName)
    {
        if (PlayerPrefs.GetInt("SoundOn") == 1)
        {
            btn_start_audio = GetComponent<AudioSource>();
            btn_start_audio.Play();
            yield return new WaitForSeconds(0.1f);
        }
        SceneManager.LoadScene(sceneName);
    }


    public void Restart_Btn()
    {
        StartCoroutine(Play_Sound(SceneManager.GetActiveScene().name));
    }

    public void Main_Menu_Btn()
    {
        StartCoroutine(Play_Sound("SplashScene"));
    }

    public void Credits_Btn()
    {
        StartCoroutine(Play_Without_Wait());
        EntryPanel.SetActive(false);
        CreditsPanel.SetActive(true);
    }

    public void Back_Btn()
    {
        StartCoroutine(Play_Without_Wait());
        EntryPanel.SetActive(true);
        CreditsPanel.SetActive(false);
    }

    IEnumerator Play_Without_Wait()
    {
        if (PlayerPrefs.GetInt("SoundOn") == 1)
        {
            btn_start_audio = GetComponent<AudioSource>();
            btn_start_audio.Play(0);
            yield return new WaitForSeconds(0.1f);
            btn_start_audio.Stop();
        }
    }

    public void OnChangeValue(bool value)
    {
        PlayerPrefs.SetInt("SoundOn", value == true ? 1 : 0);
    }
}
