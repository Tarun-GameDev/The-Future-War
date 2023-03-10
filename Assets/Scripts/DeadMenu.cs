using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DeadMenu : MonoBehaviour
{
    //[SerializeField] GameObject gameMenu;
    //[SerializeField] GameObject DeadMenu;

    AudioManager audioManager;
    private void Start()
    {
        if (audioManager == null)
        {
            audioManager = AudioManager.instance;
        }
    }

    public void RestartMenu()
    {
        if (audioManager != null)
            audioManager.Play("Click");
        GameManager.instance.levelLoader.LoadLevel(1);
    }

    public void HomeMenu()
    {
        if (audioManager != null)
            audioManager.Play("Click");
        GameManager.instance.levelLoader.LoadLevel(0);
    }
}
