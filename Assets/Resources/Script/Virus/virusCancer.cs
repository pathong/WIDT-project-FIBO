using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class virusCancer : MonoBehaviour
{
    public float speed;
    private Transform targetWaypoint;
    private int targetIndex;
    public float startHealth = 5;

    private float health;
    public float _energy = 5;
    private UnityEngine.Object dedRef;
    [Header("Healthbar Unity")]
    public Image healthBar;


    



    void Start()
    {
        dedRef = Resources.Load("Prefabs/Particles/par_ded");

        targetWaypoint = GameObject.FindGameObjectWithTag("waypoints").GetComponent<Transform>();
        health = startHealth;

 

    }
    public void Takedmg (float dmg)
    {
        health -= dmg;

        healthBar.fillAmount = health / startHealth;
        Debug.Log("Took Damage");

        if(health <= 0)
        {
            die();
        }
    }
    void Update()
    {
        transform.position = Vector2.MoveTowards(transform.position, targetWaypoint.position, speed * Time.deltaTime);
        endPathVirus();




    }
  
    public void endPathVirus()
    {
        if (Vector2.Distance(transform.position, targetWaypoint.position) < 0.1f){

            die();
            Debug.Log("Enemy reached");

            waveSpawner.EnemiesisAlive--;


            Destroy(this.gameObject);


        }
    }

    private void die ()
    {
        GameObject explosion = (GameObject)Instantiate(dedRef);
        explosion.transform.position = new Vector2(transform.position.x, transform.position.y);
        FindObjectOfType<SoundManager>().Play("sound_ded");
        Destroy(this.gameObject);


    }

}
    


