using UnityEngine;
using System.Collections;

public class BackOrPipe : MonoBehaviour 
{
    public AudioClip hitClip;
    public AudioClip dieClip;
    private AudioSource audioSoure;

    void Awake()
    {
        audioSoure = GameObject.FindWithTag("Player").GetComponent<AudioSource>();
    }

    void OnCollisionEnter(Collision other)
    {
        if (other.gameObject.tag == "Player")
        {
            audioSoure.PlayOneShot(hitClip);
            audioSoure.PlayOneShot(dieClip);
            GameManager._instance.GameState = GAMESTATE.end;
        }
    }
}
