using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemObject : MonoBehaviour
{
    //public string[] objective;
    public string objective;

    private bool inTrigger = false;

    //PARA COGER OBJETO
    public Rigidbody rb;
    public float thrust = 20000;
    public Transform theDest;
    public GameObject anguila;
    public bool onHead;

    //public Control controles;

    //ES UN OBJETO QUE SE PUEDE COGER
    public bool takeable = false;
    public static bool withItem = false;

    //DENTRO DEL RANGO QUE SE PUEDE COGER
    //[SerializeField]
    //bool rangePicked;

    void Start()
    {
        if (QuestManager.questManager.Objective == "anguilas")
        {
            //Vector3 pos = new Vector3(theDest.transform.position.x, theDest.transform.position.y, theDest.transform.position.z);
            // GameObject clone = Instantiate(anguila, pos, Quaternion.identity) as GameObject;

            anguila.SetActive(true);

            onHead = true;
            withItem = true;
        }
    }

    void Update()
    {
        //print("talking: " + QuestManager.questManager.talking);
        //print("withItem: " + withItem);
        //print("onHead: " + onHead);


        //COMO NO ES TAKEABLE EL NPC, NO COGE LA STRING DEL NPC, EN ESTE CASO DEBERÉ HACER POR CÓDIGO LAS MISIONES DE COMPLETAR HABLANDO
        if (inTrigger && Input.GetKeyDown(KeyCode.Space) && !onHead && takeable && !withItem && !QuestManager.questManager.talking) 
        {
            QuestManager.questManager.Objective = objective;

            //Debug.Log("String del objeto: " + QuestManager.questManager.Objective);

            //rotaciones a 0 y congelo todas las constraints
            rb.constraints = RigidbodyConstraints.FreezeAll;
            transform.rotation = Quaternion.Euler(0, 0, 0);

            //quito el collider y su gravedad
            //GetComponent<SphereCollider>().enabled = false;
            GetComponent<BoxCollider>().enabled = false;
            GetComponent<Rigidbody>().useGravity = false;

            //lo llevo a la position
            this.transform.position = theDest.position;                        //lo coloco en la posicion
            this.transform.parent = GameObject.Find("Destination").transform;  //lo hago hijo

            //control
            onHead = true;
            withItem = true;


            if (!QuestUIManager.uiManager.questPanelActive)
            {
                //animacion de subir objeto a la cabeza
                // bool de tengo en la cabeza algo

                for (int i = 0; i < QuestManager.questManager.currentQuestList.Count; i++)
                {
   
                    if (QuestManager.questManager.currentQuestList[i].id == 5 && QuestManager.questManager.currentQuestList[i].progress == Quest.QuestProgress.ACCEPTED && gameObject.tag == "herrero" && Interfaz.monedas >= 10)
                    {
                        //Si tengo 10 monedas el herrero me arreglará la olla
                        //Interfaz.monedas -= 10;
                        QuestManager.questManager.Objective = "consigue oro";

                    }
                    if (Interfaz.withAxe && gameObject.tag == "arbol")
                    {
                        //Debug.Log("estoy talando");
                        //La madera se puede coger
                        QuestManager.questManager.Objective = "madera";
                    }


                    //COMPROBACION DE SI EL ITEM EN LA CABEZA COINCIDE CON EL OBJETIVO
                    if (QuestManager.questManager.Objective == QuestManager.questManager.currentQuestList[i].questObjective && QuestManager.questManager.currentQuestList[i].progress == Quest.QuestProgress.ACCEPTED)
                    {
                        print("objetos coincidentes");
                        QuestManager.questManager.itemCorrecto = true;
                    }
                    else
                    {
                        QuestManager.questManager.itemCorrecto = false;
                    }
                }


                if (QuestManager.questManager.questList[12].progress == Quest.QuestProgress.DONE)
                {
                    //Debug.Log("mision del pescadero completa, ya puedo pescar");

                }

            }
        }

        else if (Input.GetKeyDown(KeyCode.Space) && onHead && takeable && withItem && !QuestManager.questManager.talking)
        {

            QuestManager.questManager.Objective = "null";
            //Debug.Log("String del objeto: " + QuestManager.questManager.Objective);


            //constraints desactivadas menos Xy Z
            rb.constraints = RigidbodyConstraints.None;
            rb.constraints = RigidbodyConstraints.FreezeRotationX | RigidbodyConstraints.FreezeRotationZ;

            //deja de ser hijo le doy gravedad, activo de nuevo su collider y le aplico una fuerza
            this.transform.parent = null;
            GetComponent<Rigidbody>().useGravity = true;
            //GetComponent<SphereCollider>().enabled = true;
            GetComponent<BoxCollider>().enabled = true;
            rb.AddForce(theDest.transform.forward * thrust * Time.deltaTime);

            //control
            withItem = false;
            onHead = false;
        }
    }


    void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "Player")
        {
            inTrigger = true;
            //rangePicked = true;
        }
    }

    void OnTriggerExit(Collider other)
    {
        if (other.gameObject.tag == "Player")
        {
            inTrigger = false;
            //rangePicked = false;
        }
    }
}
