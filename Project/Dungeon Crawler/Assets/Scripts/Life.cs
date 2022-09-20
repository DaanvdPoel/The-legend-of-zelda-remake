using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Life : MonoBehaviour
{
    private PlayerHealth playerhealth;

    [SerializeField] private GameObject heartPrefab;
    [SerializeField] private GameObject heartParent;
    [SerializeField] private Sprite emptyHeart;
    [SerializeField] private Sprite halfHeart;
    [SerializeField] private Sprite fullHeart;
    [SerializeField] private List<Image> heartImages = new List<Image>();

    void Start()
    {
        HeartSetUp();
        HeartsUpdate();
    }

    public void HeartSetUp()
    {
        playerhealth = FindObjectOfType<PlayerHealth>();
        if (playerhealth == null)
        {
            Debug.LogError("Can't find PlayerHealth in scene, Object might be inactive");
            return;
        }

        for (int i = 0; i < playerhealth.maxHealth; i++)
        {
            GameObject heart = Instantiate(heartPrefab, heartParent.transform);
            heartImages.Add(heart.GetComponent<Image>());
        }
    }

    public void AddExtraHeartToUI()
    {
        GameObject heart = Instantiate(heartPrefab, heartParent.transform);
        heartImages.Add(heart.GetComponent<Image>());
    }

    public void HeartsUpdate()
    {
        for (int i = 0; i < heartImages.Count; i++)
        {
            if(i + 0.5f == playerhealth.GetCurrentHealth())
            {
                heartImages[i].sprite = halfHeart;
            }
            else if(i < playerhealth.GetCurrentHealth())
            {
                heartImages[i].sprite = fullHeart;
            }
            else
            {
                heartImages[i].sprite = emptyHeart;
            }
        }
    }
}
