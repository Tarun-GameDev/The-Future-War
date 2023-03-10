using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityStandardAssets.CrossPlatformInput;

public class PlayerGun : MonoBehaviour
{
    [SerializeField] Animator animator;

    [Header("Bullet")]
    [SerializeField] GameObject bullet;
    [SerializeField] Transform gunEdge;
    [SerializeField] float shootForce, upwardForce;

    [Header("Gun Status")]
    [SerializeField] float delayBetweenShooting;
    [SerializeField] float timeBetweenShooting, spread, reloadTime, timeBetweenShots;
    public int magazineSize, bulletsPerTap;
    [SerializeField] bool allowBullettoHold;
    [SerializeField] bool backGuns = false;
    [SerializeField] bool missileLauncher = false;

    [Header("Bug Fixing")]
    [SerializeField] bool allowInvoke = true;

    #region Private Variables

    [SerializeField] int bulletsLeft, bulletsShot;
    bool shooting, readyToShoot;
    [SerializeField] bool reloading;
    public bool reverseControllers = false;

    AudioManager audioManager;

    ObjectPoller objectPoller;
    #endregion



    void Start()
    {
        if(audioManager == null)
        {
            audioManager = AudioManager.instance;
        }
        objectPoller = ObjectPoller.Instance;
        //make sure magazine is full
        bulletsLeft = magazineSize;
        readyToShoot = true;
    }

    void Update()
    {
        MyInput();
    }

    private void MyInput()
    {
        #region PcControllers
        /*
        if (reverseControllers)
        {
            if (backGuns)
            {
                //check if allowed to hold down and take corresponding input
                if (allowBullettoHold) shooting = Input.GetKey(KeyCode.Mouse0);
                else shooting = Input.GetKeyDown(KeyCode.Mouse0);

                if (allowBullettoHold) shooting = Input.GetKey(KeyCode.H);
                else shooting = Input.GetKeyDown(KeyCode.H);
            }
            else if (missileLauncher)
            {
                //check if allowed to hold down and take corresponding input
                if (allowBullettoHold) shooting = Input.GetKey(KeyCode.Space);
                else shooting = Input.GetKeyDown(KeyCode.Space);
            }
            else
            {
                //check if allowed to hold down and take corresponding input
                if (allowBullettoHold) shooting = Input.GetKey(KeyCode.Mouse1);
                else shooting = Input.GetKeyDown(KeyCode.Mouse1);

                if (allowBullettoHold) shooting = Input.GetKey(KeyCode.F);
                else shooting = Input.GetKeyDown(KeyCode.F);
            }

        }
        else
        {
            if (backGuns)
            {
                //check if allowed to hold down and take corresponding input
                if (allowBullettoHold) shooting = Input.GetKey(KeyCode.Mouse1);
                else shooting = Input.GetKeyDown(KeyCode.Mouse1);

                if (allowBullettoHold) shooting = Input.GetKey(KeyCode.F);
                else shooting = Input.GetKeyDown(KeyCode.F);
            }
            else if (missileLauncher)
            {
                //check if allowed to hold down and take corresponding input
                if (allowBullettoHold) shooting = Input.GetKey(KeyCode.Space);
                else shooting = Input.GetKeyDown(KeyCode.Space);
            }
            else
            {
                //check if allowed to hold down and take corresponding input
                if (allowBullettoHold) shooting = Input.GetKey(KeyCode.Mouse0);
                else shooting = Input.GetKeyDown(KeyCode.Mouse0);

                if (allowBullettoHold) shooting = Input.GetKey(KeyCode.H);
                else shooting = Input.GetKeyDown(KeyCode.H);
            }
        }*/
        #endregion

        #region AndroidControllers
        if (reverseControllers)
        {
            if (backGuns)
            {
                //check if allowed to hold down and take corresponding input
                if (allowBullettoHold) shooting = CrossPlatformInputManager.GetButton("ShootFront");
                else shooting = CrossPlatformInputManager.GetButtonDown("ShootFront");
            }
            else if (missileLauncher)
            {
                //check if allowed to hold down and take corresponding input
                if (allowBullettoHold) shooting = CrossPlatformInputManager.GetButton("ShootMissile");
                else shooting = CrossPlatformInputManager.GetButtonDown("ShootMissile");
            }
            else
            {
                //check if allowed to hold down and take corresponding input
                if (allowBullettoHold) shooting = CrossPlatformInputManager.GetButton("ShootBack");
                else shooting = CrossPlatformInputManager.GetButtonDown("ShootBack");
            }

        }
        else
        {
            if (backGuns)
            {
                //check if allowed to hold down and take corresponding input
                if (allowBullettoHold) shooting = CrossPlatformInputManager.GetButton("ShootBack");
                else shooting = CrossPlatformInputManager.GetButtonDown("ShootBack");
            }
            else if (missileLauncher)
            {
                //check if allowed to hold down and take corresponding input
                if (allowBullettoHold) shooting = CrossPlatformInputManager.GetButton("ShootMissile");
                else shooting = CrossPlatformInputManager.GetButtonDown("ShootMissile");
            }
            else
            {
                //check if allowed to hold down and take corresponding input
                if (allowBullettoHold) shooting = CrossPlatformInputManager.GetButton("ShootFront");
                else shooting = CrossPlatformInputManager.GetButtonDown("ShootFront");
            }
        }
        #endregion

        //Reloading
        if (Input.GetKeyDown(KeyCode.R) && bulletsLeft < magazineSize && !reloading) Reload();
        //Reload autimatically when trying to shoot without ammo
        if (readyToShoot && shooting && !reloading && bulletsLeft <= 0) Reload();

        //shooting
        if (readyToShoot && shooting && !reloading && bulletsLeft > 0)
        {
            //set bullets shot to 0
            bulletsShot = 0;
            Invoke("Shoot", delayBetweenShooting);
        }


    }
    private void Shoot()
    {
        readyToShoot = false;

        if (!missileLauncher && audioManager != null)
        {
            audioManager.Play("Shoot01");
        }
            

        if(animator != null)
        {
            animator.SetTrigger("DoRecoil");
        }

        //calculate direction from attackpoint to targetpoint
        Vector3 directionWithoutSpread = gunEdge.transform.forward;

        //calculate Spread
        float x = Random.Range(-spread, spread);
        float y = Random.Range(-spread, spread);

        //calculate new direction with spread
        Vector3 directionWithSpread = directionWithoutSpread + new Vector3(x, y, 0); //just add spread to last direction

        //Instantiate bullet
        GameObject currentBullet = Instantiate(bullet, gunEdge.transform.position, gunEdge.transform.rotation);
        //var currentBullet = objectPoller.SpawnFromPool("Bullet", gunEdge.transform.position, gunEdge.transform.rotation);

        //Rotate bullet to shoot direction
        currentBullet.transform.forward = directionWithSpread.normalized;

        //add force to bullet
        currentBullet.GetComponent<Rigidbody>().AddForce(currentBullet.transform.forward * shootForce, ForceMode.Impulse);

        bulletsLeft--;
        bulletsShot++;

        //invoke resetShot function (if not already invoked)
        if (allowInvoke)
        {
            Invoke("ResetShot", timeBetweenShooting);
            allowInvoke = false;
        }

        //if more than one bulletsperTap make sure to repear shoot funtion
        if (bulletsShot < bulletsPerTap && bulletsLeft > 0)
            Invoke("Shoot", timeBetweenShots);
    }

    private void ResetShot()
    {
        //allow shootin and invoking again
        readyToShoot = true;
        allowInvoke = true;
    }

    private void Reload()
    {
        reloading = true;
        Invoke("ReloadFinished", reloadTime);

        //reloading audio
        /*
        if (reloadingAudio != null)
            AudioManager.instance.Play(reloadingAudio);*/
    }

    private void ReloadFinished()
    {
        bulletsLeft = magazineSize;
        reloading = false;
    }

}
