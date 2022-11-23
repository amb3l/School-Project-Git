using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DeathTimer : MonoBehaviour
{
    [SerializeField] float deathTime;

    void Start()
    {
        Destroy(gameObject,deathTime);
    }
}
