using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PotionBag : MonoBehaviour
{
    private GameObject[] potions = new GameObject[10];
    private int quantity = 0;
    private int iterator = -1;

    public GameObject CurrentPotion {
        get{
            if (quantity > 0) {
                return potions[iterator];
                    }
            return null;
        }
    }

    public bool AddPotion(GameObject newPotion)
    {
        if (newPotion.GetComponent<PotionStats>() != null && quantity != potions.Length)
        {
            potions[quantity] = newPotion;
            quantity++;
            iterator++;
            return true;
        }
        return false;
    }
}

