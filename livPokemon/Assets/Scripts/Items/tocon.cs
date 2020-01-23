using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class tocon : MonoBehaviour
{
    public GameObject arbol;

    void OnBecameInvisible()
    {
        Destroy(gameObject);

        Vector3 pos = new Vector3(transform.position.x, 1, transform.position.z);
        GameObject clone = Instantiate(arbol, pos, Quaternion.identity) as GameObject;
        clone.SetActive(true);

    }
}
