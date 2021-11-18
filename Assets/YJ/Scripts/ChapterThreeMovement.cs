using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ChapterThreeMovement : MonoBehaviour
{
    public MSAudioManager audioManager;
    public Animator myAnimator;
    public DialogueManager dialogueManager;

    private int sqnNum;
    private int dialogueIndex;
    private bool canContinue = false;
    private bool isMoving;
    private bool pausingStory = false;
    private bool firstTimeAppear = true;

    private float speed;
    public float walkSpeed;
    public float runSpeed;

    public GameObject nextButton;
    public GameObject dialogueBox;
    public GameObject startButton;
    public GameObject orbitDynamic;
    public GameObject orbitStatic;
    public GameObject fox;
    public GameObject denFront;
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
        if(isMoving)
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

        if (canContinue && dialogue[dialogueIndex - 1].finished)
        {
            nextButton.SetActive(true);
        }

    }

    public void PlaySequence(int sequence)
    {
        switch(sequence)
        {
            case 0:
                Debug.Log("sqn1");
                dialogueManager.StartDialogue(dialogue[dialogueIndex]);
                targetSpot = walkSpot[0].transform.localPosition;
                myAnimator.SetBool("walk", true);
                speed = walkSpeed;
                isMoving = true;
                canContinue = false;
                sqnNum++;
                dialogueIndex++;
                break;
            case 1:
                Debug.Log("sqn2");
                dialogueManager.StartDialogue(dialogue[dialogueIndex]);
                myAnimator.SetBool("call", true);
                sqnNum++;
                dialogueIndex++;
                break;
            case 2:
                Debug.Log("sqn3");
                dialogueManager.StartDialogue(dialogue[dialogueIndex]);
                targetSpot = walkSpot[1].transform.localPosition;
                myAnimator.SetBool("call", false);
                myAnimator.SetBool("run", true);
                speed = runSpeed;
                isMoving = true;
                canContinue = false;
                sqnNum++;
                dialogueIndex++;
                break;
            case 3:
                Debug.Log("sqn4");
                orbitDynamic.SetActive(true);
                orbitStatic.SetActive(false);
                canContinue = false;
                dialogueManager.StartDialogue(dialogue[dialogueIndex]);
                fox.transform.localRotation = denFront.gameObject.transform.localRotation;
                myAnimator.SetBool("sit", true);       
                break;
        }
    }

    public void startSequence()
    {
        Debug.Log(sqnNum);
        PlaySequence(sqnNum);
        nextButton.SetActive(false);
        startButton.SetActive(false);
        canContinue = false;       
    }

    public void incrementSequence()
    {
        //Debug.Log(canContinue);
        //Debug.Log(dialogueIndex - 1);
        if(canContinue && dialogue[dialogueIndex-1].finished)
        {
            Debug.Log(sqnNum);
            PlaySequence(sqnNum);
            nextButton.SetActive(false);
        }        
    }

    public void resetAnimator()
    {
        myAnimator.SetBool("walk", false);
        myAnimator.SetBool("run", false);       
    }

    //in case need to reset
    public void ResetChapter()
    {
        sqnNum = 0;
        dialogueIndex = 0;
        isMoving = false;
        startButton.SetActive(true);
        orbitDynamic.SetActive(false);
        orbitStatic.SetActive(true);
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
