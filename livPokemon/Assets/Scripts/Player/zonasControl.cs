using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class zonasControl : MonoBehaviour
{
    public GameObject cartel;
    Animator anim;

    void Start()
    {

        anim = cartel.GetComponent<Animator>();
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "puerto")
        {
            anim.Play("cartel");
        }
        else if (other.gameObject.tag == "ayuntamiento")
        {
            anim.Play("cartel");
        }
        else if (other.gameObject.tag == "huerto")
        {
            anim.Play("cartel");
        }

    }
}
