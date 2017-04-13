using UnityEngine;
using System.IO;
using System.Xml;
using System.Collections;
using UnityEngine.UI;

public class _XMLManager
{
    private static bool isFirst = true;
    private const string xmlName = "/GameData.xml";
    private static string _xmlPath;

    private static XmlDocument myXmlDoc = new XmlDocument();

    static void Init()
    {
        if (isFirst)
        {
            isFirst = false;
            if (Application.platform == RuntimePlatform.Android)
            {
                _xmlPath = Application.streamingAssetsPath + "/" + xmlName;//Android环境下的文件路径
            }
            else if (Application.platform == RuntimePlatform.IPhonePlayer)
            {

            }
            else
            {
                _xmlPath = Application.dataPath + "/" + xmlName;//PC
            }
        }
    }


    private static string XMLPath
    {
        get
        {
            if (isFirst)
            {
                Init();
            }
            return _xmlPath;
        }
    }




    public static void CreatXML()
    {
        //检测xml是否存在
        if (!File.Exists(XMLPath))
        {
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

            //将xml文件保存到本地
            xmlDoc.Save(XMLPath);
        }
    }

    private static WWW Getwww()
    {
        CreatXML();
        string localPath;
        if (Application.platform == RuntimePlatform.Android)
        {
            localPath = XMLPath; //在Android中实例化WWW不能在路径前面加"file://"
        }
        else
        {
            localPath = "file://" + XMLPath;//在Windows中实例化WWW必须要在路径前面加"file://"
        }
        WWW www = new WWW(localPath);
        return www;
    }

    public static IEnumerator GetXML()
    {
        WWW www = Getwww();

        while (!www.isDone)
        {
            yield return www;

            myXmlDoc.LoadXml(www.text);
        }
    }

    public static void SetHighScore(int highScore)
    {
        if (!File.Exists(XMLPath))
        {
            CreatXML();
        }
        XmlDocument xmlDoc = new XmlDocument();
        xmlDoc.Load(XMLPath);
        XmlNodeList nodeList = xmlDoc.SelectSingleNode("data/user").ChildNodes;
        foreach (XmlElement xe in nodeList)
        {
            if (xe.Name == "highScore")
            {
                xe.InnerText = highScore.ToString();
                break;
            }
        }
        xmlDoc.Save(XMLPath);
    }


    public static int GetHighSocre()
    {
        int highScore = 0;
        if (!File.Exists(XMLPath))
        {
            CreatXML();
        }
        XmlDocument xmlDoc = new XmlDocument();
        xmlDoc.Load(XMLPath);
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
}