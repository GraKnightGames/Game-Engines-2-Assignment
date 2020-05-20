using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LaserPosition : MonoBehaviour
{
    private LineRenderer lr;
    public Transform targ;
    public Transform origin;
    public bool firing;
    public bool exploded;
    public GameObject explosionPrefab;
    // Start is called before the first frame update
    void Start()
    {
        lr = GetComponentInChildren<LineRenderer>();
        origin = GetComponentInParent<Transform>();
        exploded = false;
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
            lr.SetPosition(0, origin.position);
            lr.SetPosition(1, targ.position);
            if(exploded == false)
            {
                Instantiate(explosionPrefab, targ.position, targ.rotation);
                exploded = true;
            }
            else
            {

            }
        }
        else if (!firing)
        {
            lr.enabled = false;
            exploded = false;
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
