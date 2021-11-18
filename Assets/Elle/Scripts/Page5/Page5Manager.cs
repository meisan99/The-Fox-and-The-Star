using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Page5Manager : MonoBehaviour
{
    public MSAudioManager audioManager;
    public DialogueManager dialogueManager;
    public Page5RabbitDisappear rabbitDisappear;

    public Transform firstPoint;
    public Transform secondPoint;
    public Transform thirdPoint;

    public GameObject fox;
    public GameObject dialogueBox;
    public GameObject pressButton1;
    public GameObject pressButton2;
    public GameObject pressButton3;
    public GameObject trickets;
    public GameObject rabbits;
    public GameObject trees;
    public GameObject nextButton;
    public Animator foxAnim;

    private int sequence = 0;
    private int dialogueIndex = 0;

    private float distanceOffset = 0.01f;
    private float speed = 0.07f;
    private float waitingDelayTime = 0.8f;

    private bool appearButton = false;
    private bool animReady = true;
    private bool moving = false;
    private bool isPlayingSequence = false;
    private bool pausingStory = false;
    private bool firstTimeAppear = true;
    private bool waitAWhile = false;

    private Vector3 targetPosition;

    public Dialogue[] dialogue;

    private void Start()
    {
        //starLight.intensity = 0.0f;
    }

    void Update()
    {
        if (Page5TackableEventHandler.instance.PageIsActive)
        {

            if (moving)
            {
                fox.transform.localPosition = Vector3.MoveTowards(fox.transform.localPosition, targetPosition, Time.deltaTime * speed);

                if (Vector3.Distance(fox.transform.localPosition, targetPosition) < distanceOffset)
                {
                    if (sequence == 3) //rabbits
                    {
                        ResetAnimation();
                        foxAnim.SetBool("talk2", true);
                        animReady = true;
                    }

                    if (sequence == 5) //trees
                    {
                        ResetAnimation();
                        animReady = true;
                    }
                }
            }

            if (isPlayingSequence)
            {
                if (animReady && dialogue[dialogueIndex].finished)
                {
                    //if(waitAWhile)
                    //{
                    //    waitAWhile = false;
                    //    isPlayingSequence = false;
                    //    StartCoroutine(WaitforWhile());
                    //}

                    isPlayingSequence = false;
                    if (sequence <= 5)
                    {
                        nextButton.SetActive(true);
                    }
                }
            }
        }
    }

    IEnumerator WaitforWhile()
    {
        yield return new WaitForSeconds(waitingDelayTime);

        if (sequence <= 5)
        {
            nextButton.SetActive(true);
        }
    }

    void ResetAnimation()
    {
        foxAnim.SetBool("jump", false);
        foxAnim.SetBool("run", false);
        foxAnim.SetBool("walk", false);
        foxAnim.SetBool("sit", false);
        foxAnim.SetBool("talk1", false);
        foxAnim.SetBool("talk2", false);
        foxAnim.SetBool("talk3", false);
    }

    public void RunSeuqence()
    {
        //standby
        if (sequence == 0)
        {
            isPlayingSequence = true;

            dialogueManager.StartDialogue(dialogue[dialogueIndex]);

            sequence++;
        }

        else
        {
            //appear button
            if (sequence == 1 && !appearButton)
            {
                dialogueBox.GetComponent<UITween>().PopOut();
                pressButton1.SetActive(true);
                appearButton = true;
            }

            //ask trickets
            else if (sequence == 1 && appearButton)
            {
                isPlayingSequence = true;

                pressButton1.SetActive(false);

                trickets.SetActive(true);
                ResetAnimation();
                foxAnim.SetBool("talk1", true);

                dialogueIndex++;
                dialogueManager.StartDialogue(dialogue[dialogueIndex]);

                sequence++;

                appearButton = false;
            }

            //appear button
            else if (sequence == 2 && !appearButton)
            {
                trickets.GetComponent<UITween>().PopOut();
                dialogueBox.GetComponent<UITween>().PopOut();
                pressButton2.SetActive(true);
                appearButton = true;
            }

            //ask rabbits
            else if (sequence == 2 && appearButton)
            {
                isPlayingSequence = true;

                targetPosition = secondPoint.localPosition;
                moving = true;
                pressButton2.SetActive(false);

                rabbits.SetActive(true);
                ResetAnimation();
                foxAnim.SetBool("walk", true);

                dialogueIndex++;
                dialogueManager.StartDialogue(dialogue[dialogueIndex]);

                sequence++;

                appearButton = false;
            }

            else if (sequence == 3)
            {
                isPlayingSequence = true;

                rabbitDisappear.RabbitRunAway();
                ResetAnimation();
                foxAnim.SetBool("idle", true);

                dialogueIndex++;
                dialogueManager.StartDialogue(dialogue[dialogueIndex]);

                sequence++;

                appearButton = false;
            }

            //appear button
            else if (sequence == 4 && !appearButton)
            {
                dialogueBox.GetComponent<UITween>().PopOut();
                pressButton3.SetActive(true);
                appearButton = true;
            }

            //ask trees
            else if (sequence == 4 && appearButton)
            {
                isPlayingSequence = true;

                targetPosition = thirdPoint.localPosition;
                moving = true;
                pressButton3.SetActive(false);

                trees.SetActive(true);
                ResetAnimation();
                foxAnim.SetBool("walk", true);

                dialogueIndex++;
                dialogueManager.StartDialogue(dialogue[dialogueIndex]);

                sequence++;

                appearButton = false;
            }

            //talk with tree
            else if (sequence == 5)
            {
                isPlayingSequence = true;

                ResetAnimation();
                foxAnim.SetBool("talk3", true);

                dialogueIndex++;
                dialogueManager.StartDialogue(dialogue[dialogueIndex]);

                sequence++;
            }
        }
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
