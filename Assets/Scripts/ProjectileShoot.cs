using UnityEngine;
using TMPro;

public class ProjectileShoot : MonoBehaviour
{
    //Bullet
    public GameObject bullet;

    //Bullet Force
    public float shootForce, upwardForce;

    //Player stats
    public float timeBetweenShooting, spread, reloadTime, timeBetweenShoots;
    public int magazineSize, bulletsPerTap;
    public bool allowButtonHold;
    int bulletsLeft, bulletsShot;

    // Bools
    bool shooting, readyToShot, reloading;

    // Reference (CAMBIAR LO DEL VÍDEO (CAMARA) A PLAYER)
    public Camera camera;
    public Transform attackPoint;

    // Bug fixing
    public bool allowInvoke = true;

    private void Awake()
    {
        // Magazine full (será infinito xd)
        bulletsLeft = magazineSize;
        readyToShot = true;
    }
    void Update()
    {
        MyInput();
    }

    private void MyInput()
    {
        // Check if allowed to hold shoot button
        if (allowButtonHold) shooting = Input.GetKey(KeyCode.LeftArrow);
        else shooting = Input.GetKeyDown(KeyCode.LeftArrow);

        // Shooting
        if (readyToShot && shooting && !reloading && bulletsLeft > 0)
        {
            // Set bullets shot to 0
            bulletsShot = 0;

            Shoot();
        }
    }

    private void Shoot()
    {
        readyToShot = false;

        // Find the exact hit position using raycast
        //Ray ray = camera

        bulletsLeft--;
        bulletsShot++;
    }
}
