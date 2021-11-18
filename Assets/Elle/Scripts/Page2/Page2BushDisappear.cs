using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Page2BushDisappear : MonoBehaviour
{
    public GameObject[] berries;
    public Animator foxAnim;

    public void EatBerries()
    {
        StartCoroutine(RandomDisappearBerry());
    }

    IEnumerator RandomDisappearBerry()
    {
        for (int i = 0; i < berries.Length; i++)
        {
            berries[i].GetComponent<UITween>().PopOut();
            yield return new WaitForSeconds(0.3f);
        }

        foxAnim.SetBool("eat", false);
    }
}
