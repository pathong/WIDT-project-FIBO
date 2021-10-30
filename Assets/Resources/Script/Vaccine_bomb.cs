using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Vaccine_bomb : MonoBehaviour
{
    public float _dmg;
    public GameObject _par;
    // Start is called before the first frame update
    void Start()
    {
        LeanTween.moveY(this.gameObject, -1.04f, 3f).setEase(LeanTweenType.easeOutBounce).setOnComplete(() =>
        {
            Explode();
        }); 
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void Explode()
    {
        Instantiate(_par, transform.position, Quaternion.identity);
        GameObject[] enermy_in_field = GameObject.FindGameObjectsWithTag("enemy");
        foreach(GameObject enemy in enermy_in_field)
        {
            enemy.GetComponent<Virus>().Takedmg(_dmg);
        }

        FindObjectOfType<SoundManager>().Play("sound_vaccine_bomb");
        Destroy(this.gameObject);


    }
}
