using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class B_cell_plasme : WhiteBloodCell
{

    public GameObject _bullet;
    public float _bullet_force;
    public float _bullet_dmg;

    public GameObject _explode_bullet;
    public float _explode_bullet_force;
    public float _explode_bullet_dmg;

    
    //public LayerMask _mask_wcb;

    //public float _skill_range;

    // Start is called before the first frame update
    protected override void Attack()
    {

        if (_cool_down_atk_foor_loop > 0)
        {
            _cool_down_atk_foor_loop -= Time.deltaTime;
        }
        if (_cool_down_atk_foor_loop < 0)
        {
            //print("at");
            Vector2 _position_shoot = nearest_range_enemy.transform.position;
            Vector2 look_dir = _position_shoot - _rb.position;


            GameObject _bullet_obj = Instantiate(_bullet, transform.position, transform.rotation);
            _bullet_obj.GetComponent<Rigidbody2D>().AddForce(look_dir.normalized * _bullet_force, ForceMode2D.Impulse);
            _bullet_obj.GetComponent<Bullet_wcb>()._bullet_dmg = _bullet_dmg;
            this.GetComponent<Rigidbody2D>().AddForce(-look_dir.normalized * _bullet_force / 10, ForceMode2D.Impulse);
            FindObjectOfType<SoundManager>().Play("sound_shoot");

            _cool_down_atk_foor_loop = _cool_down_atk;
        }




    }

    protected override void Skill()
    {
        //print(_cool_down_skill_foor_loop);
        if (_cool_down_skill_foor_loop > 0)
        {
            _cool_down_skill_foor_loop -= Time.deltaTime;
        }
        if (_cool_down_skill_foor_loop < 0)
        {

            Shoot_explode();

            _cool_down_skill_foor_loop = _cool_down_skill;
        }

    }

    public void Shoot_explode()
    {
        if(nearest_range_enemy != null)
        {
            Vector2 _position_shoot = nearest_range_enemy.transform.position;

            Vector2 look_dir = _position_shoot - _rb.position;

            GameObject explode_bullet = Instantiate(_explode_bullet, transform.position, Quaternion.identity);
            explode_bullet.GetComponent<Rigidbody2D>().AddForce(look_dir.normalized * _bullet_force, ForceMode2D.Impulse);
            explode_bullet.GetComponent<explode_bullet>()._dmg = _bullet_dmg;
            this.GetComponent<Rigidbody2D>().AddForce(-look_dir.normalized * _bullet_force / 10, ForceMode2D.Impulse);


            FindObjectOfType<SoundManager>().Play("sound_shoot");
        }

    }
}
