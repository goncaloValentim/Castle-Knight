using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class Health : MonoBehaviour
{


    private int healthPerHeart = 10;
    public Image[] images;
    public Sprite[] sprites;
    // Use this for initialization
    void Start()
    {
    }
    public void UpdateHearts(int currentHealth)
    {
        if (currentHealth >= 0)
        {
            bool empty = false;
            int i = 0;
            foreach (Image image in images)
            {
                if (empty)
                {
                    image.sprite = sprites[0];
                }
                else
                {
                    i++;
                    if (currentHealth >= i * healthPerHeart)
                    {
                        image.sprite = sprites[sprites.Length - 1];
                    }
                    else
                    {
                        int currentHeartHealth = (int)(healthPerHeart - (healthPerHeart * i - currentHealth));
                        int healthPerImage = healthPerHeart / (sprites.Length - 1);
                        int index = currentHeartHealth / healthPerImage;
                        image.sprite = sprites[index];
                        empty = true;
                    }
                }
            }
        }
    }
}