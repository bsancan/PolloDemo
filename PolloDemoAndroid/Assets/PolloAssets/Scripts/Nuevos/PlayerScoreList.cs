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

    private List<GameObject> golist;
    void Start()
    {
        //golist = new List<GameObject>();
        //string[] names = scoreManager.GetPlayerNames();
        //int rank = 0;
        //foreach (string name in names)
        //{
        //    rank++;
        //    GameObject go = (GameObject)Instantiate(playerScoreEntryPrefab);
        //    go.transform.SetParent(this.transform);
        //    go.GetComponent<PlayerScoreEntry>().SetPlayerScoreData(rank, name, scoreManager.GetScore(name));

        //}

    }

    private void OnEnable()
    {
        // es un nuevo usuario es necesario validarlo, solo se aceptan 10 puntajes
        scoreManager.UpdatePlayerScoreList();
        golist = new List<GameObject>();
        string[] names = scoreManager.GetPlayerNames();
        int rank = 0;
        foreach (string name in names)
        {
            rank++;
            GameObject go = (GameObject)Instantiate(playerScoreEntryPrefab);
            go.transform.SetParent(this.transform);
            //go.GetComponent<RectTransform>().anchoredPosition = GetComponent<RectTransform>().anchoredPosition;
            go.transform.localScale = Vector3.one;
            go.GetComponent<PlayerScoreEntry>().SetPlayerScoreData(rank, name, scoreManager.GetScore(name));
            go.SetActive(true);
            golist.Add(go);
        }

    }

    private void OnDisable()
    {
        if (golist == null)
            return;

        foreach (GameObject g in golist)
        {
            Destroy(g);
        }
        golist = null;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    //public void AddPlayerScore(int r, string n, int s)
    //{
    //    GameObject go = (GameObject)Instantiate(playerScoreEntryPrefab);
    //    go.transform.SetParent(this.transform);
    //    go.GetComponent<PlayerScoreEntry>().SetPlayerScoreData(r, n, s);

    //}
}
