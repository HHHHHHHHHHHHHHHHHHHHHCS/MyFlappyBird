using UnityEngine;
using System.Collections;

public class Bird : MonoBehaviour
{

    public int frameNumber = 10;//frame number on seconds
    public int frameCount = 0;//frame counter
    public int framePhotoNumber = 3;//frame number of bird photo

    private float time;
    private float timeNumber = 0;
    private float timer;
    private Renderer myRenderer;
    private Rigidbody rigi;
    private AudioSource audioSource;

    void Awake()
    {
        myRenderer = GetComponent<Renderer>();
        rigi = GetComponent<Rigidbody>();
        audioSource = GetComponent<AudioSource>();

    }

    void Update()
    {


        //animation
        if (GameManager._instance.GameState==GAMESTATE.playing)
        {
            if (timeNumber != frameNumber)
            {
                timeNumber = frameNumber;
                time = 1.0f / timeNumber;
            }
            timer += Time.deltaTime;
            if (timer >= time)
            {
                frameCount++;
                timer -= time;

                int frameIndex = frameCount % framePhotoNumber;
                //update material 's offest x, _MainTex  :   Main Texture
                myRenderer.material.SetTextureOffset("_MainTex", new Vector2(0.33333f * frameIndex, 0));
            }
            //control jump
            if (GameManager._instance.GameState == GAMESTATE.playing)
            {
                if (Input.GetMouseButtonDown(0))
                {//left mouse button down
                    Vector3 vel = rigi.velocity;
                    rigi.velocity = new Vector3(vel.x, 5, vel.z);
                    audioSource.Play();
                }
            }
        }
    }


    public void GetLife()
    {
        rigi.useGravity = true;
        rigi.velocity = new Vector3(3, 0, 0);           
    }

    public void LoseLife()
    {
        rigi.constraints = RigidbodyConstraints.FreezePositionX | rigi.constraints;//| RigidbodyConstraints.FreezeRotationZ;
    }
}
