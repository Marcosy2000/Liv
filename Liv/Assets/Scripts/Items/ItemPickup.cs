using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemPickup : MonoBehaviour
{
    public Rigidbody rb;
    public float thrust = 20000;
    public Transform theDest;

    public bool onHead;

    public Control controles;

    [SerializeField]
    bool takeable = false;
    [SerializeField]
    bool itemPicked;

    void Start()
    {
        thrust *= Time.deltaTime;
    }

    void Update()
    {
        //bool wasPickedUp = Inventory.instance.Add(item);

        if (Input.GetKeyDown(controles.interact) && takeable && !PlayerControls.withItem && !onHead && PlayerControls.interactiveItem && itemPicked)
        {
            //Debug.Log("Te estoy puto cogiendo");

            //rotaciones a 0 y congelo todas las constraints
            rb.constraints = RigidbodyConstraints.FreezeAll;
            transform.rotation = Quaternion.Euler(0, 0, 0);

            //quito el collider y su gravedad
            GetComponent<SphereCollider>().enabled = false;
            GetComponent<Rigidbody>().useGravity = false;

            //lo llevo a la position
            this.transform.position = theDest.position;
            this.transform.parent = GameObject.Find("Destination").transform; //lo hago hijo

            //control
            PlayerControls.withItem = true;
            onHead = true;
            PlayerControls.interactiveItem = false;
            itemPicked = false;
        }
        else if (Input.GetKeyDown(controles.interact) && PlayerControls.withItem && onHead)
        {
            //Debug.Log("Lanzo un objeto");

            //constraints desactivadas menos Xy Z
            rb.constraints = RigidbodyConstraints.None;
            rb.constraints = RigidbodyConstraints.FreezeRotationX | RigidbodyConstraints.FreezeRotationZ;

            //deja de ser hijo le doy gravedad, activo de nuevo su collider y le aplico una fuerza
            this.transform.parent = null;
            GetComponent<Rigidbody>().useGravity = true;
            GetComponent<SphereCollider>().enabled = true;
            rb.AddForce(theDest.transform.forward * thrust);

            //control
            PlayerControls.withItem = false;

            onHead = false;
        }

        /* if (wasPickedUp)
             {
                 //GetComponent<BoxCollider>().enabled = false;
                 //this.transform.position = theDest.position;
                 //this.transform.parent = GameObject.Find("Destination").transform;

                 //Destroy(gameObject);

                 //Vector3 pos = new Vector3(player.transform.position.x, player.transform.position.y + 2, player.transform.position.z);
                 //GameObject clone = Instantiate(gameObject, pos, Quaternion.identity) as GameObject;
                // clone.SetActive(true);
                 //rb.AddForce(transform.forward * thrust);
             }*/


        /* }

         void OnTriggerEnter(Collider other)
         {
             if (other.gameObject.tag == "Player")
             {
                 takeable = true;
             }
         }

         private void OnTriggerExit(Collider other)
         {
             if (other.gameObject.tag == "Player")
             {
                 takeable = false;
             }
         }*/
    }
}
