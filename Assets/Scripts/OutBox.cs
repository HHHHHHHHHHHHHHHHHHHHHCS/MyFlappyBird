using UnityEngine;
using System.Collections;

public class OutBox : MonoBehaviour 
{
    public AudioClip hitClip;
    public AudioClip dieClip;
    private AudioSource audioSoure;

    void Awake()
    {
        audioSoure = GameObject.FindWithTag("Player").GetComponent<AudioSource>();
    }

    void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "Player")
        {
            audioSoure.PlayOneShot(hitClip);
            audioSoure.PlayOneShot(dieClip);
            GameManager._instance.GameState = GAMESTATE.end;
        }
    }
}
