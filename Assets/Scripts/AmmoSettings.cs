using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AmmoSettings : MonoBehaviour
{
    private Rigidbody r;
    private float creationTime;
    private Material material;

    [HideInInspector]
    public bool isReference = true;

    [Header("Ammo Settings")]
    public bool useGravity;
    public float timeToLive;


    // Start is called before the first frame update
    void Start()
    {
        if (isReference) {
            gameObject.SetActive(false);
        }

        r = GetComponent<Rigidbody>();
        r.useGravity = useGravity;
        creationTime = Time.time; 
    }

    // Update is called once per frame
    void Update()
    {
        if (!isReference && timeToLive < Time.time - creationTime) {
            Destroy(gameObject);
        }
    }
}
