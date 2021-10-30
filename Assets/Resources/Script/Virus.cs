using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class Virus : MonoBehaviour
{
    private House house;
    private GameManeger game;
    private Transform targetWaypoint;
    [SerializeField] private float speed = 1;
    public float startHealth = 40;
    public float dmgtoHouse = 20;
    public float value = 20;

    protected GameObject target;
    private GameObject[] _list_enemy;

    private float health;
    private float dmg = 10;
    public string enemyTag = "wbc";

    [SerializeField]private float attack_range = 5f;

    [SerializeField] protected float cooldown_atk;
    [HideInInspector] protected float cooldown_atk_for_loop;


    public float _dmg;
    public LayerMask _mask;


    protected Rigidbody2D _rb;

    private UnityEngine.Object dedRef;
    protected UnityEngine.Object hitRef;


    [Header("Healthbar Unity")]
    public Image healthBar;

    private void Start()
    {
        house = GameObject.FindGameObjectWithTag("new_waypoint").GetComponent<House>();

        dedRef = Resources.Load("Prefabs/Particles/par_ded");
        health = startHealth;
        targetWaypoint = GameObject.FindGameObjectWithTag("new_waypoint").GetComponent<Transform>();
        target = null;
        cooldown_atk_for_loop = cooldown_atk;
        hitRef = Resources.Load("Prefabs/Particles/par_hit");
    }

    private void Update()
    {
        target = find_wbc();

        if (target != null)
        {

            if (Vector2.Distance(target.transform.position, transform.position) > attack_range)
            {
                transform.position = Vector2.MoveTowards(transform.position, target.transform.position, Time.deltaTime/35f);
            }
            if (Vector2.Distance(target.transform.position, transform.position) <= attack_range)
            {
                Debug.Log("attacking");
                Attack();

            }
        }

        if(target == null)
        {
            objective();
        }





    }


    public void Takedmg(float dmg)
    {

        health -= dmg;
        healthBar.fillAmount = health / startHealth;
        Debug.Log("Took Damage");
        if (health <= 0)
        {
            die();
            waveSpawner.EnemiesisAlive--;
            wavespawnerInfinite.EnemiesisAlive--;

        }
    }


    private void objective()
    {
        if (targetWaypoint != null)
        {
            Vector2 dir = targetWaypoint.position - transform.position;
            transform.Translate(dir.normalized * speed * Time.deltaTime, Space.World);
            if (Vector2.Distance(transform.position, targetWaypoint.position) < 0.1f)
            {
                Destroy(gameObject);
                Debug.Log("Enemy reached");
                house.Take_dmg(dmg);
                waveSpawner.EnemiesisAlive--;
                wavespawnerInfinite.EnemiesisAlive--;
            }

        } else
        {
            Debug.Log("Game Over");
            return;
        }



    }
    void die() {

        GameObject explosion = (GameObject)Instantiate(dedRef);
        explosion.transform.position = new Vector2(transform.position.x, transform.position.y);
        FindObjectOfType<SoundManager>().Play("sound_ded");
        Destroy(this.gameObject);
        FindObjectOfType<GameManeger>()._energy += 10;

        //game.gain_energy(value);

    }

    public GameObject find_wbc()
    {

        float _distanceToclosedEnemy = Mathf.Infinity;
        GameObject closetEnemy = null;
        GameObject[] _list_enemy = GameObject.FindGameObjectsWithTag("wbc");

        foreach (GameObject currentEnemy in _list_enemy)
        {
            float _distance_to_enemy = (currentEnemy.transform.position - this.transform.position).sqrMagnitude;
            if (_distance_to_enemy < _distanceToclosedEnemy && _distance_to_enemy < attack_range)
            {
                _distanceToclosedEnemy = _distance_to_enemy;
                closetEnemy = currentEnemy;
            }
        }
        return closetEnemy;
    }


    protected void Attack()
    {

        if (cooldown_atk_for_loop > 0)
        {
            cooldown_atk_for_loop -= Time.deltaTime;
        }
        if (cooldown_atk_for_loop < 0)
        {
            //print("at");
            //int enemy_mask = LayerMask.NameToLayer("enemy");
            print("hit");
            Collider2D[] collider2D_list = Physics2D.OverlapCircleAll(transform.position, 1, _mask);
            if(collider2D_list.Length != 0)
            {
                //Vector2 _position_shoot = collider2D_list[0].transform.position;
                //Vector2 look_dir = _position_shoot - _rb.position;
                //collider2D_list[0].transform.gameObject.GetComponent<Rigidbody2D>().AddForce(-look_dir.normalized * _dmg / 5, ForceMode2D.Impulse);

                GameObject hit = (GameObject)Instantiate(hitRef);
                hit.transform.position = new Vector2(transform.position.x, transform.position.y);

                collider2D_list[0].transform.gameObject.GetComponent<WhiteBloodCell>().Take_dmg(10);
                FindObjectOfType<SoundManager>().Play("sound_hit");

            }


            cooldown_atk_for_loop = cooldown_atk;
        }
    }


    public void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == "wall")
        {
            die();
        }
    }
}


