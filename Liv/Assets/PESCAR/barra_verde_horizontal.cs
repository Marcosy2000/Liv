using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class barra_verde_horizontal : MonoBehaviour
{
    float velocidad = 400.0f;

    void Update()
    {
        float traslacionx = velocidad;
        traslacionx = traslacionx * Time.deltaTime;
        transform.Translate(traslacionx, 0, 0);



        if (transform.position.x >= 1020.0f)
        {
            velocidad = -400.0f;
        }
        else if (transform.position.x <= 512.5f)
        {

            velocidad = 400.0f;
        }



    }
}
