using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class InGameMenuController : BaseMenu
{
    [SerializeField] private Button resume;
    [SerializeField] private Button restart;
    [SerializeField] private Button settings;
    [SerializeField] private Button quit;
    // Start is called before the first frame update
    protected override void Start()
    {
        base.Start();

        menu.SetActive(false);
        resume.onClick.AddListener(ChangeMenuStatus);
        restart.onClick.AddListener(serviceManager.Restart);

        quit.onClick.AddListener(ExitToMenu);
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            ChangeMenuStatus();
        }   
    }

    protected override void OnDestroy()
    {
        base.OnDestroy();
        resume.onClick.RemoveListener(ChangeMenuStatus);
        restart.onClick.RemoveListener(serviceManager.Restart);
        quit.onClick.RemoveListener(ExitToMenu);
    }

    protected override void ChangeMenuStatus()
    {
        base.ChangeMenuStatus();
        Time.timeScale = menu.activeInHierarchy ? 0 : 1;
    }

    public void ExitToMenu()
    {
        serviceManager.ChangeLevel((int)Scenes.MainMenu);
    }

    
}
