using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    [Header("Camera Shake")]
    [SerializeField] float cameraShakeDuration;
    [SerializeField] float cameraShakeStrength;
    [SerializeField] CameraShake cameraShake;
    public LevelLoader levelLoader;

    public static GameManager instance;
    private void Start()
    {
        cameraShake = Camera.main.GetComponent<CameraShake>();
    }


    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
        }
        else
        {
            Destroy(gameObject);
        }
    }

    private void Update()
    {
        if(Input.GetKeyDown(KeyCode.U))
        {
            SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
        }
    }

    public void Camerashake()
    {
        StartCoroutine(cameraShake.Shake(cameraShakeDuration, cameraShakeStrength));
    }

}
