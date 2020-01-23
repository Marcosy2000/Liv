using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Arbol : MonoBehaviour
{

    public GameObject tocon;
    public GameObject arbol;
    public GameObject target;
    public GameObject madera;

    private int golpes;

    private float tiempoBase;
    private float timeDif;
    public float tiempoEntreGolpes;

    public float thrust = 1.0f;
    public Rigidbody rb;

    void Golpea()
    {
        timeDif = Time.time - tiempoBase;

        if (Input.GetKeyDown(KeyCode.Space) && timeDif >= tiempoEntreGolpes)
        {
            golpes++;
            tiempoBase = Time.time;
        }

        if (golpes >=3)
        {
            Destroy(gameObject);
            //Destroy(gameObject);
            Vector3 posTocon = new Vector3(transform.position.x, 0, transform.position.z);
            Vector3 rotation = new Vector3(-90, 0, -180);
            Quaternion rotationClone = Quaternion.Euler(rotation);
            GameObject cloneTocon = Instantiate(tocon, posTocon, rotationClone);
            //transform.rotation = 
            cloneTocon.SetActive(true);

            golpes = 0;

            Vector3 pos = new Vector3(transform.position.x, 2, transform.position.z);
            GameObject clone = Instantiate(madera, pos, Quaternion.identity) as GameObject;
            clone.SetActive(true);
        }
    }



    private void OnTriggerStay(Collider other)
    {
        if (other.gameObject.tag == "Player")
        {
            if (Interfaz.withAxe)
            {
                Golpea();
            }
            else
            {
                print("Te falta un hacha");
            }
        }
    }


    void OnBecameInvisible()
    {
        //Debug.Log("KKKKKKKK");
        //Destroy(this.gameObject);

    }


}
