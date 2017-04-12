using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayAudio : MonoBehaviour {
    AudioSource aud;
    void Start() //on start, get audio source object.
    {
        aud = GetComponent<AudioSource>();

        playAudio();
    }

    private IEnumerator playAndDestroy() // play and wait until playing ends to destroy game object
    {
        aud.Play();
        while (aud.isPlaying)
        {
            yield return new WaitForSeconds(1);
            Debug.Log("PLAYING TRACK");
        }
        Destroy(gameObject); //there might be a better way to implement to do this method.
    }
    
    void playAudio() //run the IEnumerator Coroutine.
    {
        StartCoroutine(playAndDestroy());
    }
}
