using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Character : MonoBehaviour
{
    public Text scoreText;
    public GameObject explosion;
    GameObject gameBg;
    GameBg bgScript;
    AudioSource eatingAudio;

    void Start()
    {
        eatingAudio = GetComponent<AudioSource>();
        gameBg = GameObject.FindGameObjectWithTag("bgScript");
        bgScript = gameBg.GetComponent<GameBg>();
    }

    void Update()
    {

    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == "harmfulFood")
        {
            Destroy(collision.gameObject);
            Instantiate(explosion, transform.position, transform.rotation);
            Destroy(gameObject);
            bgScript.GameOver();
        }
        else if (collision.tag == "healthyFood")
        {
            if (PlayerPrefs.GetInt("SoundOn") == 1)
            {
                eatingAudio.Play(0);
            }
            Destroy(collision.gameObject);
            scoreText.text = "Score : " + (bgScript.setScore());
        }
    }

}
