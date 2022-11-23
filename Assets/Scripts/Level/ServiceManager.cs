using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ServiceManager : MonoBehaviour
{
    #region SingleTon
    public static ServiceManager Instance;
    private void Awake()
    {
        if(Instance == null)
        {
            Instance = this;
        }
        else
        {
            Destroy(gameObject);
        }
    }
    #endregion

    private void Start()
    {
        Time.timeScale = 1;
    }
    public void Restart()
    {
        ChangeLevel(SceneManager.GetActiveScene().buildIndex);
    }

    public void EndLevel()
    {
        ChangeLevel(SceneManager.GetActiveScene().buildIndex+1);
    }

    public void ChangeLevel(int lvl)
    {
        SceneManager.LoadScene(lvl);
    }
}

public enum Scenes
{
    MainMenu,
    First,
    Second,
    Third
}
