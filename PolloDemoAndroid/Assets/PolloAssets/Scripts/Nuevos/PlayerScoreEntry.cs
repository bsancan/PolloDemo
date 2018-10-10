using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerScoreEntry : MonoBehaviour
{
    [SerializeField]
    private Text txtRank;
    [SerializeField]
    private Text txtName;
    [SerializeField]
    private Text txtScore;

    public void SetPlayerScoreData(int rank, string name, int score) {
        txtRank.text = rank.ToString();
        txtName.text = name;
        txtScore.text = score.ToString();
    }
}
