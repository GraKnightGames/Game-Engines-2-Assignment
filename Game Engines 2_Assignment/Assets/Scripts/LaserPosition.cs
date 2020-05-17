using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LaserPosition : MonoBehaviour
{
    private LineRenderer lr;
    public Transform targ;
    public bool firing;
    // Start is called before the first frame update
    void Start()
    {
        lr = GetComponentInChildren<LineRenderer>();
    }

    // Update is called once per frame
    void Update()
    {
        if (targ = null)
        {
        }
        else
        {
        }
        if (firing)
        {
            targ = GameObject.FindGameObjectWithTag("LaserTarget").GetComponent<Transform>();
            lr.enabled = true;
            lr.SetPosition(0, transform.position);
            lr.SetPosition(1, targ.position);
        }
        else if (!firing)
        {
            lr.enabled = false;
        }
    }
    public void Fire()
    {
        firing = true;
    }
    public void StopFiring()
    {
        firing = false;
    }
}
