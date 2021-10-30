using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class House : MonoBehaviour
{
    // Start is called before the first frame update

    public float _house_health;
    public float _house_health_max;

    public Slider _house_health_bar;
    public TMP_Text _house_health_text;

    public int _house_level;
    public GameManeger _gameManager;

    public bool _can_regen_health;

    public GameObject _par_dead;




    void Start()
    {
        _house_health = _house_health_max;

    }

    // Update is called once per frame
    void Update()
    {

        _house_level = _gameManager._House_Level;
        _house_health_bar.value = _house_health / _house_health_max;
        _house_health_text.SetText(((int)(_house_health)).ToString() + " / " + ((int)(_house_health_max)).ToString());

        if(_house_health <= 0)
        {
            //Die();
            StartCoroutine(Die());

        }


        //if (Input.anyKeyDown)
        //{
        //    Take_dmg(1);
        //}

    }

    public void Take_dmg(float dmg)
    {
        _house_health -= dmg;
        Instantiate(_par_dead, transform.position, Quaternion.identity);
        FindObjectOfType<SoundManager>().Play("sound_hit");


    }

    public IEnumerator Die()
    {
        //! particle
        Instantiate(_par_dead, transform.position, Quaternion.identity);
        //! sound
        FindObjectOfType<SoundManager>().Play("sound_vaccine_bomb");

        //! wait for ded panel
        yield return new WaitForSeconds(.2f);
        //! show dead panel
        FindObjectOfType<pause>().Game_end();
        Destroy(this.gameObject);


        //!
    }


}
