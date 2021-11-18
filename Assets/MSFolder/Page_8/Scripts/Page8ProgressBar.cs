using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Page8ProgressBar : MonoBehaviour
{
    private Slider slider;

    public Gradient gradient;
    public float fillSpeed = 0.5f;
    private float targetProgress = 0;
    

    private void Start()
    {
        slider = gameObject.GetComponent<Slider>();
        slider.value = 0f;
    }

    private void Update()
    {
        slider.value = targetProgress;
    }

    //public void SetMaxValue(float energy)
    //{
    //    slider.maxValue = energy;
    //    slider.value = energy;

    //    fill.color = gradient.Evaluate(0f);
    //}

    public void UpdateProgress(float progress)
    {
        targetProgress = progress;
        if(targetProgress > 1.0f)
        {
            targetProgress = 1.0f;
        }
    }

    public void ClearProgress()
    {
        slider.value = 0f;
        targetProgress = 0f;
    }
}
