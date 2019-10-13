using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Border : MonoBehaviour
{
    GameObject gameBg;
    GameBg bgScript;
    void Start()
    {
        gameBg = GameObject.FindGameObjectWithTag("bgScript");
        bgScript = gameBg.GetComponent<GameBg>();
    }

    void Update()
    {
        
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == "knife")
        {
            bgScript.GameOver();
        }
    }
}
