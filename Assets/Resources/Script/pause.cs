using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class pause : MonoBehaviour
{

    public GameObject _panel;
    public GameObject _end_panel;
    public bool _ispause;
    public bool _is_ded;
    public GameObject fader;
    // Start is called before the first frame update
    void Start()
    {
        _ispause = false;
	
	_is_ded = false;
    }

    // Update is called once per frame
    void Update()
    {
        
        
        if (Input.GetKeyDown(KeyCode.Escape) && !_is_ded)
        {
            if (!_ispause)
            {
                LeanTween.moveY(_panel, -6f, 3f).setIgnoreTimeScale(true).setEase(LeanTweenType.easeOutBounce);
                Time.timeScale = 0f;
                _ispause = true;
            }
            else if (_ispause)
            {

                LeanTween.moveY(_panel, 10f, 3f).setIgnoreTimeScale(true).setEase(LeanTweenType.easeInOutBack);
                Time.timeScale = 1f;
                _ispause = false;
            }
        }

        
    }

    public void Game_end()
    {
        LeanTween.moveY(_end_panel, -11f, 3f).setIgnoreTimeScale(true).setEase(LeanTweenType.easeOutBounce);
        Time.timeScale = 0f;
        _is_ded = true;
    }


    public void Reset_scene()
    {
        //! reset scene
        fader.gameObject.SetActive(true);

        LeanTween.scale(fader, Vector3.zero, 0f);
        LeanTween.scale(fader, new Vector3(1, 1, 1), 2f).setIgnoreTimeScale(true).setEase(LeanTweenType.easeInOutExpo).setOnComplete(() =>
        {
            SceneManager.LoadScene("main_game");
            Time.timeScale = 1f;
        });
    }

    public void Menu_scene()
    {
        fader.gameObject.SetActive(true);

        LeanTween.scale(fader, Vector3.zero, 0f);
        LeanTween.scale(fader, new Vector3(1, 1, 1), 2f).setIgnoreTimeScale(true).setEase(LeanTweenType.easeInOutExpo).setOnComplete(() =>
        {
            SceneManager.LoadScene("Menu");
            Time.timeScale = 1f;
        });
    }

}
