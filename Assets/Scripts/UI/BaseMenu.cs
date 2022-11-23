using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BaseMenu : MonoBehaviour
{
            protected ServiceManager serviceManager;
    [SerializeField] protected GameObject menu;
    // Start is called before the first frame update
    protected virtual void Start()
    {
        serviceManager = ServiceManager.Instance;
    }

    protected virtual void OnDestroy()
    {

    }

    protected virtual void ChangeMenuStatus()
    {
        menu.SetActive(!menu.activeInHierarchy);
    }
}
