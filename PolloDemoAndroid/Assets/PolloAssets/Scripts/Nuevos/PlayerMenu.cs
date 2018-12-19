using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class PlayerMenu : MonoBehaviour
{
    [SerializeField]
    private GameObject btnMenu;
    [SerializeField]
    private GameObject pnlMenu;
    [SerializeField]
    private GameObject pnlQuestion;
    [SerializeField]
    private Text txtQuestion;

    private int currentQuestion;
    
    void Start()
    {
        
    }

  
    void Update()
    {
        
    }

    public void ShowMenuButton(bool b)
    {
        btnMenu.SetActive(b);
    }

    public void ShowMenuPanel(bool b)
    {
        pnlMenu.SetActive(b);
    }

    public void ShowMenuQuestion(bool b)
    {
        pnlQuestion.SetActive(b);
    }

    public void MenuButton()
    {
        Time.timeScale = 0;
        ShowMenuPanel(true);
        ShowMenuButton(false);
    }

    public void Continue()
    {
        Time.timeScale = 1;
        ShowMenuPanel(false);
        ShowMenuButton(true);
    }

    public void ExitGame()
    {
        currentQuestion = 1;
        pnlMenu.SetActive(true);
        txtQuestion.text = "¿Está seguro de salir del juego?";
    }

    public void QuestionYes()
    {
        if(currentQuestion == 1)
        {
            Application.Quit();
        }
        else if (currentQuestion == 2)
        {
            SceneManager.LoadScene(0);
        }

    }

    public void QuestionNo()
    {
        pnlMenu.SetActive(false);
        currentQuestion = 0;
    }

    public void MainMenu()
    {
        currentQuestion = 2;
        pnlMenu.SetActive(true);
        txtQuestion.text = "¿Está seguro de salir del juego?";
    }

    public void ResetLevel()
    {
        if (!CharacterManager.characterManagerInstance.playerIsDead)
        {
            UIManager.uiManagerInstance.FadeAnimationOUT();
        }
        else if (CharacterManager.characterManagerInstance.playerIsDead)
        {
            UIManager.uiManagerInstance.FadeAnimationOUT();
        }
    }
}
