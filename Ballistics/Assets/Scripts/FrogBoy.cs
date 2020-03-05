using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FrogBoy : MonoBehaviour
{
    public float myTimeScale = 1.0f;
    public float shotTimer = 0;
    public bool ballShot = false;
    public GameObject target;
    public float launchForce = 10f;
    Rigidbody rb;

    public void Start()
    {
        rb = GetComponent<Rigidbody>();
        rb.constraints = RigidbodyConstraints.FreezePosition;

        Time.timeScale = myTimeScale; // allow for slowing time to see whats up
    }
    // Start is called before the first frame update
    void Update()
    {
        FiringSolution fs = new FiringSolution();
        Nullable<Vector3> aimVector = fs.Calculate(transform.position,
            target.transform.position, launchForce, Physics.gravity);

        if (aimVector.HasValue && shotTimer == 50 && ballShot == false)
        {
            rb.constraints = RigidbodyConstraints.None;
            rb.AddForce(aimVector.Value.normalized
                * launchForce, ForceMode.VelocityChange);
            ballShot = true;
        }
        else
        {
            shotTimer++;
        }
    }
}
