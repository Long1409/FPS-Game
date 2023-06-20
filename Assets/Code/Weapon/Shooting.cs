
using UnityEngine;
using TMPro;

public class Shooting : MonoBehaviour
{
    public GameObject bullet;
    public float shootForce, upwardForce;
    public float timeBetweenShooting, spread, reloadTime, timeBetweenShot;
    public int magazineSize, bulletperTap;
    int bulletLeft, bulletShot;
    public bool shooting, readyToShoot, reloading;

    public Rigidbody playerRb;
    public float recoilForce;

    public Camera fpsCam;
    public Transform attackPoint;
    public Transform GunHolder;
    public Transform Gun;

    public GameObject muzzleFlash;
    public TextMeshProUGUI ammunitionDisplay;

    public bool allowInvoke = true;

    private void Awake()
    {
        bulletLeft = magazineSize;
        readyToShoot = false;
    }

    private void Update()
    {
        if(Gun.transform.parent == GunHolder.transform) readyToShoot = true;
        else readyToShoot=false;
        MyInput();
        if (ammunitionDisplay != null)
            ammunitionDisplay.SetText(bulletLeft / bulletperTap + "/" + magazineSize / bulletperTap);
    }

    private void MyInput()
    {
        
        shooting = Input.GetKey(KeyCode.Mouse0);
        if (Input.GetKeyDown(KeyCode.R) && bulletLeft < magazineSize && !reloading) Reload();
        if(readyToShoot && shooting && !reloading && bulletLeft <= 0) Reload();
        if(readyToShoot && shooting && !reloading && bulletLeft > 0) 
        {
            bulletShot = 0;
            Shoot();
        }
    }

    private void Shoot()
    {
        readyToShoot = false;

        Ray ray = fpsCam.ViewportPointToRay(new Vector3(0.5f, 0.5f, 0));
        RaycastHit hit;
        Vector3 targetpoint;

        if(Physics.Raycast(ray, out hit))
            targetpoint = hit.point;
        else targetpoint = ray.GetPoint(75);

        Vector3 directionWithoutSpread = targetpoint - attackPoint.position;

        float x = Random.Range(-spread, spread);
        float y = Random.Range(-spread, spread);

        Vector3 directionWithSpread = directionWithoutSpread + new Vector3(x, y, 0);

        GameObject currentBullet = Instantiate(bullet, attackPoint.position, Quaternion.identity);

        currentBullet.transform.forward = directionWithSpread.normalized;

        currentBullet.GetComponent<Rigidbody>().AddForce(directionWithSpread.normalized * shootForce, ForceMode.Impulse);
        currentBullet.GetComponent<Rigidbody>().AddForce(fpsCam.transform.up * upwardForce, ForceMode.Impulse);


        if(muzzleFlash != null)
            Instantiate(muzzleFlash, attackPoint.position, Quaternion.identity);

        bulletLeft--;
        bulletShot++;

        if(allowInvoke)
        {
            Invoke("ResetShot", timeBetweenShooting);
            allowInvoke = false;

            playerRb.AddForce(-directionWithSpread.normalized * recoilForce, ForceMode.Impulse);
        }

        if(bulletShot < bulletperTap && bulletLeft > 0)
        {
            Invoke("Shoot", timeBetweenShooting); 
        }
    }

    private void ResetShot()
    {
        readyToShoot = true;
        allowInvoke = true;
    }

    private void Reload()
    {
        reloading = true;
        Invoke("ReloadFinished", reloadTime);
    }

    private void ReloadFinished()
    {
        bulletLeft = magazineSize;
        reloading = false;
    }
}
