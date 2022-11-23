using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerStats : EntityStats
{
                     protected ServiceManager serviceManager;
    [SerializeField] protected float invincibilityTime;
                     protected float lastDamageTime;
    [SerializeField] private GameObject UI;
    private HealthVisualizer healthVis;

    public AudioSource DamageSound;//character gets gamage

    void Start()
    {
        serviceManager =  FindObjectOfType<ServiceManager>();
        currentHP = maxHP;
        healthVis = UI.GetComponent<HealthVisualizer>();
        healthVis.Initialize(maxHP);
        serviceManager = ServiceManager.Instance;
    }
    
    public override void TakeDamage(int damage)
    {
        if (Time.time - lastDamageTime > invincibilityTime)
        {
            healthVis.ChangeHearts(-damage);

            lastDamageTime = Time.time;

            DamageSound.pitch = Random.Range(0.9f, 1.1f);
            DamageSound.Play();
            base.TakeDamage(damage);
        }
    }

    public override void RestoreHP(int hp)
    {
        base.RestoreHP(hp);
        healthVis.ChangeHearts(hp);
    }

    public override void OnDeath()
    {
        serviceManager.Restart();
    }
}

    

    
