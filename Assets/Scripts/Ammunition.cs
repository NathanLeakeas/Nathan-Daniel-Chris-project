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

    // Start is called before the first frame update
    void Start()
    {
        currentAmmo = maxAmmo;
    }

    // Update is called once per frame
    void Update()
    {
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
        currentAmmo--;
    }

}
