using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class NowScore : MonoBehaviour 
{
    public static NowScore _instance;
    private Text socreText;
	// Use this for initialization
	void Awake () 
    {
        _instance = this;
        socreText = transform.Find("NowScore").GetComponent<Text>();
	}
	
	// Update is called once per frame
	public void UpdateScore (int score) 
    {
        socreText.text = score.ToString();
	}
}
