using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PickUp : MonoBehaviour
{
    void OnTriggerEnter2D(Collider2D collision)
    {
        
        if (collision.GetComponent<PlayerStats>() != null)
        {
            collision.GetComponent<MovementController>().Take(gameObject.GetComponent<ItemData>().Item);
            Destroy(gameObject);
        }
    }
}
