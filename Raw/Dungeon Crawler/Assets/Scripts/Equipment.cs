using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Equipment : MonoBehaviour
{
    [SerializeField] private Image iconButton_A;
    [SerializeField] private Image iconButton_B;
    [SerializeField] private Text rupeeCountText;
    [SerializeField] private Text keyCountText;
    public int keyCount;
    public int rupeeCount;

    void Start()
    {

        StartInactive();
    }

    private void Update()
    {
        UpdateTextCounters();
    }

    private void StartInactive()
    {
        if (iconButton_A == null)
        {
            iconButton_A.gameObject.SetActive(false);
        }

        if (iconButton_B == null)
        {
            iconButton_B.gameObject.SetActive(false);
        }
    }

    public void SetIconA(Sprite iconsprite)
    {
        iconButton_A.gameObject.SetActive(true);
        iconButton_A.sprite = iconsprite;
    }

    public void SetIconB(Sprite iconsprite)
    {
        iconButton_B.gameObject.SetActive(true);
        iconButton_B.sprite = iconsprite;
    }

    private void UpdateTextCounters()
    {
        rupeeCountText.text = "X" + rupeeCount;
        keyCountText.text = "X" + keyCount;
    }
}
