using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HealthVisualizer : MonoBehaviour
{
    [SerializeField] public Sprite fullHeartSprite;
    [SerializeField] public Sprite emptyHeartSprite;

    private List<Image> hearts = new List<Image>();
    private int lastFull;

    public void Initialize(int heartsCount)
    {
        hearts.Add( AddHeart(transform.position.x, transform.position.y) );
        for (int i = 1; i < heartsCount; i++)
        {
            Vector3 lastHeartPosition = hearts[i-1].transform.localPosition;
            lastHeartPosition.x += 32;
            hearts.Add( AddHeart(lastHeartPosition.x,lastHeartPosition.y) );
        }

        lastFull = hearts.Count-1;
    }
    private Image AddHeart(float x, float y)
    {
        GameObject heartGameObject = new GameObject("Heart", typeof(Image));

        RectTransform rect = heartGameObject.GetComponent<RectTransform>();
        rect.sizeDelta = new Vector2(32, 32);
        heartGameObject.layer = gameObject.layer;
        heartGameObject.transform.parent = transform;
        heartGameObject.transform.localPosition = new Vector3(x,y,-800);
        heartGameObject.transform.localScale = Vector3.one;


        Image heartImage = heartGameObject.GetComponent<Image>();
        heartImage.sprite = fullHeartSprite;
        return heartImage;
    }

    public void ChangeHearts(int value)
    {
        if (value >= 0)
        {
            for(int count = lastFull + value ; (lastFull < count) && (lastFull + 1 < hearts.Count); lastFull++)
            {
                hearts[lastFull+1].sprite = fullHeartSprite;
            }
        }
        else
        {
            for (int count = lastFull + value; (lastFull > count) && (lastFull >= 0); lastFull--)
            {
                hearts[lastFull].sprite = emptyHeartSprite;
            }
        }
    }
}
