﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    [Header("PARA PRUEBAS Y DESARROLLO")]
    [SerializeField]
    private bool developMode;
    [SerializeField]
    private bool resetAllKeys;


    [Header("============")]
    public int currentLevel;

    public static GameManager gameManagerInstance;
    public bool isGameOver;
    [SerializeField]
    private int frameRate;

    public GameObject virtualCam1;
    public GameObject virtualCam2;

    public GameObject playerPivot;
    public GameObject uiManager;
    public GameObject ammoManager;
    public GameObject explosionManager;

    public UnityEngine.UI.Text txtFPS;

    float frameCount = 0f;
    float nextUpdate = 0.0f;
    float fps = 0.0f;
    float updateRate = 4.0f;  // 4 updates per sec.

    //private Quaternion initialRotation;
    //private Level02 level02;
    //private Transform tunnel;

    //private GameObject _playerPivot;
    //private GameObject _uiManager;
    //private GameObject _ammoManger;

    void Start()
    {
        if(resetAllKeys)
        {
            PlayerPrefs.DeleteAll();
        }

        if (gameManagerInstance == null)
        {
            gameManagerInstance = this;
        } else
        {
            Destroy(gameObject);
        }
        //Sets this to not be destroyed when reloading scene
        //DontDestroyOnLoad(gameObject);

        if (CharacterManager.characterManagerInstance == null)
        {
            GameObject go1 = Instantiate(playerPivot, Vector3.zero, Quaternion.identity) as GameObject;
            go1.SetActive(true);
        }

        if (UIManager.uiManagerInstance == null)
        {
            GameObject go2 = Instantiate(uiManager) as GameObject;
            go2.SetActive(true);
        }

        if (AmmoManager.ammoManagerInstance == null)
        {
            GameObject go3 = Instantiate(ammoManager) as GameObject;
            go3.SetActive(true);
        }

        if (ExplosionManager.explosionManagerInstance == null)
        {
            GameObject go4 = Instantiate(explosionManager) as GameObject;
            go4.SetActive(true);
        }

        // Make the game run as fast as possible
        Application.targetFrameRate = frameRate;

        StartCoroutine(CorFirstInitial());

        //busco el primer nivel
        //int level01 = 0;
        //for(int i = 0; i < SceneManager.sceneCount; i++)
        //{
        //    if (SceneManager.GetSceneAt(i).name == NameDictionary.levelScene_01)
        //    {
        //        level01++;
        //    }
        //}

        //if(level01 == 0)
        //{
        //    SceneManager.LoadScene(NameDictionary.levelScene_01, LoadSceneMode.Additive);
        //    print("se añadio level 01");
        //}

        //currentLevel = 1;
        //inicia el movimiento hacia adelante del player
        //CharacterManager.characterManagerInstance.startMovement = true;
    }

    // Update is called once per frame
    void Update()
    {
        frameCount++;
        if (Time.time > nextUpdate)
        {
            nextUpdate += 1.0f / updateRate;
            fps = frameCount * updateRate;
            frameCount = 0;
            txtFPS.text = fps.ToString();
        }
    }

    public void SetInitialLevelValues() {
        if (!developMode)
        {
            PlayerData playerData = SaveSystem.LoadPlayerData();

            if(playerData == null)
            {
                //en caso de no encontrar el archivo del player
                playerData = new PlayerData(1, Vector3.zero);
                SaveSystem.SavePlayerData(playerData);
            }

            currentLevel = playerData.lastLevel;

            if (currentLevel == 1)
            {
                virtualCam1.SetActive(true);
                virtualCam2.SetActive(false);
                SceneManager.LoadScene(NameDictionary.levelScene_01, LoadSceneMode.Additive);
                CharacterManager.characterManagerInstance.character.SetInitialValues();
                CharacterManager.characterManagerInstance.character.StartEnergyConsumption();
                CharacterManager.characterManagerInstance.startMovement = true;
            }
            else if (currentLevel == 2)
            {
                virtualCam1.SetActive(false);
                virtualCam2.SetActive(true);
                SceneManager.LoadScene(NameDictionary.levelScene_02, LoadSceneMode.Additive);
                CharacterManager.characterManagerInstance.character.SetInitialValues();
                //CharacterManager.characterManagerInstance.character.StartEnergyConsumption();
                CharacterManager.characterManagerInstance.startMovement = false;
                //CharacterManager.characterManagerInstance.SetInitialTunnelValues();

            }


        }
        else
        {
            if (currentLevel == 1)
            {
                virtualCam1.SetActive(true);
                virtualCam2.SetActive(false);
                //SceneManager.LoadScene(NameDictionary.levelScene_01, LoadSceneMode.Additive);
                CharacterManager.characterManagerInstance.character.SetInitialValues();
                CharacterManager.characterManagerInstance.character.StartEnergyConsumption();
                CharacterManager.characterManagerInstance.startMovement = true;
            }
            else if (currentLevel == 2)
            {
                virtualCam1.SetActive(false);
                virtualCam2.SetActive(true);
                //SceneManager.LoadScene(NameDictionary.levelScene_02, LoadSceneMode.Additive);
                CharacterManager.characterManagerInstance.character.SetInitialValues();
                //CharacterManager.characterManagerInstance.character.StartEnergyConsumption();
                CharacterManager.characterManagerInstance.startMovement = false;
             
                //CharacterManager.characterManagerInstance.SetInitialTunnelValues();

            }
        }
    }

    public void GoToNextLevel()
    {
        //SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
        StartCoroutine(CorNextLevel());
    }

    public void ResetLevel()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }

    IEnumerator CorFirstInitial() {
        yield return null;

        while (CharacterManager.characterManagerInstance == null) {
            yield return null;
        }

        SetInitialLevelValues();

    }

    IEnumerator CorNextLevel() {
        //UIManager.uiManagerInstance.FadeAnimationOUT();
        yield return new WaitForSeconds(0.5f);
        SceneManager.UnloadSceneAsync(currentLevel);
        UIManager.uiManagerInstance.scoreManager.ShowScoreList(false);
        currentLevel = 2;
        SetInitialLevelValues();
        yield return new WaitForSeconds(0.5f);
        UIManager.uiManagerInstance.FadeAnimationIN();
    }
}
