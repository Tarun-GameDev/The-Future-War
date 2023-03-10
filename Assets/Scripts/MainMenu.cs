using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour
{
    [SerializeField] GameObject mainmenu;
    [SerializeField] GameObject rulesmenu;
    [SerializeField]GameObject settingsmenu;

    AudioManager audioManager;

    private void Start()
    {
        if (audioManager == null)
        {
            audioManager = AudioManager.instance;
        }
    }

    public void PlayButton()
    {
        if(audioManager != null)
            audioManager.Play("Click");

        GameManager.instance.levelLoader.LoadLevel(1);
    }

    public void RulesButton()
    {
        if (audioManager != null)
            audioManager.Play("Click");


        rulesmenu.SetActive(true);
        mainmenu.SetActive(false);
    }

    public void SettingsMenu()
    {

        if (audioManager != null)
            audioManager.Play("Click");

        settingsmenu.SetActive(true);
        mainmenu.SetActive(false);
    }

    public void BackFromRules()
    {
        if (audioManager != null)
            audioManager.Play("Click");

        mainmenu.SetActive(true);
        rulesmenu.SetActive(false);
    }

    public void BackFromSettings()
    {
        if (audioManager != null)
            audioManager.Play("Click");

        mainmenu.SetActive(true);
        settingsmenu.SetActive(false);
    }

    public void QuitButton()
    {
        if (audioManager != null)
            audioManager.Play("Click");

        Application.Quit();
    }
}
