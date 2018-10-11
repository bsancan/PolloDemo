using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;
using System.Xml.Serialization;
using UnityEngine;

public static class Helper
{
   public static string Serialize<T>(this T toSerialize)
    {
        XmlSerializer xml = new XmlSerializer(typeof(T));
        StringWriter writer = new StringWriter();
        xml.Serialize(writer, toSerialize);
        return writer.ToString();
    }

    public static T Deserialize<T>(this string toDeserialize)
    {
        XmlSerializer xml = new XmlSerializer(typeof(T));
        StringReader reader = new StringReader(toDeserialize);
        return (T)xml.Deserialize(reader);
    }

    public static bool SavePlayersScore(string tag, object obj)
    {
        if(tag.Length > 4 && obj != null)
        {
            MemoryStream memoryStream = new MemoryStream();
            BinaryFormatter binaryFormatter = new BinaryFormatter();
            binaryFormatter.Serialize(memoryStream, obj);
            string temp = System.Convert.ToBase64String(memoryStream.ToArray());
            PlayerPrefs.SetString(tag, temp);
            return true;
        }else
        {
            return false;
        }
        

    }

    public static object LoadPlayersScore(string tag)
    {
        if(PlayerPrefs.GetString(tag) != string.Empty)
        {
            MemoryStream memoryStream = new MemoryStream(Convert.FromBase64String(PlayerPrefs.GetString(tag)));
            BinaryFormatter binaryFormatter = new BinaryFormatter();
            return binaryFormatter.Deserialize(memoryStream);
        }else
        {
            return null;
        }
    }
}
