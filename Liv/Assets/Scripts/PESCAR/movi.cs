using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class movi : MonoBehaviour
{
    float timeAux;
    public static float velocidad = 900.0f;
    bool movercaja = true;
    public Text PecesTexto;
    public Text monedasTexto;
    public Text jugarTexto;
    public Text NoMonedasTexto;
    int peces = 0;
    int monedas = 50;
    bool jugar = false;
    int inicio = 0;
    int reseteo = 0;
    int nomonedas = 0;


    void Start()
    {
        PecesTexto.text = "= " + peces;
        monedasTexto.text = "= " + monedas;
        timeAux = Time.time;
        NoMonedasTexto.enabled = false;
        jugarTexto.enabled = true;
    }


    void Update()
    {


        if (Input.GetKeyDown(KeyCode.E)) // te resta las monedas y se mueve la barra y todo
        {
            if (reseteo == 0)
            {
                monedas = monedas - 10;
                monedasTexto.text = "= " + monedas;
                jugarTexto.enabled = false;
                jugar = true;
                velocidad = 900.0f;
                movercaja = true;
                reseteo = 1;
                nomonedas = 1;


            }
        }

        if (jugar)
        {

            float traslacionx = velocidad;
            traslacionx = traslacionx * Time.deltaTime;
            transform.Translate(traslacionx, 0, 0);

            float timeDif = Time.time - timeAux;

            if (Input.GetKeyDown(KeyCode.Space)) // si pulsas espacio la barra que se mueve se para 
            {
                movercaja = false;
                velocidad = 0;
                reseteo = 0;
                nomonedas = 0;
                jugarTexto.enabled = true;

                if (monedas <= 0)  // si no tienes monedas sale el texto de no tienes monedas y no te deja volver a jugar
                {
                    reseteo = 1;
                    nomonedas = 0;
                    jugarTexto.enabled = false;
                    NoMonedasTexto.enabled = true;
                }
              
            }

            if (movercaja)  //Para que se mueva la barra
            {


                if (transform.position.x >= 1060.5f)
                {
                    velocidad = -900.7f;
                }
                else if (transform.position.x <= 478.5f)
                {
                    velocidad = 900.7f;
                }

            }

        }
       

    }
    
    private void OnTriggerStay(Collider collision)
    {
        if (nomonedas == 1)
        {
            if (collision.gameObject.tag == "gana" && Input.GetKeyDown(KeyCode.Space))
            {
                peces = peces + 1;
                PecesTexto.text = "= " + peces;

            }
            if (collision.gameObject.tag == "gana_naranja" && Input.GetKeyDown(KeyCode.Space))
            {
                peces = peces + 3;
                PecesTexto.text = "= " + peces;

            }
        }

    }
    

}
