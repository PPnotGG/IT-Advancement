using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class Settings : MonoBehaviour
{
    public Toggle screenToggle;
    public bool FSOn = true;

    public void Awake()
    {
        DontDestroyOnLoad(this);
    }

    public void FullScreenToggle()
    {
        FSOn = !FSOn;
        Screen.fullScreen = FSOn;
    }

    public void BackPressed()
    {
        SceneManager.LoadScene("Menu");
    }
}