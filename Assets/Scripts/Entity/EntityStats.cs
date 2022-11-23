using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EntityStats : MonoBehaviour
{
    [SerializeField] protected int maxHP;
    [SerializeField] protected int currentHP;
    

    void Start()
    {
        currentHP = maxHP;
    }

    public virtual void RestoreHP(int hp)
    {
        currentHP += hp;
        currentHP = currentHP > maxHP ? maxHP : currentHP;
    }

    public virtual void TakeDamage(int damage)
    {
        currentHP -= damage;
        if (currentHP <= 0)
        {
            OnDeath();
        }
    }

    public virtual void OnDeath()
    {
        Destroy(gameObject);
    }
}
