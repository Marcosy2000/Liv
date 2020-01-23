using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class sonido : MonoBehaviour
{
    //Bool 
    public static bool boolsoundhostia, boolsoundhostiapower;

    private bool OnnOff;

    //AudiSources
    public AudioClip FXhostia, FXhostiapower;

    void Awake()
    {
        //Music = GetComponent<AudioSource>();

        boolsoundhostia = false;
        boolsoundhostiapower = false;
    }

    void Start()
    {
        OnnOff = true;
    }

    void Update()
    {
        if (OnnOff)
        {
            // ######## SONIDOS DEL PLAYER ##########

            if (boolsoundhostia)
            {
                AudioSource.PlayClipAtPoint(FXhostia, transform.position, 0.6f);
                boolsoundhostia = false;
            }
            if (boolsoundhostiapower)
            {
                AudioSource.PlayClipAtPoint(FXhostiapower, transform.position, 0.8f);
                boolsoundhostiapower = false;
            }
        }

    }

}
