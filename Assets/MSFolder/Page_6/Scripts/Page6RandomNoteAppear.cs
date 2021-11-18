using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Page6RandomNoteAppear : MonoBehaviour
{
    public MSAudioManager audioManager;
    public GameObject[] notes;

    public void SpawnNotes()
    {
        StartCoroutine(RandomSpawnNotes());
    }

    IEnumerator RandomSpawnNotes()
    {
        for (int i = 0; i < notes.Length; i++)
        {
            notes[i].SetActive(true);
            yield return new WaitForSeconds(0.5f);
        }
    }
}
    
