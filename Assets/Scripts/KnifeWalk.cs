using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class KnifeWalk : MonoBehaviour
{
    Rigidbody2D rigid2d;
    public GameObject explosion;
    public Button resumeBtn;
    public int way = 1;
    public int speed = 7;
    public int score;
    public int foodSpeed = 1;
    public Text scoreText;
    int lastInc = 0;
    void Start()
    {
        foodSpeed = PlayerPrefs.GetInt("foodSpeed");
        rigid2d = GetComponent<Rigidbody2D>();
        score = PlayerPrefs.GetInt("GameScore");
        if (PlayerPrefs.GetInt("GameResume") == 1)
        {
            resumeBtn.gameObject.SetActive(false);
            PlayerPrefs.SetInt("GameResume", 0);
        }
        else
        {
            resumeBtn.gameObject.SetActive(true);
        }
    }

    void Update()
    {
        if (SceneManager.GetActiveScene().name == "GameScene")
        {
            if (Input.GetButtonDown("Fire1"))
            {
                way *= -1;
            }
        }

    }
    void FixedUpdate()
    {
        if (SceneManager.GetActiveScene().name == "GameScene")
        {
            rigid2d.velocity = new Vector2(15 + foodSpeed, (25 + foodSpeed) * way) * Time.deltaTime * speed;
            transform.Rotate(new Vector3(0, 0, -30));
        }
    }

    void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.tag == "bg")
        {
            rigid2d.position = new Vector2(-9, transform.position.y);
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == "harmfulFood")
        {
            GameObject exp = Instantiate(explosion, collision.transform.position, collision.transform.rotation);
            Destroy(collision.gameObject);
            Destroy(exp, 2);
            score += 5;
        }
        else if (collision.tag == "healthyFood")
        {
            Destroy(collision.gameObject);
            score -= 5;
        }
        scoreText.text = "Score : " + score;

        if (score > lastInc && score % 50 == 0)
        {
            if (lastInc >= 100)
                foodSpeed += 2;
            else
                foodSpeed += 5;

            lastInc = score;
        }
    }


    public int Range()
    {
        return Random.Range(3 + foodSpeed, 15 + foodSpeed);
    }

}
