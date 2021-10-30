using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Monocyte : WhiteBloodCell
{

    //public float _dmg;
    public LayerMask _mask;
    public LayerMask _mask_wcb;

    public float _skill_range;
    public GameObject _par_heal;

    //private void Start()
    //{
    //    //_dmg *= 1 + _level / 10;

    //}
    //private void Update()
    //{

    //    print("test");
    //}


    protected override void Attack()
    {

        if (_cool_down_atk_foor_loop > 0)
        {
            _cool_down_atk_foor_loop -= Time.deltaTime;
        }
        if (_cool_down_atk_foor_loop < 0)
        {
            //print("at");
           
            Collider2D[] collider2D_list = Physics2D.OverlapCircleAll(transform.position, 1, _mask);
            foreach (Collider2D collider2D in collider2D_list)
            {
                collider2D.transform.gameObject.GetComponent<Virus>().Takedmg(_dmg);
                GameObject hit = (GameObject)Instantiate(hitRef);
                hit.transform.position = new Vector2(transform.position.x, transform.position.y);
                FindObjectOfType<SoundManager>().Play("sound_hit");

            }
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
            // buff skill
            FindObjectOfType<SoundManager>().Play("sound_heal");
            Instantiate(_par_heal, transform.position, Quaternion.identity);
            Collider2D[] collider2D_list = Physics2D.OverlapCircleAll(transform.position, _skill_range, _mask_wcb);
            foreach (Collider2D collider2D in collider2D_list)
            {
                //heal 30 percent.
                //OnDrawGizmosSelected();
                
                collider2D.transform.gameObject.GetComponent<WhiteBloodCell>()._health *= 1.3f;

                
            }

            _cool_down_skill_foor_loop = _cool_down_skill;
        }
    }




}
