using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChapterFourDialogue : MonoBehaviour
{
    public Animator myAnimator;
    public DialogueManager dialogueManager;
    public ChapterFourMovement bushInteraction;
    public MSAudioManager audioManager;

    public GameObject startButton;
    public GameObject nextButton;
    public GameObject dialogueBox;

    private int sqnNum;
    private int dialogueIndex;
    private bool canContinue;
    private bool isMoving;
    private bool pausingStory = false;
    private bool firstTimeAppear = true;

    private float speed;
    public float walkSpeed;
    public bool stopShowingNextButton = false;

    public GameObject pickBushInterface;
    public GameObject fox;
    public GameObject[] walkSpot;

    private Vector3 targetSpot;

    public Dialogue[] dialogue;

    // Start is called before the first frame update
    void Start()
    {
        ResetChapter();
    }

    // Update is called once per frame
    void Update()
    {
        if (isMoving)
        {
            if (Vector3.Distance(fox.transform.localPosition, targetSpot) <= 0.1f)
            {
                resetAnimator();
                isMoving = false;
                canContinue = true;
            }

            fox.transform.localPosition = Vector3.MoveTowards(fox.transform.localPosition, targetSpot, Time.deltaTime * speed);
            fox.transform.localRotation = Quaternion.LookRotation(targetSpot);
        }

        if (canContinue && dialogue[dialogueIndex - 1].finished && !stopShowingNextButton)
        {
            nextButton.SetActive(true);
        }

    }

    public void PlaySequence(int sequence)
    {
        switch (sequence)
        {
            case 0:
                Debug.Log("sqn1");
                dialogueManager.StartDialogue(dialogue[dialogueIndex]);              
                sqnNum++;
                dialogueIndex++;
                break;
            case 1:
                Debug.Log("sqn2");
                dialogueManager.StartDialogue(dialogue[dialogueIndex]);
                myAnimator.SetBool("stand", true);
                sqnNum++;
                dialogueIndex++;
                break;
            case 2:
                Debug.Log("sqn3");
                dialogueManager.StartDialogue(dialogue[dialogueIndex]);
                targetSpot = walkSpot[0].transform.localPosition;
                myAnimator.SetBool("walk", true);
                speed = walkSpeed;
                canContinue = false;
                isMoving = true;
                sqnNum++;
                dialogueIndex++;
                break;
            case 3:
                Debug.Log("sqn4");
                //start minigame
                pickBushInterface.SetActive(true);
                bushInteraction.enabled = true;
                dialogueBox.GetComponent<UITween>().PopOut();
                //sqnNum++;
                //dialogueIndex++;
                break;
            case 4:
                Debug.Log("sqn5");
                dialogueBox.SetActive(true);
                dialogueManager.StartDialogue(dialogue[dialogueIndex]);
                pickBushInterface.SetActive(false);
                bushInteraction.enabled = false;
                myAnimator.SetBool("jump", true);
                //sqnNum++;
                //dialogueIndex++;
                break;
        }
    }

    public void startSequence()
    {
        Debug.Log(sqnNum);
        PlaySequence(sqnNum);
        nextButton.SetActive(false);
        startButton.SetActive(false);
        canContinue = true;
    }

    public void incrementSequence()
    {
        if (canContinue && dialogue[dialogueIndex - 1].finished)
        {
            Debug.Log(sqnNum);
            nextButton.SetActive(false);
            PlaySequence(sqnNum);           
        }
    }

    public void resetAnimator()
    {
        myAnimator.SetBool("walk", false);
    }

    //in case need to reset
    public void ResetChapter()
    {
        sqnNum = 0;
        dialogueIndex = 0;
        isMoving = false;
        pickBushInterface.SetActive(false);
        bushInteraction.enabled = false;
        startButton.SetActive(true);
        resetAnimator();
    }

    public void PauseStory()
    {
        pausingStory = true;
        Time.timeScale = 0f;
        dialogueManager.PauseCurrentNarration();
    }

    public void PlayStory()
    {
        pausingStory = false;
        Time.timeScale = 1f;
        dialogueManager.UnPauseCurrentNarration();
    }

    public void onTrackLostStopStory()
    {
        Time.timeScale = 0f;
        dialogueManager.PauseCurrentNarration();
        audioManager.PauseMusic();
        audioManager.PauseAllSFX();
    }

    public void onTrackFoundPlayStory()
    {
        if (firstTimeAppear)
        {
            audioManager.PlayMusic();
            firstTimeAppear = false;
        }

        if (!pausingStory)
        {
            Time.timeScale = 1f;
            dialogueManager.UnPauseCurrentNarration();
        }
        else if (pausingStory)
        {
            Time.timeScale = 0f;
        }
        audioManager.UnPauseMusic();
        audioManager.UnPauseAllSFX();
    }
}
