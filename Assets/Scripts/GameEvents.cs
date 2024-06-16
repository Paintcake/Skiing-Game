using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameEvents : MonoBehaviour
{
    public delegate void RaceAction();
    public static event RaceAction OnRaceStart;
    public static event RaceAction OnRaceStop;
    public static event RaceAction OnRestartLevel;
    public static event RaceAction OnLoadNextLevel;
    public static event RaceAction OnQuitGame;

    public static void StartRace()
    {
        if (OnRaceStart != null)
        {
            OnRaceStart();
        }
    }

    public static void StopRace()
    {
        if (OnRaceStop != null)
        {
            OnRaceStop();
        }
    }

    public static void RestartLevel()
    {
        if (OnRestartLevel != null)
        {
            OnRestartLevel();
        }
    }

    public static void LoadNextLevel()
    {
        if (OnLoadNextLevel != null)
        {
            OnLoadNextLevel();
        }
    }

    public static void QuitGame()
    {
        if (OnQuitGame != null)
        {
            OnQuitGame();
        }
    }

    private static IEnumerator QuitAfterDelay(float delay)
    {
        yield return new WaitForSeconds(delay);
        Application.Quit();
    }
}
