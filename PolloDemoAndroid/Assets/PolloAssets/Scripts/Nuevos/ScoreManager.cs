using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

public class ScoreManager : MonoBehaviour
{
    Dictionary<string, int> playerScores;
    [SerializeField]
    private GameObject pnlScoreList;
    [SerializeField]
    private PlayerScoreList playerScoreList;

    public string currentPlayerName;
    public int currentPlayerScore;

    void Start()
    {

        //pnlScoreList.SetActive(false);
       
    }

    public void ShowScoreList(bool b) {
        pnlScoreList.SetActive(b);
    }

    void Init() {
        if (playerScores != null) {
            return;
        }
        playerScores = new Dictionary<string, int>();

        //busoc si se encuentra alguna memoria guardada
        SL_playersScoreList load = (SL_playersScoreList)Helper.LoadPlayersScore(NameDictionary.playerScoreListKey_lvl_01);
        if(load != null)
        {
            foreach(SL_playersScoreList sl in load.scores)
            {
                SetScore(sl.name, sl.score);
            }

            print("Se cargo la memoria de < " + NameDictionary.playerScoreListKey_lvl_01 + " >");

        }   else
        {
            //default data
            //SetScore("Nuevo record", 0);
            SetScore("Barto14", 100);
            SetScore("Garfield", 200);
            SetScore("Luffy", 300);
            SetScore("Balrog", 400);
            SetScore("JustinWong", 500);
            SetScore("OnePunch1", 600);
            SetScore("ElGato", 700);
            SetScore("Homero", 800);
            SetScore("KillYou", 900);
            SetScore("KissMe", 1000);

            print("Lista por default");
        }

        //print("---------------");
    }

    public int GetScore(string userName) {
        Init();
        if(playerScores.ContainsKey(userName) == false)
        {
            return 0;
        }

        return playerScores[userName];
    }

    public void SetScore(string userName, int value) {
        Init();
        playerScores[userName] = value;

    }

    public void ChangeScore(string userName, int value) {
        Init();
        int currScore = GetScore(userName);
        if (value > currScore) {
            SetScore(userName, value);
        }
        
    }

    public string[] GetPlayerNames() {
        Init();
        return playerScores.Keys.OrderByDescending(n => GetScore(n)).ToArray();
        //string[] names = playerScores.Keys.ToArray();

        //return names.OrderBy(n => GetScore(n)).ToArray();
    }

    public void UpdatePlayerScoreList()
    {
        Init();
        if(GetScore(currentPlayerName) > 0)
        {
            ChangeScore(currentPlayerName, currentPlayerScore);

        }
        else
        {
            SetScore(currentPlayerName, currentPlayerScore);
            string[] _list = playerScores.Keys.OrderByDescending(n => GetScore(n)).ToArray();

            playerScores.Remove(_list[_list.Length - 1]);

        }
    }

   

}
