using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FadeController : MonoBehaviour
{
    public void GameOver()
    {
        if (CharacterManager.characterManagerInstance.playerIsDead)
        {
            //reiniciar nivel
            GameManager.gameManagerInstance.ResetLevel();
        }
        else
        {
            //ir al siguiente nivel
            GameManager.gameManagerInstance.GoToNextLevel();
        }
    }
}
