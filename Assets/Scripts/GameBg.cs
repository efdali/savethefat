using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using GoogleMobileAds.Api;
using Random = UnityEngine.Random;

public class GameBg : MonoBehaviour
{

    private RewardBasedVideoAd reklamObjesi;

    public GameObject[] foods;
    public GameObject[] harmfulFoodList;
    public Text scoreText;
    public Text panelScoreText;
    public Text panelBestScoreText;
    GameObject knife;
    KnifeWalk knifeScript;
    GameObject foodObject;
    GameBg foodScript;
    public GameObject panel;

    string oldFoodTag="tag";

    int healtyFood = 0,harmfulFood=0;

    void Start()
    {
        knife = GameObject.FindGameObjectWithTag("knife");
        knifeScript = knife.GetComponent<KnifeWalk>();
        foodObject = GameObject.FindGameObjectWithTag("bgScript");
        StartCoroutine(CreateFoods());
    }


    IEnumerator CreateFoods()
    {
        while (true)
        {
            for (int i = 0; i < 10; i++)
            {
                GameObject food = foods[Random.Range(0, foods.Length)];
                if(food.tag== "healthyFood")
                {
                    while (oldFoodTag == food.tag)
                    {
                        food = foods[Random.Range(0, foods.Length)];
                    }
                }

                if(food.tag== "harmfulFood")
                {
                    harmfulFood++;
                }
                else
                {

                    healtyFood++;
                }

                if (healtyFood >= 3)
                {
                    food = harmfulFoodList[Random.Range(0, harmfulFoodList.Length)];
                }
               
                oldFoodTag = food.tag;

                Instantiate(food, new Vector3(9, 0, 0), Quaternion.identity);
                yield return new WaitForSeconds(Random.Range(0.8f, 3));
            }
            healtyFood = 0;
            harmfulFood = 0;
            oldFoodTag = "tag";
            yield return new WaitForSeconds(Random.Range(1, 2.6f));
        }
    }

    public void GameOver()
    {
        int bestScore = PlayerPrefs.GetInt("score");
        knifeScript.speed = 0;
        foodObject.SetActive(false);
        panel.SetActive(true);
        int score = knifeScript.score;
        panelBestScoreText.text = bestScore+"";
        panelScoreText.text = score+"";
        scoreText.enabled = false;
        if(bestScore < score)
        {
            PlayerPrefs.SetInt("score", score);
        }
        PlayerPrefs.SetInt("GameScore", 0);
        PlayerPrefs.SetInt("foodSpeed", 1);
    }

    public int setScore()
    {
        knifeScript.score += 5;
        return knifeScript.score;
    }
}
