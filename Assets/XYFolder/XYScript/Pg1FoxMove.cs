using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Pg1FoxMove : MonoBehaviour
{
    float speed = 0.1f;
    int num = 0;
    private int dialogueIndex = 0;
    int sequence = 0;

    bool walk = false;
    bool start = false;
    bool moving = false;
    private bool firstTimeAppear = true;
    private bool pausingStory = false;
    bool animReady = true;
    private bool isPlayingSequence = false;

    public Animator foxAnim;
    public DialogueManager dialogueManager;
    public MSAudioManager audioManager;

    public GameObject[] targets;
    public GameObject dialogueBox;
    public Dialogue[] dialogue;
    public GameObject star;
    public GameObject nextButton;
    public GameObject foxParticle;
    public GameObject starParticle;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (Pg1TrackableEventHandler.instance.PageIsActive)
        {
            if (moving)
            {
                if (Vector3.Distance(transform.localPosition, targets[num].transform.localPosition) < 0.01f)
                {
                    moving = false;
                    animReady = true;
                    if(sequence == 2)
                    {
                        foxAnim.SetTrigger("idle");
                    }
                    if(sequence == 3)
                    {
                        foxAnim.SetTrigger("jump");
                        //transform.LookAt(star.transform.localPosition);
                    }
                }

                float step = speed * Time.deltaTime;
                transform.localPosition = Vector3.MoveTowards(transform.localPosition, targets[num].transform.localPosition, step);
                //transform.LookAt(targets[num].transform.localPosition);
            }

            if (isPlayingSequence)
            {
                if (animReady && dialogue[dialogueIndex].finished)
                {
                    isPlayingSequence = false;
                    if (sequence <= 3)
                    {
                        nextButton.SetActive(true);
                    }
                }
            }
        }
    }

    public void RunStory()
    {
        Debug.Log("run story");
        if(sequence == 0)
        {
            isPlayingSequence = true;
            moving = false;
            dialogueManager.StartDialogue(dialogue[dialogueIndex]);
            sequence++;

        }
        else if(sequence == 1)
        {
            isPlayingSequence = true;
            num = 0;
            foxAnim.SetTrigger("stand");
            moving = true;
            dialogueIndex++;
            dialogueManager.StartDialogue(dialogue[dialogueIndex]);
            sequence++;
            speed = 1.0f;
        }
        else if(sequence == 2)
        {
            isPlayingSequence = true;
            star.SetActive(true);
            num = 1;
            foxAnim.SetTrigger("run");
            moving = true;
            dialogueIndex++;
            dialogueManager.StartDialogue(dialogue[dialogueIndex]);
            sequence++;
            speed = 1.5f;
        }
        else if(sequence == 3)
        {
            foxParticle.SetActive(true);
            starParticle.SetActive(true);
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
