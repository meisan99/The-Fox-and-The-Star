using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DialogueManager : MonoBehaviour
{
    public GameObject DialogueBox;
    public Text dialogueText;
    public Dialogue currentDialogue;

    private Queue<string> sentences;

    //The lower the value, the faster the speed
    private float dialogueSpeed = 0.05f;

    private float dialogueDelayTime = 1.0f;

    private void Awake()
    {
        sentences = new Queue<string>();
    }

    public void StartDialogue (Dialogue dialogue)
    {
        currentDialogue = dialogue;

        DialogueBox.SetActive(true);

        sentences.Clear();

        foreach (string sentence in dialogue.sentences)
        {
            sentences.Enqueue(sentence);
        }

        //the specific dialogue is typing
        dialogue.finished = false;

        DisplayNextSentence(dialogue);
    }

    public void DisplayNextSentence(Dialogue dialogue)
    {
        if(sentences.Count == 0)
        {
            EndDialogue();
            return;
        }

        string sentence = sentences.Dequeue();
        StopAllCoroutines();
        StartCoroutine(WaitForAudio(sentence, dialogue));
    }

    IEnumerator WaitForAudio(string sentence, Dialogue dialogue)
    {
        dialogueText.text = sentence;

        if(dialogue.narrationIndex <= dialogue.narrationAudios.Length - 1)
        {
            if (dialogue.narrationAudios[dialogue.narrationIndex] != null)
            {
                dialogue.narrationAudios[dialogue.narrationIndex].Play();

                while (dialogue.narrationAudios[dialogue.narrationIndex].isPlaying || dialogue.narrationPause)
                {
                    yield return null;
                }
            }
        }

        yield return new WaitForSeconds(0.8f);

        dialogue.narrationIndex++;

        /*check whether there are remaining sentences, 
         * if there is no remaining sentence, change to next one
         * if there are remaining, display the next sentence
         */
        if (sentences.Count == 0)
        {
            //the specific dialogue has finished
            dialogue.finished = true;
        }
        else
        {
            DisplayNextSentence(dialogue);
        }
    }

    public void PauseCurrentNarration()
    {
        if(currentDialogue != null)
        {
            if (currentDialogue.narrationIndex <= currentDialogue.narrationAudios.Length - 1)
            {
                currentDialogue.narrationPause = true;
                currentDialogue.narrationAudios[currentDialogue.narrationIndex].Pause();
            }
                
        }
        
    }

    public void UnPauseCurrentNarration()
    {
        if(currentDialogue != null)
        {
            if (currentDialogue.narrationIndex <= currentDialogue.narrationAudios.Length - 1)
            {
                currentDialogue.narrationPause = false;
                currentDialogue.narrationAudios[currentDialogue.narrationIndex].UnPause();
            }
                
        }
        
    }

    //IEnumerator TypeSentence (string sentence, Dialogue dialogue)
    //{
    //    dialogueText.text = "";
    //    foreach (char letter in sentence.ToCharArray())
    //    {
    //        dialogueText.text += letter;
    //        yield return new WaitForSeconds(dialogueSpeed);
    //    }

    //    //a short delay after the sentence has finished
    //    yield return new WaitForSeconds(dialogueDelayTime);

    //    /*check whether there are remaining sentences, 
    //     * if there is no remaining sentence, change to next one
    //     * if there are remaining, display the next sentence
    //     */
    //    if (sentences.Count == 0)
    //    {
    //        //the specific dialogue has finished
    //        dialogue.finished = true;
    //        runningDialogue = false;
    //    }
    //    else
    //    {
    //        DisplayNextSentence(dialogue);
    //    }
    //}

    public void EndDialogue()
    {
        DialogueBox.SetActive(false);
    }
}
