using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayAudio : MonoBehaviour {
    AudioSource aud;
    void Start() //on start, get audio source object and play it
    {
        aud = GetComponent<AudioSource>();

        playAudio();
    }

    private IEnumerator playAndDestroy() // play and wait until playing ends to destroy game object to reduce lags
    {
        aud.Play();
        while (aud.isPlaying)
        {
            yield return new WaitForSeconds(1);
            Debug.Log("PLAYING TRACK");
        }
        Destroy(gameObject); 
    }
    
    void playAudio() //run the IEnumerator Coroutine.
    {
        StartCoroutine(playAndDestroy());
    }
}
