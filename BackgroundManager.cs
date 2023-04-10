using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BackgroundManager : MonoBehaviour
{
    [SerializeField] Image backgroundImage;
    [SerializeField] List<Sprite> backgroundSprites;
    [SerializeField] Sprite defaultSprite;

    void Start()
    {
        SetBackground(defaultSprite);
    }

    //public void GetNextImage()
    //{
        //int index = Random.Range(0, backgroundSprites.Count);
        //backgroundImage.sprite = backgroundSprites[index];
        //backgroundImage.sprite = image;
    //}

    public void SetBackground(Sprite questionSprite)
    {
        backgroundImage.sprite = questionSprite;
    }
}
