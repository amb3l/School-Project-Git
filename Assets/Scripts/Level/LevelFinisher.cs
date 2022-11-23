using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelFinisher : MonoBehaviour
{
    private void OnTriggerEnter2D(Collider2D stats)
    {
        if (stats.GetComponent<PlayerStats>() != null)
        {
            ServiceManager.Instance.EndLevel();
        }
    }
}
