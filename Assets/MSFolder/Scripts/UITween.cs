using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UITween : MonoBehaviour
{
    public LeanTweenType easeType = LeanTweenType.easeOutElastic ;

    public enum TweenType // your custom enumeration
    {
        Pop
    };
    public TweenType tweenType = TweenType.Pop;

    private Vector3 oriScale;

    private void Awake()
    {
        oriScale = gameObject.transform.localScale;
    }

    private void OnEnable()
    {
        if(tweenType == TweenType.Pop)
        {
            PopIn();
        }
    }

    void PopIn()
    {
        gameObject.transform.localScale = new Vector3(0.1f, 0.1f, 0.1f);
        gameObject.SetActive(true);
        LeanTween.scale(gameObject, oriScale, 0.5f).setEase(easeType);
    }

    public void PopOut()
    {
        LeanTween.scale(gameObject, new Vector3(0, 0, 0), 0.5f).setEase(easeType).setOnComplete(Close);
    }

    void Close()
    {
        gameObject.SetActive(false);
    }
}
