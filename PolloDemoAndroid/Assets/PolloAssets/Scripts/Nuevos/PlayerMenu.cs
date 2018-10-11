using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMenu : MonoBehaviour
{
    [SerializeField]
    private GameObject btnMenu;
    [SerializeField]
    private GameObject pnlMenu;
    [SerializeField]
    private GameObject pnlQuestion;

    
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
}
