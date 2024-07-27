using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class efektDestroy : MonoBehaviour
{
    public float destroyTime; // Yok edilecek s�re

    private void Start()
    {
        StartCoroutine(DestroyAfterSeconds());
    }

    IEnumerator DestroyAfterSeconds()
    {
        yield return new WaitForSeconds(destroyTime);
        Destroy(gameObject);

    }
}
