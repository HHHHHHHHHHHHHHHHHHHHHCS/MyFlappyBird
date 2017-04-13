using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public enum GAMESTATE
{
    menu = 0,//游戏菜单状态
    playing = 1,//游戏中状态
    end = 3//游戏结束状态
}

public class GameManager : MonoBehaviour 
{
    //define the state of the game


    public static GameManager _instance;
    public Transform endBg;
    private int _score=0;
    private GAMESTATE _gameState = GAMESTATE.menu;
    private GameObject bird;
    private bool havedDieVel = false;//是否归零了死亡速度 

    public GAMESTATE GameState
    {
        get
        {
            return _gameState;
        }
        set
        {
            _gameState = value;
        }
    }

    public int Score
    {
        get{return _score;}
        set{ _score = value;}
    }

    void Awake()
    {
        _instance = this;
        bird = GameObject.FindGameObjectWithTag("Player");
    }

    void Update()
    {
        if (GameState == GAMESTATE.menu)
        {
            if(Input.GetMouseButtonDown(0))
            {
                //transform state 
                GameState = GAMESTATE.playing;
                //set bird is playing
                //1.set gravity 2.add velocity of x
                bird.GetComponent<Bird>().GetLife();
            }
        }
        else if (GameState == GAMESTATE.end)
        {
            if(!havedDieVel)
            {
                bird.GetComponent<Bird>().LoseLife();
                havedDieVel = true;
            }
            
            if (GameMenu._instance.gameObject.activeSelf == false)
            {
                GameMenu._instance.UpdateScore(Score);
                GameMenu._instance.gameObject.SetActive(true);
            }
           
        }
    }

    public void UpdateScoreText()
    {
        NowScore._instance.UpdateScore(Score);
    }
}
