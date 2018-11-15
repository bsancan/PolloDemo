using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using System.Xml.Serialization;
using System.IO;
using System.Runtime.Serialization;
using System.Runtime.Serialization.Formatters.Binary;
using System;
using System.Xml;

public class SaveLoadXml {

    //public static BinaryFormatter binaryFormatter = new BinaryFormatter();

    public static ManualVectors LoadVectorsFromResources(string path)
    {

        // if your original XML file is located at
        // "Ressources/MyXMLFile     .xml"
        //Resources.Load("MyXMLFile");  
        TextAsset textAsset = (TextAsset)Resources.Load(path);
        //XmlDocument xmldoc = new XmlDocument();
        //xmldoc.LoadXml(textAsset.text);

        ManualVectors dia = new ManualVectors();
        XmlSerializer serz = new XmlSerializer(typeof(ManualVectors));
        using (StringReader reader = new StringReader(textAsset.text))
        {
            if (reader.ReadLine() != null)
                dia = (ManualVectors)serz.Deserialize(reader);
        }
        Debug.Log("CARGADO - XML METEOR");
        return dia;

    }

    public static void SaveDialogueXml(string txtFile, ManualVectors vectors)
    {
        try
        {
            XmlSerializer serz = new XmlSerializer(typeof(ManualVectors));
            StreamWriter writer = new StreamWriter("Assets/Resources/" + txtFile + ".xml");

            serz.Serialize(writer, vectors);

            writer.Close();
            Debug.Log("GUARDADO - " + txtFile);
        }
        catch (Exception ex)
        {
            Debug.Log(ex.Message.ToString());
        }



    }

    

    //public static MeteorVectors LoadXml(string path)
    //{
    //    XmlSerializer serz = new XmlSerializer(typeof(MeteorVectors));
    //    StreamReader reader = new StreamReader(path + ".xml");

    //    MeteorVectors dia = (MeteorVectors)serz.Deserialize(reader);
    //    reader.Close();
    //    // txtFileLoad.Text = path;
    //    Debug.Log("CARGADO - XML");
    //    return dia;
    //}


    //public static MeteorVectors LoadXml(TextAsset xml)
    //{
    //    MeteorVectors dia = new MeteorVectors();
    //    XmlSerializer serz = new XmlSerializer(typeof(MeteorVectors));
    //    using (StringReader reader = new StringReader(xml.text))
    //    {
    //        if (reader.ReadLine() != null)
    //            dia = (MeteorVectors)serz.Deserialize(reader);
    //    }
    //    Debug.Log("CARGADO - XML");
    //    return dia;
    //}


    //public static string SaveObject(string tag, object obj)
    //{
    //    string res = "";
    //    try
    //    {
    //        MemoryStream memoryStream = new MemoryStream();
    //        binaryFormatter.Serialize(memoryStream, obj);
    //        string temp = Convert.ToBase64String(memoryStream.ToArray());
    //        PlayerPrefs.SetString(tag, temp);
    //        res = "OK";
    //    }
    //    catch (Exception ex)
    //    {
    //        res = ex.Message.ToString();
    //    }

    //    return res;
    //}
}
