using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Enemy_test : MonoBehaviour
{
    public float health;
    public Slider _cell_health_bar;
    public Vector3 offset_health_bar;

    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        if(health <= 0)
        {
            Dead();
        }
    }

    public void Take_dmg(float _dmg)
    {
        print("dmg");
        health -= _dmg;
    }

    public void Dead()
    {
        Destroy(gameObject);
    }
}
