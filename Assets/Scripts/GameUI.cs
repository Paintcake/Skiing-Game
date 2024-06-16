using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameUI : MonoBehaviour
{
    public void Restart()
    {
        GameEvents.RestartLevel();
    }

    public void Load()
    {
        GameEvents.LoadNextLevel();
    }

    public void Quit()
    {
        GameEvents.QuitGame();
    }
}
