using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class EnemyGun : MonoBehaviour
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
    [SerializeField] AudioSource Shootaudio;
    [SerializeField] bool allowBullettoHold;
    [SerializeField] bool chasingMissile = false;

    [Header("Bug Fixing")]
    [SerializeField] bool allowInvoke = true;

    #region Private Variables

    [SerializeField] int bulletsLeft, bulletsShot;
    bool readyToShoot;
    [SerializeField] bool reloading;

    ObjectPoller objectPoller;
    #endregion



    void Start()
    {
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
        //Reload autimatically when trying to shoot without ammo
        if (readyToShoot && !reloading && bulletsLeft <= 0) Reload();

        //shooting
        if (readyToShoot && !reloading && bulletsLeft > 0)
        {
            //set bullets shot to 0
            bulletsShot = 0;
            Invoke("Shoot", delayBetweenShooting);
        }

    }
    private void Shoot()
    {
        if (Shootaudio != null)
            Shootaudio.Play();

        readyToShoot = false;

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
        if(!chasingMissile)
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
