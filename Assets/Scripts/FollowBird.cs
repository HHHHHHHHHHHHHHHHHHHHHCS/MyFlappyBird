using UnityEngine;
using System.Collections;

public class FollowBird : MonoBehaviour 
{
    private Transform bird;
    private Vector3 offest;

    void Start()
    {
        bird = GameObject.FindGameObjectWithTag("Player").transform;
        offest = transform.position - bird.position;
    }

    void Update()
    {
        Vector3 newPos = transform.position;
        newPos.x = bird.position.x + offest.x;
        //newPos.y = Mathf.Clamp(bird.position.y + offest.y, -2.4f, 2.4f);
        transform.position = newPos;
    }
}
