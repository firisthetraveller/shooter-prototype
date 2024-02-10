using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Weapon : MonoBehaviour
{
    private float lastFireTime = 0;

    [Header("Ammo")]
    public GameObject ammoPrefab;
    public float fireRatePerSecond;
    public float launchForce;
    private float fireInterval;

    // Start is called before the first frame update
    void Start()
    {
        fireInterval = 1f / fireRatePerSecond;
    }

    public void Fire(Vector3 aimDirection, Vector3 position)
    {
        float currentTime = Time.time;
        // Debug.Log("Fire attempt");
        if (currentTime - lastFireTime > fireInterval)
        {
            GameObject ammo = Instantiate(ammoPrefab);
            ammo.transform.position = new(position.x, position.y, position.z);
            ammo.SetActive(true);
            ammo.GetComponent<AmmoSettings>().isReference = false;
            ammo.GetComponent<Rigidbody>().AddForce(aimDirection * launchForce);
            lastFireTime = currentTime;
        }
    }
}
