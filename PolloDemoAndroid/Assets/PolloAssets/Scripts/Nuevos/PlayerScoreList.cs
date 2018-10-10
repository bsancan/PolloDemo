using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerScoreList : MonoBehaviour
{
    [SerializeField]
    private GameObject playerScoreEntryPrefab;

    [SerializeField]
    private ScoreManager scoreManager;

    [SerializeField]
    private InputField ifdNombre;

    [SerializeField]
    private Text txtScore;

    [SerializeField]
    private Text txtRank;

    void Start()
    {
        string[] names = scoreManager.GetPlayerNames();
        int rank = 0;
        foreach (string name in names) {
            rank++;
            GameObject go = (GameObject)Instantiate(playerScoreEntryPrefab);
            go.transform.SetParent(this.transform);
            go.GetComponent<PlayerScoreEntry>().SetPlayerScoreData(rank, name, scoreManager.GetScore(name));

        }
      

    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
