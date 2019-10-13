using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FoodWalk : MonoBehaviour
{
    Rigidbody2D rigid2D;
    GameObject Knife;
    KnifeWalk knifeScript;

    void Start()
    {
        rigid2D = GetComponent<Rigidbody2D>();
        Knife = GameObject.FindGameObjectWithTag("knife");
        knifeScript = Knife.GetComponent<KnifeWalk>();

    }

    void Update()
    {
    }

    void FixedUpdate()
    {
        float hiz = knifeScript.Range();
        rigid2D.velocity = new Vector2(-15 * Time.deltaTime * hiz, 0);
    }

}
