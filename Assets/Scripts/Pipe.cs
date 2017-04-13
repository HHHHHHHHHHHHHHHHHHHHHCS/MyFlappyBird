using UnityEngine;
using System.Collections;


public class Pipe : MonoBehaviour 
{
    private AudioSource audioSource;


    void Awake()
    {
        audioSource = GetComponent<AudioSource>();
    }

    void Start()
    {
        RandomGeneratePosition();
    }


    public void RandomGeneratePosition()
    {
        //how to random a number
        float pos_y = Random.Range(-0.1f, 0.15f);
        transform.localPosition = new Vector3(transform.localPosition.x, pos_y, transform.localPosition.z);
    }

    void OnTriggerExit(Collider other)
    {//OnTriggerEnteer OnTriggerStay OnTriggerExit
        if(other.tag=="Player"&&GameManager._instance.GameState==GAMESTATE.playing)
        {
            //plus score
            audioSource.Play();
            GameManager._instance.Score++;
            GameManager._instance.UpdateScoreText();
        }
    }
    
}
