using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Tweener : MonoBehaviour
{
    public GameObject _start_title;
    // Start is called before the first frame update
    void Start()
    {
        LeanTween.scale(_start_title, new Vector3(1.2f, 1.2f, 1.2f), 1.4f).setEase(LeanTweenType.easeInOutSine).setLoopPingPong();
        LeanTween.rotate(_start_title, new Vector3(1.2f, 1.2f, 1.2f), 1.4f).setEase(LeanTweenType.easeInOutSine).setLoopPingPong();

    }

    // Update is called once per frame
    void Update()
    {
        //LeanTween.size
    }
}
