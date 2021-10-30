using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class GameManeger : MonoBehaviour
{
    public GameObject[] _list_white_blood_cell;

    public int _House_Level = 1;
    public int _House_Level_max = 15;
    public House houseObj;

    public float _energy;
    public float _energy_max;

    public TMP_Text _text_energy;
    public TMP_Text _text_house_lvl;

    public TMP_Text _text_price_house;

    public float[] _list_price_house;

    public float _price_bomb;
    public TMP_Text _text_price_bomb;
    public GameObject _bomb_obj;
    public Transform _bomb_spawner;

    public GameObject _par_up_house;


    // Start is called before the first frame update
    void Start()
    {
        _list_price_house = new float[] { 15f, 40f, 60f, 90f, 120f, 160f, 200f, 400f, 500f};
        _energy = _energy_max;
    }

    // Update is called once per frame
    void Update()
    {
        _text_price_bomb.SetText(((int)(_price_bomb)).ToString());

        _text_energy.SetText(((int)(_energy)).ToString());
        _text_house_lvl.SetText("Lv. " + _House_Level.ToString());

        _text_price_house.SetText(_list_price_house[_House_Level - 1].ToString());


        _energy += 10 * Time.deltaTime * _House_Level/10;
        if(_energy > _energy_max)
        {
            _energy = _energy_max;
        }
        //print(_energy);
        
    }


    // use for button upgrade house
    public void Upgrage_LV_House()
    {
        if(( _energy >= _list_price_house[_House_Level-1]) && (_House_Level < _House_Level_max))
        {
            _energy -= _list_price_house[_House_Level - 1];
            _energy_max *= 3;
            _House_Level += 1;
            houseObj._house_health_max += 50;
            houseObj._house_health += 50;
            _energy_max *= 2;
            FindObjectOfType<SoundManager>().Play("sound_up_house");
            Vector3 house_transform = houseObj.transform.position;
            Instantiate(_par_up_house, new Vector3(house_transform.x+1,house_transform.y, house_transform.z), Quaternion.identity);


            if(_House_Level <= 5)
            {
                Spawn[] button_spawn_arr = FindObjectsOfType<Spawn>();

                foreach (Spawn spawn in button_spawn_arr)
                {
                    spawn._cool_down *= 1 - (_House_Level / 35f);
                }
            }
            

        }

    }

    public void Spawn_Bomb()
    {
        if(_energy >= _price_bomb)
        {
            Instantiate(_bomb_obj, _bomb_spawner.position, Quaternion.identity);
            _energy -= _price_bomb;
        }

    }

    public void gain_energy(float value)
    {
        _energy += value;
    }


}
