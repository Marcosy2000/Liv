using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovimientoPersonajeMCM : MonoBehaviour
{

    public float velocidadhorizontal;
    public float velocidadvertical;
    float h;
    float v;

    Rigidbody rBody;
    Vector3 direccion = Vector3.zero;





    void Start()
    {

        rBody = GetComponent<Rigidbody>();

    }


    void Update()
    {

        h = velocidadhorizontal * Input.GetAxis("Mouse X");
        v = velocidadvertical * Input.GetAxis("Mouse Y");

        transform.Rotate(0, h, 0);
    



        if (Input.GetKey(KeyCode.W))  // esto hace que puedas moverte cuando apretes una teclas
        {
            transform.Translate(0, 0, 4f * Time.deltaTime);
        }

        if (Input.GetKey(KeyCode.S))
        {
            transform.Translate(0, 0, -4f * Time.deltaTime);
        }

        if (Input.GetKey(KeyCode.A))
        {
            transform.Translate(-4f * Time.deltaTime, 0, 0);
        }

        if (Input.GetKey(KeyCode.D))
        {
            transform.Translate(4f * Time.deltaTime, 0, 0);
        }





    }

}
