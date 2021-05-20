using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Ammunition : MonoBehaviour
{
    public int maxAmmo = 16;
    private int currentAmmo;
    public float reloadTime = 1f;
    private bool isReloading = false;
    public Text ammoDisplay;
    public float damage = 25f;
    public float range = 100f;
    public AudioSource barrelSound;
    public ParticleSystem muzzleFlash;
    public Camera fpsCam;
    public GameObject impactEffect;
    public PlayerStats player;
    

    // Start is called before the first frame update
    void Start()
    {
        currentAmmo = maxAmmo;
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetButtonDown("Fire1"))
        {
            Shoot();
        }

        ammoDisplay.text = currentAmmo.ToString();
        
        if (isReloading)
            return;

        if (currentAmmo<= 0 )
        {
            StartCoroutine(Reload());
            return;
        }
    }

    IEnumerator Reload()
    {
        isReloading = true;

        Debug.Log("Reloading...");

        yield return new WaitForSeconds(reloadTime);

        currentAmmo = maxAmmo;

        isReloading = false;
    }

    void Shoot()
    {
        barrelSound.Play();
        muzzleFlash.Play();
        RaycastHit hit;
        player.totalShots++;
        if (Physics.Raycast(fpsCam.transform.position, fpsCam.transform.forward, out hit, range))
        {
            Debug.Log(hit.transform.name);

            Damage target = hit.transform.root.GetComponent<Damage>();
            if (target != null)
            {
                target.TakeDamage(damage);
                player.shotsHit++;
            }
            else
            { 
                GameObject newHole = Instantiate(impactEffect, (hit.point + hit.normal * .001f), Quaternion.LookRotation(hit.normal));
                Destroy(newHole, 20f);
            }

        }
        
        currentAmmo--;
    }

}
