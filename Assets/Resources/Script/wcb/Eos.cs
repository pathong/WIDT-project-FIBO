using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Eos : WhiteBloodCell
{

    

    public GameObject _bullet;
    public float _bullet_force;
    //public float _bullet_dmg;
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
            float _angle = Mathf.Atan2(look_dir.y, look_dir.x) * Mathf.Rad2Deg - 90f;
            //this.GetComponentInChildren<Rigidbody2D>().rotation = _angle;

            GameObject _bullet_obj = Instantiate(_bullet, transform.position, transform.rotation);
            _bullet_obj.GetComponent<Rigidbody2D>().AddForce(look_dir.normalized * _bullet_force, ForceMode2D.Impulse);
            _bullet_obj.GetComponent<Bullet_wcb>()._bullet_dmg = _dmg;
            this.GetComponent<Rigidbody2D>().AddForce(-look_dir.normalized * _bullet_force/10, ForceMode2D.Impulse);
            FindObjectOfType<SoundManager>().Play("sound_shoot");

            _cool_down_atk_foor_loop = _cool_down_atk;
        }
    }

    protected override void Skill()
    {

        if (_cool_down_skill_foor_loop > 0)
        {
            _cool_down_skill_foor_loop -= Time.deltaTime;
        }
        if (_cool_down_skill_foor_loop < 0)
        {

            StartCoroutine(Each_shoot());

            _cool_down_skill_foor_loop = _cool_down_skill;
        }
    }

    public IEnumerator Each_shoot()
    {

        if(nearest_range_enemy != null)
        {
            Vector2 _position_shoot = nearest_range_enemy.transform.position;
            Vector2 look_dir = _position_shoot - _rb.position;
            //float _angle = Mathf.Atan2(look_dir.y, look_dir.x) * Mathf.Rad2Deg - 90f;
            //this.GetComponentInChildren<Rigidbody2D>().rotation = _angle;
            for (int i = 0; i < 3; i++)
            {
                GameObject _bullet_obj1 = Instantiate(_bullet, transform.position, transform.rotation);
                _bullet_obj1.GetComponent<Rigidbody2D>().AddForce(look_dir.normalized * _bullet_force, ForceMode2D.Impulse);
                _bullet_obj1.GetComponent<Bullet_wcb>()._bullet_dmg = _dmg * 1.5f;
                FindObjectOfType<SoundManager>().Play("sound_shoot");
                this.GetComponent<Rigidbody2D>().AddForce(-look_dir.normalized * _bullet_force / 10, ForceMode2D.Impulse);
                yield return new WaitForSeconds(0.1f);
            }
        }



    }



}
