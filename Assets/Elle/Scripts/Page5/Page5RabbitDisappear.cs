using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Page5RabbitDisappear : MonoBehaviour
{
    public GameObject particleEffect;
    public GameObject[] rabbits;

    public void RabbitRunAway()
    {
        StartCoroutine(RunAway());
    }

    IEnumerator RunAway()
    {
        for (int i = 0; i < rabbits.Length; i++)
        {
            Instantiate(particleEffect, rabbits[i].transform);
            rabbits[i].GetComponent<UITween>().PopOut();
            yield return new WaitForSeconds(0.3f);
        }
    }
}
