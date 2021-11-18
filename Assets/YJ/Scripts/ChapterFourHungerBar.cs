using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ChapterFourHungerBar : MonoBehaviour
{
    public static ChapterFourHungerBar instance;

    public ChapterFourDialogue chapterFourDialogue;
    public ChapterFourMovement bushInteraction;
    public MSAudioManager audioManager;

    public float currentHunger;
    public float initialHunger = 0;
    public float maxHunger = 100;
    public Image ImgHungerBar;


    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
        }
    }

    // Start is called before the first frame update
    void Start()
    {
        resetHunger();
        UpdateHungerBar();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void EatBerries(float berryvalue)
    {
        Debug.Log("berry give: " + berryvalue);

        currentHunger += berryvalue;

        UpdateHungerBar();

        if(currentHunger >= maxHunger)
        {
            //win
            audioManager.PlaySFX(2);
            bushInteraction.ZoomBack();

            StartCoroutine(Satisfied());
        }
    }

    IEnumerator Satisfied()
    {
        yield return new WaitForSeconds(2f);
        chapterFourDialogue.stopShowingNextButton = true;
        chapterFourDialogue.nextButton.SetActive(false);
        chapterFourDialogue.PlaySequence(4);
    }

    public void resetHunger()
    {
        currentHunger = initialHunger;
    }


    void UpdateHungerBar()
    {
        float mCurrentPercent = currentHunger / maxHunger;

        if (ImgHungerBar != null)
        {
            ImgHungerBar.fillAmount = mCurrentPercent;
        }
    }
}
