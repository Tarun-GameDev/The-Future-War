using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class PlainController : MonoBehaviour
{
    Rigidbody rb;
    public float speed;
    public float acceleration;

    public float rotationControl;
    public int distance_Travelled;

    [SerializeField] float maxheigh;
    [SerializeField] int heighDamage;
    [SerializeField] float horizontal,vertical = 1;

    [SerializeField] int velocity;
    [SerializeField] int heigh;
    [SerializeField] PlayerHealth playerHealth;
    [SerializeField] TextMeshProUGUI heighText;
    [SerializeField] TextMeshProUGUI distanceTravelledText;
    [SerializeField] TextMeshProUGUI speedText;

    Vector3 lasstposition;

    public bool reverseControllers = false;

    void Start()
    {
        rb = GetComponent<Rigidbody>();
        Cursor.lockState = CursorLockMode.Locked;
    }

    void Update()
    {
        if(reverseControllers)
        {
            vertical = -Input.GetAxis("Vertical");
            horizontal = -Input.GetAxis("Horizontal");
        }
        else
        {
            vertical = Input.GetAxis("Vertical");
            horizontal = Input.GetAxis("Horizontal");
        }


        heigh = Mathf.FloorToInt(transform.position.y);
        if(heighText != null)
            heighText.text = "Heigh:" + heigh.ToString() + "m";


        distance_Travelled = Mathf.FloorToInt(transform.position.x);
        if(distanceTravelledText != null)
            distanceTravelledText.text = "Distance Travelled:" + distance_Travelled.ToString() + "m";


        velocity = Mathf.FloorToInt((transform.position - lasstposition).magnitude / Time.deltaTime);
        speedText.text = "Speed:" + velocity.ToString();
        lasstposition = transform.position;

        if(heigh > maxheigh)
        {
            playerHealth.TakeDamage(heighDamage);
        }

    }

    private void FixedUpdate()
    {
        //transform.Rotate(new Vector3(-vertical * rotationControl * Time.fixedDeltaTime,0f, 0f));
        transform.Rotate(new Vector3(Input.acceleration.x * rotationControl * Time.fixedDeltaTime, 0f, 0f));
        
        if (horizontal > .1 || horizontal < -.1)
        {
            transform.position += transform.forward * (speed + horizontal * acceleration) * Time.fixedDeltaTime ;
        }
        else
        {
            transform.position += transform.forward * speed * Time.fixedDeltaTime;
        }
    }
}

