using UnityEngine;
using System.IO;
using System.Xml;
using System;
using System.Text;
using System.Collections;
using UnityEngine.UI;

public class XMLManager : MonoBehaviour 
{
    public static Text t;
    private static bool isFirst = true;
    private const string _xmlName = "GameData.xml";
    private static string _savePath;

    static void Init()
    {
        if (isFirst)
        {
            isFirst = false;
            if (Application.platform == RuntimePlatform.Android)
            {
                _savePath = Application.persistentDataPath + "/" + XMLName;//Android环境下的文件路径
            }
            else if (Application.platform == RuntimePlatform.IPhonePlayer)
            {

            }
            else
            {
                _savePath = Application.dataPath + "/" + XMLName;//PC
            }
        }
    }


    private static string XMLName
    {
        get
        {
            if (isFirst)
            {
                Init();
            }
            return _xmlName;
        }
    }
    private static string SavePath
    {
        get
        {
            if (isFirst)
            {
                Init();
            }
            return _savePath;
        }
    }

    public static void SetHighScore(int highScore)
    {
        XmlDocument xmlDoc = new XmlDocument();
        CreatXML();
        //xmlDoc.LoadXml(TextManager.LoadFile(SavePath, XMLName));
        xmlDoc.Load(SavePath);
        XmlNodeList nodeList = xmlDoc.SelectSingleNode("data/user").ChildNodes;

        foreach (XmlElement xe in nodeList)
        {
            if (xe.Name == "highScore")
            {

                xe.InnerText = highScore.ToString();
                break;
            }
        }
        //TextManager.UpdateFile(SavePath, XMLName, xmlDoc.InnerXml);
        xmlDoc.Save(SavePath);
    }


    public static int GetHighSocre()
    {
        int highScore = 0;
        XmlDocument xmlDoc = new XmlDocument();
        CreatXML();
        //xmlDoc.LoadXml(TextManager.LoadFile(SavePath,XMLName));
        xmlDoc.Load(SavePath);
        XmlNodeList nodeList = xmlDoc.SelectSingleNode("data/user").ChildNodes;
        foreach (XmlElement xe in nodeList)
        {
            if (xe.Name == "highScore")
            {
                highScore = int.Parse(xe.InnerText);
                break;
            }
        }
        return highScore;
    }

    private static void CreatXML()
    {
        //检测xml是否存在
        if(File.Exists(SavePath))
        {
            return;
        }
        //新建xml实例
        XmlDocument xmlDoc = new XmlDocument();
        //创建根节点，最上层节点
        XmlElement data = xmlDoc.CreateElement("data");
        xmlDoc.AppendChild(data);
        //二级节点
        XmlElement user = xmlDoc.CreateElement("user");
        data.AppendChild(user);
        //二级节点的属性
        XmlElement highScore = xmlDoc.CreateElement("highScore");
        highScore.InnerText = "0";
        user.AppendChild(highScore);
        xmlDoc.Save(SavePath);
    }
}