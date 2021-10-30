using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;
using UnityEngine.UI;

public class Menu_maneger : MonoBehaviour
{
    [Header("general")]

    public RectTransform fader;
    public RectTransform _tutorial_img;
    public RectTransform _credit_img;
    [HideInInspector]
    public bool _is_open;





    [Header("cell infomation")]
    public GameObject cell_info;
    public Image _cell_img;
    private bool _is_info_open;
    private bool _is_credit_open;
    public TMP_Text text_name, text_lv, text_health, text_dmg, text_info, text_skill;

    // Start is called before the first frame update
    void Start()
    {

        _is_open = false;
        _is_info_open = false;
        _is_credit_open = false;
        fader.gameObject.SetActive(true);


        LeanTween.scale(fader, new Vector3(1, 1, 1), 0);
        LeanTween.scale(fader, Vector3.zero, 2f).setEase(LeanTweenType.easeInOutBack).setOnComplete(() =>
        {
            fader.gameObject.SetActive(true);
        });
            


    }

    // Update is called once per frame
    void Update()
    {

    }

    public void _Start()
    {
        fader.gameObject.SetActive(true);

        LeanTween.scale(fader, Vector3.zero, 0f);
        LeanTween.scale(fader, new Vector3(1, 1, 1), 2f).setEase(LeanTweenType.easeInOutExpo).setOnComplete(() =>
        {
            SceneManager.LoadScene("main_game");
        }); 

    }


    public void _exit()
    {
        fader.gameObject.SetActive(true);

        LeanTween.scale(fader, Vector3.zero, 0f);
        LeanTween.scale(fader, new Vector3(1, 1, 1), 2f).setEase(LeanTweenType.easeInOutExpo).setOnComplete(() =>
        {
            Application.Quit();
        });
    }

    public void _tutorial()
    {
        if (_is_open == false)
        {
            LeanTween.moveX(_tutorial_img, 270.1f, 2f).setEase(LeanTweenType.easeOutBounce);
            _is_open = true;
        }
        else if (_is_open == true)
        {
            LeanTween.moveX(_tutorial_img, 531f, 2f).setEase(LeanTweenType.easeOutBounce);
            _is_open = false;
        }
    }

    public void _credit()
    {
        if (_is_credit_open == false)
        {
            LeanTween.moveX(_credit_img, -270f, 2f).setEase(LeanTweenType.easeOutBounce);
            _is_credit_open = true;
        }
        else if (_is_credit_open == true)
        {
            LeanTween.moveX(_credit_img, -527f, 2f).setEase(LeanTweenType.easeOutBounce);
            _is_credit_open = false;
        }
    }

    public void Info_cell_turtorial(WhiteBloodCell cell)
    {

        if (_is_info_open == false)
        {
            LeanTween.moveX(cell_info, 1f, 2f).setEase(LeanTweenType.easeOutBounce);
            _is_info_open = true;
        }
        else if (_is_info_open == true)
        {
            LeanTween.moveX(cell_info, 15f, 2f).setEase(LeanTweenType.easeOutBounce);
            _is_info_open = false;
        }


        //TODO: name, lv., health, dmg, info
        text_name.SetText(cell.name);
        text_lv.SetText("Lv.: " + cell._level_need.ToString());
        text_health.SetText("DEF: " + cell.max_health.ToString());
        text_dmg.SetText("ATK.: " + cell._dmg.ToString());
        text_info.SetText("INFO.: " + cell._cell_info);
        text_skill.SetText("Skill.: " + cell._skill_info);

        _cell_img.sprite = cell._img;
        
    }


}
