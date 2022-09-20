using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class MenuManager : MonoBehaviour
{
    [SerializeField] private GameObject startButton;
    [SerializeField] private GameObject ezModeButton;
    [SerializeField] private GameObject normalModeButton;
    [SerializeField] private GameObject Text;

    public void LoadOverworld()
    {
        SceneManager.LoadScene("Overworld");
        Debug.Log("Loading normal mode");
    }

    public void LoadOverworldEZ()
    {
        SceneManager.LoadScene("Overworld_EZ");
        Debug.Log("Loading EZ mode");
    }

    public void Continue()
    {
        ezModeButton.SetActive(true);
        normalModeButton.SetActive(true);
        startButton.SetActive(false);
        Text.SetActive(true);
    }
}
