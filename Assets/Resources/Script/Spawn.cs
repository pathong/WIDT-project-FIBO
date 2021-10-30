using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class Spawn : MonoBehaviour
{

    public GameObject cell;
    [HideInInspector] public GameManeger _gameManager;

    public float _cool_down;
    public float _cool_down_for_loop;

    public TMP_Text _text_price;
    public TMP_Text _text_cell_name;

    public GameObject _par_spawn;

    public Image _cool_down_img;

    // Start is called before the first frame update
    void Start()
    {
        _cool_down = cell.GetComponent<WhiteBloodCell>()._cool_down ; // do with lv.
        _cool_down_for_loop = _cool_down;
        _gameManager = FindObjectOfType<GameManeger>();
    }

    // Update is called once per frame
    void Update()
    {
        _text_price.SetText(cell.GetComponent<WhiteBloodCell>()._price.ToString());
        _text_cell_name.SetText(cell.GetComponent<WhiteBloodCell>()._name);


        _cool_down_img.fillAmount = _cool_down_for_loop / _cool_down;

        //
        if (_gameManager._House_Level >= cell.GetComponent<WhiteBloodCell>()._level_need)
        {
            if (_cool_down_for_loop >= 0)
            {
                _cool_down_for_loop -= Time.deltaTime;
            }
            if (_cool_down_for_loop < 0)
            {
                _cool_down_for_loop = 0;
            }
        }

        //
    }

    public void Spawn_wb()
    {

        //int lv_wb = this.GetComponent<GameManeger>()._House_Level;


        // condition to spawn
            // have energy >= price.
            // cooldown of each cell = 0.
            // house level > cell level need.

        if (cell.GetComponent<WhiteBloodCell>()._price <= _gameManager._energy 
            &&
            _cool_down_for_loop == 0 
            && 
            _gameManager._House_Level >= cell.GetComponent<WhiteBloodCell>()._level_need
            )
        {
            //spawn
            _gameManager._energy -= cell.GetComponent<WhiteBloodCell>()._price;
            Instantiate(cell, new Vector3(9f, 0f, 0f), Quaternion.identity);
            Instantiate(_par_spawn, new Vector3(9f, 0f, 0f), Quaternion.identity);
            FindObjectOfType<SoundManager>().Play("sound_wcb_spawn");
            _cool_down_for_loop = _cool_down;

        }

    }
}
