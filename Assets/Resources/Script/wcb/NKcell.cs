using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NKcell : WhiteBloodCell
{

    //public float _dmg;
    public LayerMask _mask;


    protected override void Attack()
    {

        if (_cool_down_atk_foor_loop > 0)
        {
            _cool_down_atk_foor_loop -= Time.deltaTime;
        }
        if (_cool_down_atk_foor_loop < 0)
        {
            //print("at");
            int enemy_mask = LayerMask.NameToLayer("enemy");
            Collider2D[] collider2D_list = Physics2D.OverlapCircleAll(transform.position, 1, _mask);
            foreach (Collider2D collider2D in collider2D_list)
            {
                collider2D.transform.gameObject.GetComponent<Virus>().Takedmg(_dmg);

            }
            FindObjectOfType<SoundManager>().Play("sound_hit");
            GameObject hit = (GameObject)Instantiate(hitRef);
            hit.transform.position = new Vector2(transform.position.x, transform.position.y);

            _cool_down_atk_foor_loop = _cool_down_atk;
        }
    }
}
