using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet_wcb : MonoBehaviour
{
    [SerializeField] public float _bullet_dmg;
    public LayerMask _mask_for_hit;
    public float _life_time;

    public void OnTriggerEnter2D(Collider2D other)
    {
        //print(other.transform.gameObject.layer);
        if (_mask_for_hit == (_mask_for_hit | (1<< other.gameObject.layer)))
        {
	    
            other.GetComponent<Virus>().Takedmg(_bullet_dmg);

            //maybe add particle.

            Destroy(this.gameObject);
        }
    }

    private void Update()
    {
        _life_time -= Time.deltaTime;
        
        if(_life_time <= 0)
        {
            Destroy(this.gameObject);
        }
    }
}
