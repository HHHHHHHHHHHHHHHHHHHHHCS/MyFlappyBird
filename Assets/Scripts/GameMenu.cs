using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class GameMenu : MonoBehaviour 
{
    public static GameMenu _instance;
    private Text nowText;
    private Text highText;
    

    void Awake()
    {
        _instance = this;
        gameObject.SetActive(false);
        nowText = transform.Find("NowScore").GetComponent<Text>();
        highText = transform.Find("HighScore").GetComponent<Text>();

    }

    public void UpdateScore(int score)
    {
        nowText.text = score.ToString();
        int highScore = JSONManager.GetHighSocre(); //XMLManager.GetHighSocre();
        if (score>highScore)
        {//刷新记录
            highScore=score;
            JSONManager.UpdateHighSocre(score); //XMLManager.SetHighScore(score);
            
        }
        highText.text = highScore.ToString();
        
    }

    public void ReStart()
    {
        if(GameManager._instance.GameState==GAMESTATE.end)
        {

            Application.LoadLevel(Application.loadedLevelName);
        }
    }
}
