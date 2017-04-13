using UnityEngine;
using System.Collections;
using System.IO;
using System.Collections.Generic;
using System;
using System.Xml;

public class TextManager
{
    //文本中每行的内容
    ArrayList infoall;

    /*void Start()
    {

        //删除文件
        //DeleteFile(Application.persistentDataPath, "FileName.txt");

        //创建文件，共写入3次数据
        CreateFile(Application.persistentDataPath, "FileName.txt", "宣雨松MOMO");
        CreateFile(Application.persistentDataPath, "FileName.txt", "宣雨松MOMO");
        CreateFile(Application.persistentDataPath, "FileName.txt", "宣雨松MOMO");
        //得到文本中每一行的内容
        infoall = LoadFile(Application.persistentDataPath, "FileName.txt");

    }*/

    /**
    * path：文件创建目录
    * name：文件的名称
    *  info：写入的内容
    */
    public static void UpdateFile(string path, string name, string info)
    {
        //文件流信息
        StreamWriter sw;
        FileInfo t = new FileInfo(path + "//" + name);

        //如果此文件不存在则创建
        sw = t.CreateText();

        //写入信息
        sw.Write(info);

        //关闭流
        sw.Close();

        //销毁流
        sw.Dispose();

        
    }

    /**
     * path：读取文件的路径
     * name：读取文件的名称
     */
    public static string LoadFile(string path, string name)
    {
        //使用流的形式读取
        
        StreamReader sr = null;
        string finalS = "";
        FileInfo t = new FileInfo(path + "//" + name);
        if (!t.Exists)
        {
            UpdateFile(path, name, CreatXML());
        }
        try
        {
            sr = File.OpenText(path + "//" + name);
        }
        catch (Exception e)
        {
            //路径与名称未找到文件则直接返回空
            return null;
        }

        string line;
        while ((line = sr.ReadLine()) != null)
        {
            //一行一行的读取
            //将每一行的内容存入数组链表容器中
            finalS += line;
        }
        //关闭流
        sr.Close();
        //销毁流
        sr.Dispose();

        return finalS;
    }

    /**
     * path：删除文件的路径
     * name：删除文件的名称
     */

    public void DeleteFile(string path, string name)
    {
        File.Delete(path + "//" + name);
    }

    private static string CreatXML()
    {
        //检测xml是否存在

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
        return xmlDoc.InnerXml;
    }
}