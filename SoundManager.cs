using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoundManager : MonoBehaviour
{
    [SerializeField] List<AudioClip> backingTracks;
    [SerializeField] AudioClip backingTrack;
    [SerializeField] AudioClip answerSfx;
    
    // Start is called before the first frame update
    void Start()
    {
       PlayTrack();
    }

    void PlayTrack()
    {
        int index = Random.Range(0, backingTracks.Count);
        backingTrack = backingTracks[index];

        GameObject soundGameObject = new GameObject("Sound");
        AudioSource audioSource = soundGameObject.AddComponent<AudioSource>();
        audioSource.PlayOneShot(backingTrack); 
    }

    public void PlayAnswerSound()
    {
        GameObject soundGameObject = new GameObject("Sound");
        AudioSource audioSource = soundGameObject.AddComponent<AudioSource>();
        audioSource.PlayOneShot(answerSfx);
    }

}
