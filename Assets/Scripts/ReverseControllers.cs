using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ReverseControllers : MonoBehaviour
{
    public bool reverseControllers = false;
    [SerializeField] PlainController playerController;
    [SerializeField] PlayerGun[] guns;
    [SerializeField] GameObject postProcess_Reverse;
    [SerializeField] GameObject toReveres_incicator_Text;
    [SerializeField] GameObject toNormal_Indicator_Text;

    AudioManager audioManager;

    private void Start()
    {
        if (audioManager == null)
        {
            audioManager = AudioManager.instance;
        }

    }

    public void ReverseTheControllers()
    {
        if (audioManager != null)
        {
            audioManager.Play("Emergency");
        }

            reverseControllers = !reverseControllers;
        if(reverseControllers)
        {
            toReveres_incicator_Text.SetActive(true);
        }
        else
        {
            toNormal_Indicator_Text.SetActive(true);
        }


        Invoke("controllers", 5f);
    }

    void controllers()
    {

        playerController.reverseControllers = reverseControllers;
        foreach (var playergun in guns)
        {
            playergun.reverseControllers = reverseControllers;
        }
        postProcess_Reverse.SetActive(reverseControllers);

        if (reverseControllers)
            toReveres_incicator_Text.SetActive(false);
        else
            toNormal_Indicator_Text.SetActive(false);

    }

}
