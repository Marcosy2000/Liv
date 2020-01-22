using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class barra_naranja_horizontal : MonoBehaviour
{
    float velocidad = 700.0f;

    void Update()
    {
        float traslacionx = velocidad;
        traslacionx = traslacionx * Time.deltaTime;
        transform.Translate(traslacionx, 0, 0);



        if (transform.position.x >= 1057.0f)
        {
            velocidad = -700.0f;
        }
        else if (transform.position.x <= 477.5f)
        {

            velocidad = 700.0f;
        }



    }
}
