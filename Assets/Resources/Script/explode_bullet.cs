using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class explode_bullet : MonoBehaviour
{
    [HideInInspector] public float _dmg;
    public float _dmg_range;
    [HideInInspector] public float _time_bome_left;
    public float _time_bome;
    public LayerMask _mask;
    public float _knock_back_force;


    public GameObject _par_bomb;
    private void Start()
    {
        _time_bome_left = _time_bome;
    }

    private void Update()
    {
        _time_bome_left -= Time.deltaTime;
        //print(_time_bome_left);
        if(_time_bome_left <= 0)
        {
            print("explode");
            Explode();
	    print("hello");
            //? particle

        }
    }

    public void Explode()
    {
        Collider2D[] collider2D_list = Physics2D.OverlapCircleAll(transform.position, _dmg_range, _mask);
        foreach (Collider2D collider2D in collider2D_list)
        {
            collider2D.transform.gameObject.GetComponent<Virus>().Takedmg(_dmg);
            collider2D.transform.gameObject.GetComponent<Rigidbody2D>().AddForce((collider2D.transform.position - transform.position).normalized, ForceMode2D.Impulse);
        }
        FindObjectOfType<SoundManager>().Play("sound_bullet_boom");
        Instantiate(_par_bomb, transform.position, Quaternion.identity);

        Destroy(this.gameObject);
    }

}
