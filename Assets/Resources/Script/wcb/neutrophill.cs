using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class neutrophill : WhiteBloodCell
{

    //public float _dmg;
    public LayerMask _mask;

    //private void Start()
    //{
        //_dmg *= 1 + _level / 10;

    //}
    //private void Update()
    //{

        //print("test");
    // }


    protected override void Attack()
    {

        if (_cool_down_atk_foor_loop > 0)
        {
            _cool_down_atk_foor_loop -= Time.deltaTime;
        }
        if (_cool_down_atk_foor_loop < 0)
        {
            //print("at");
            //int enemy_mask = LayerMask.NameToLayer("enemy");
            Collider2D[] collider2D_list = Physics2D.OverlapCircleAll(transform.position, 1, _mask);
            
            Vector2 _position_shoot = nearest_range_enemy.transform.position;
            Vector2 look_dir = _position_shoot - _rb.position;
            //collider2D_list[0].transform.gameObject.GetComponent<Rigidbody2D>().AddForce(-look_dir.normalized * _dmg/10f, ForceMode2D.Impulse);
            GameObject hit = (GameObject)Instantiate(hitRef);
            hit.transform.position = new Vector2(transform.position.x, transform.position.y);

            collider2D_list[0].transform.gameObject.GetComponent<Virus>().Takedmg(_dmg);
            FindObjectOfType<SoundManager>().Play("sound_hit");

            
            _cool_down_atk_foor_loop = _cool_down_atk;
        }
    }
}
