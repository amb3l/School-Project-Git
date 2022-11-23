using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class PotionStats : MonoBehaviour
{
    [SerializeField] GameObject potionEffect;
    [SerializeField] int coolDown;
    [SerializeField] LayerMask aimLayer;
    public int CoolDown { get { return this.coolDown; }  }
    

    // Update is called once per frame
    void FixedUpdate()
    {
        if (GetComponent<Rigidbody2D>().IsTouchingLayers(aimLayer))
        {
            Destroy(gameObject);
            Instantiate(potionEffect, new Vector3(transform.position.x, (float)System.Math.Ceiling(transform.position.y)), Quaternion.identity);

        }
    }
}
