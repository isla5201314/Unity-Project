using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Setting : MonoBehaviour
{
    public GameObject SettingManager;

    public void OpenSetting()
    {
        Time.timeScale = 0;
        SettingManager.SetActive(true);
    }

    public void BackTheGame()
    {
        Time.timeScale = 1;
        SettingManager.SetActive(false);
    }

    public void ExitTheGame()
    {
        UnityEditor.EditorApplication.isPlaying = false;
        Application.Quit();
    }
}
