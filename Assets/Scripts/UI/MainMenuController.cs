using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MainMenuController : BaseMenu

{
    [SerializeField] private Button play;
    [SerializeField] private Button level;
    [SerializeField] private Button settings;
    [SerializeField] private Button about;
    [SerializeField] private Button reset;
    [SerializeField] private Button quit;

    [Header("Level Menu")]
    [SerializeField] private GameObject levelMenu;
    [SerializeField] private Button back;
    
    protected override void Start()
    {
        base.Start();
        play.onClick.AddListener(Play);
        level.onClick.AddListener(UseLevelMenu);
        quit.onClick.AddListener(Quit);

        back.onClick.AddListener(UseLevelMenu);
        levelMenu.SetActive(false);
    }

    protected override void OnDestroy()
    {
        base.OnDestroy();
        play.onClick.RemoveListener(Play);
        quit.onClick.RemoveListener(Quit);
        level.onClick.RemoveListener(UseLevelMenu);
        back.onClick.RemoveListener(UseLevelMenu);
    }

    public void Play()
    {
        serviceManager.ChangeLevel((int)Scenes.First);
    }

    public void UseLevelMenu()
    {
        levelMenu.SetActive(!levelMenu.activeInHierarchy);
        ChangeMenuStatus();
    }
    public void Quit()
    {
        Application.Quit();
    }
}
