using UnityEngine;
using System;

public class DamageDealer : MonoBehaviour
{
    [SerializeField] int damage;
    [SerializeField] float timeDelay;
    private EntityStats entity;
    private float lastEncounter;
    private void OnTriggerEnter2D(Collider2D stats)
    {
        if (Time.time - lastEncounter < 0.4f)
        {
            return;
        }

        lastEncounter = Time.time;
        entity = stats.GetComponent<EntityStats>();
        if(entity != null)
        {
            entity.TakeDamage(damage);
        }
    }

    private void FixedUpdate()
    {
        if (entity != null && (Time.time - lastEncounter) > timeDelay)
        {
            entity.TakeDamage(damage);
            lastEncounter = Time.time;
        }
    }

    private void OnTriggerExit2D(Collider2D stats)
    {
        
        if(entity == stats.GetComponent<PlayerStats>())
        {
            entity = null;
        }
    }
}
