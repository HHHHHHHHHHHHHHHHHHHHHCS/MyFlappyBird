using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System.Xml;
using System.IO;
using System.Text;
using LitJson;

public class JSONManager
{
    private const string HIGHSCORE = "highScore";
    private const string _jsonName = "GameData.xml";
    private static string _savePath;
    private static bool isFirst = true;

    static void Init()
    {
        if (isFirst)
        {
            isFirst = false;
            if (Application.platform == RuntimePlatform.Android)
            {
                _savePath = Application.persistentDataPath + "/" + _jsonName;//Android环境下的文件路径
            }
            else if (Application.platform == RuntimePlatform.IPhonePlayer)
            {

            }
            else
            {
                _savePath = Application.dataPath + "/" + _jsonName;//PC
            }
        }
    }

    private static string SavePath
    {
        get
        {
            Init();
            return _savePath;
        }
    }


    private const string sample = @"{
      ""highScore""  : 0
      }";


    public static void UpdateHighSocre(int newScore)
    {
        CreatJson();
        StreamReader sr = File.OpenText(SavePath);
        string strLine = sr.ReadToEnd();
        JsonData jd = JsonMapper.ToObject(strLine);
        jd["highScore"] = newScore;



        strLine = jd.ToJson();
        sr.Close();
        sr.Dispose();


        UpdateFile(strLine);
    }

    public static int GetHighSocre()
    {
        CreatJson();
        StreamReader sr = File.OpenText(SavePath);
        string strLine = sr.ReadToEnd();
        JsonData jd = JsonMapper.ToObject(strLine);
        int highScore = (int)jd["highScore"];
        sr.Close();
        sr.Dispose();
        return highScore;
    }

    private static void CreatJson()
    {
        //检测xml是否存在
        if (File.Exists(SavePath))
        {
            return;
        }

        UpdateFile(sample);
    }


    /**
    * path：文件创建目录
    * name：文件的名称
    *  info：写入的内容
    */
    private static void UpdateFile(string info)
    {
        //文件流信息
        StreamWriter sw;
        FileInfo t = new FileInfo(SavePath);

        //如果此文件不存在则创建
        sw = t.CreateText();

        //写入信息
        sw.Write(info);

        //关闭流
        sw.Close();

        //销毁流
        sw.Dispose();


    }


}
