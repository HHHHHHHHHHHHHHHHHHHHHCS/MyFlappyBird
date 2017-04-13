using UnityEngine;
using System.Collections;

public class MoveTrigger : MonoBehaviour 
{
    private float x_offest = 10;
    private Transform father;
    private Pipe pipe1;
    private Pipe pipe2;

    void Awake()
    {
        father = transform.parent;
        pipe1 = transform.parent.Find("pipe1").GetComponent<Pipe>();
        pipe2 = transform.parent.Find("pipe2").GetComponent<Pipe>();
    }


    void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Player") 
        {
            //move the bg to the front of first transform
            //1. get the first transform
            Transform endBg = GameManager._instance.endBg;
            //2. move 
            father.position = new Vector3(endBg.position.x + x_offest, endBg.position.y, endBg.position.z);

            GameManager._instance.endBg = father;

            pipe1.RandomGeneratePosition();
            pipe2.RandomGeneratePosition();
        }
    }
}
