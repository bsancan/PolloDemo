using UnityEngine;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;

public static class SaveSystem
{
   public static void SaveScores(ScoresData scores, string name)
    {
        BinaryFormatter formatter = new BinaryFormatter();
        string path = Application.persistentDataPath + "/" 
            + name + ".fun";

        FileStream stream = new FileStream(path, FileMode.Create);

        formatter.Serialize(stream, scores);
        stream.Close();
    }

    public static ScoresData LoadScores(string name)
    {
        string path = Application.persistentDataPath + "/" + name + ".fun";
        if (File.Exists(path))
        {
            BinaryFormatter formatter = new BinaryFormatter();
            FileStream stream = new FileStream(path, FileMode.Open);

            ScoresData scores = formatter.Deserialize(stream) as ScoresData;
            stream.Close();
            return scores;
        }else
        {
            Debug.LogError("No se encontro archivo de scores");
            return null;
        }
    }

    public static void SavePlayerData(PlayerData player)
    {
        BinaryFormatter formatter = new BinaryFormatter();
        string path = Application.persistentDataPath + "/"
            + NameDictionary.playerDataKey + ".fun";

        FileStream stream = new FileStream(path, FileMode.Create);

        formatter.Serialize(stream, player);
        stream.Close();
    }

    public static PlayerData LoadPlayerData()
    {
        string path = Application.persistentDataPath + "/"
            + NameDictionary.playerDataKey + ".fun";
        if (File.Exists(path))
        {
            BinaryFormatter formatter = new BinaryFormatter();
            FileStream stream = new FileStream(path, FileMode.Open);

            PlayerData player = formatter.Deserialize(stream) as PlayerData;
            stream.Close();
            return player;
        }
        else
        {
            Debug.LogError("No se encontro archivo de player");
            return null;
        }
    }
}
