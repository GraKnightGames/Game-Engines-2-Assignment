using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ExplosionDestroy : MonoBehaviour
{
    private void Awake()
    {
        StartCoroutine(Alive());
    }
    IEnumerator Alive()
    {
        yield return new WaitForSeconds(2.0f);
        Destroy(gameObject);
    }
}
