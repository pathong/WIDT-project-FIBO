using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System.Linq;

public class WhiteBloodCell : MonoBehaviour
{
    
    [HideInInspector] public int _level;
    public int _price;
    public float _cool_down;
    public string _name;
    public int _level_need;

    public float _range_move;
    public float _range_attack;

    public float _move_speed;
    public float _move_time;
    private float _move_time_for_loop;
    protected Rigidbody2D _rb;

    private GameObject[] _list_enemy;
    protected GameObject nearest_range_enemy;

    //[HideInInspector]
    public float _health;
    public float max_health;

    [HideInInspector] public float _life_time;
    public float max_life_time;

    public Slider _cell_health_bar;
    public Slider _cell_life_time_bar;

    public Vector3 offset_health_bar;
    public Vector3 offset_life_time_bar;
    //public TMP_Text _house_health_text;

    [SerializeField] protected float _cool_down_atk;
    [HideInInspector] protected float _cool_down_atk_foor_loop;

    //[SerializeField]
    public float _cool_down_skill;
    public float _cool_down_skill_foor_loop;


    private UnityEngine.Object dedRef;
    protected UnityEngine.Object hitRef;

    public float _dmg;

    public string _cell_info;
    public string _skill_info;

    public Sprite _img;
    //public GameObject _par_dead = Resources.Load("Prefabs/Particles/par_ded", typeof(GameObject)) as GameObject;










    // Start is called before the first frame update
    void Start()
    {


        //_par_dead = Resources.Load<GameObject>("Prefabs/Particles/par_ded.prefab");
        dedRef = Resources.Load("Prefabs/Particles/par_ded");
        hitRef = Resources.Load("Prefabs/Particles/par_hit");
        //! FIRST STATS
        _level = GameObject.FindGameObjectWithTag("Gamemaneger").GetComponent<GameManeger>()._House_Level;
        _rb = this.GetComponent<Rigidbody2D>();
        nearest_range_enemy = null;
        //_dmg *= 1 + _level / 10;
        _health = max_health;
        _life_time = max_life_time;
        //start Move;
        _rb.AddForce(Vector2.left * 150);

        _move_time_for_loop = _move_time;
        _cool_down_atk_foor_loop = _cool_down_atk;
        _cool_down_skill_foor_loop = _cool_down_skill;

    }

    // Update is called once per frame
    void Update()
    {

        if(_health >= max_health)
        {
            _health = max_health;
        }


        _cell_health_bar.transform.position = Camera.main.WorldToScreenPoint(transform.position + offset_health_bar);
        _cell_life_time_bar.transform.position = Camera.main.WorldToScreenPoint(transform.position + offset_life_time_bar);

        _cell_health_bar.value = _health / max_health;
        _cell_life_time_bar.value = _life_time / max_life_time;

        Redure_life_time();


        if(_life_time <= 0 || _health <= 0)
        {
            Dead();
        }


        
        nearest_range_enemy = Find_enemy();

        //attack
        if(nearest_range_enemy != null)
        {

            if (Vector2.Distance(nearest_range_enemy.transform.position, transform.position) > _range_attack)
            {
                Move_to_enemy();
            }
            if (Vector2.Distance(nearest_range_enemy.transform.position, transform.position) <= _range_attack)
            {
                Attack();


            }
        }

        Skill();

        // if not have enemy in range;
        if (nearest_range_enemy == null)
        {
            Random_move();
        }

    }

    public void Random_move()
    {
        if (_move_time_for_loop > 0)
        {
            _move_time_for_loop -= Time.deltaTime;
        }
        if (_move_time_for_loop < 0)
        {
            //! set initaite direction
            Vector2 random_direction = new Vector2(0, 0);
            float _random_side = Random.Range(0f, 1f);
            if (_random_side < 0.8)
            {
                random_direction = new Vector2(Random.Range(-1f, -.05f), Random.Range(-1f, 1f));

            }
            if (_random_side > 0.8)
            {
                random_direction = new Vector2(Random.Range(-.5f, 1f), Random.Range(-1f, 1f));

            }
            //print(random_direction);

            //find random force
            float _max_move_speed = _move_speed * 1.7f;
            float _min_move_speed = _move_speed / .8f;
            float _random_speed = Random.Range(_max_move_speed, _min_move_speed);

            _rb.AddForce(random_direction * _random_speed);
            FindObjectOfType<SoundManager>().Play("sound_wcb_jump");
            _move_time_for_loop = Random.Range(_move_time * 0.7f, _move_time * 1.3f);
        }
    }

    public void Move_to_enemy()
    {

        // move to enemy
        transform.position = Vector2.MoveTowards(transform.position, nearest_range_enemy.transform.position, Time.deltaTime * _move_speed / 35f);

    }

    public GameObject Find_enemy()
    {

        float _distanceToclosedEnemy = Mathf.Infinity;
        GameObject closetEnemy = null;
        GameObject[] _list_enemy = GameObject.FindGameObjectsWithTag("enemy");

        foreach( GameObject currentEnemy in _list_enemy)
        {
            float _distance_to_enemy = (currentEnemy.transform.position - this.transform.position).sqrMagnitude;
            if(_distance_to_enemy < _distanceToclosedEnemy && _distance_to_enemy < _range_move)
            {
                _distanceToclosedEnemy = _distance_to_enemy;
                closetEnemy = currentEnemy;
            }
        }
        return closetEnemy;

    }

    protected virtual void Attack()
    {
        //FindObjectOfType<SoundManager>().Play("sound_wcb_spawn");
    }

    public void Particle_when_hit()
    {
        GameObject hit = (GameObject)Instantiate(dedRef);
        hit.transform.position = new Vector2(transform.position.x, transform.position.y);
    }

    protected virtual void Skill()
    {

    }

    
    public void Redure_life_time()
    {
        _life_time -= Time.deltaTime/3;
    }
    public void Dead()
    {

        //Instantiate(Resources.Load<ParticleSystem>("Prefabs/Particles/par_ded.prefab"), transform.position, Quaternion.identity);

        GameObject explosion = (GameObject)Instantiate(dedRef);
        explosion.transform.position = new Vector2(transform.position.x, transform.position.y);
        FindObjectOfType<SoundManager>().Play("sound_ded");


        Destroy(this.gameObject);
    }

    public void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.tag == "wall")
        {
            Dead();
        }
    }

    public void Take_dmg(float _dmg)
    {
        print("dmg");
        _health -= _dmg;
    }


}
