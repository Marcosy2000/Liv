using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MaderaRigid : MonoBehaviour
{
    //string del objeto
    private string objective;

    //fuerza para el addforce
    public float thrust = 1.0f;
    private Rigidbody rb;
    public GameObject target;

    //PARA COGER OBJETO
    private bool inTrigger = false;
    GameObject theDest;
    public bool onHead;

    //ES UN OBJETO QUE SE PUEDE COGER
    public bool takeable = true;

    //CONTROL DE BASURA, SI NO LA COGES EN 10 SEG DESAPARECE
    float timeToDestroy;

    private void Awake()
    {
        rb = GetComponent<Rigidbody>();
    }

    void Start()
    {
        timeToDestroy = Time.time;

        objective = "madera";
        target = GameObject.Find("Player");
        theDest = GameObject.Find("Player/liv/Destination");

        Vector3 direction = transform.position - target.transform.position;
        Vector3 newPos = transform.position + direction;

        rb.AddForce(direction * thrust * Time.deltaTime);
    }

    void Update()
    {
        float timeDif = Time.time - timeToDestroy;

        //COMPROBACION DE SI EL ITEM EN LA CABEZA COINCIDE CON EL OBJETIVO
        if (!QuestUIManager.uiManager.questPanelActive)
        {
            //animacion de subir objeto a la cabeza
            // bool de tengo en la cabeza algo

            for (int i = 0; i < QuestManager.questManager.currentQuestList.Count; i++)
            {
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
        }

        //LANZAMIENTO DE OBJETOS
        if (inTrigger && Input.GetKeyDown(KeyCode.Space) && !onHead && takeable && !ItemObject.withItem && !QuestManager.questManager.talking)
        {
            QuestManager.questManager.Objective = objective;

            //rotaciones a 0 y congelo todas las constraints
            rb.constraints = RigidbodyConstraints.FreezeAll;
            transform.rotation = Quaternion.Euler(0, 0, 0);

            //quito el collider y su gravedad
            //GetComponent<SphereCollider>().enabled = false;
            GetComponent<BoxCollider>().enabled = false;
            GetComponent<Rigidbody>().useGravity = false;

            //lo llevo a la position
            this.transform.position = theDest.transform.position;              //lo coloco en la posicion
            this.transform.parent = GameObject.Find("Destination").transform;  //lo hago hijo

            //control
            onHead = true;
            ItemObject.withItem = true;

        }

        else if (Input.GetKeyDown(KeyCode.Space) && onHead && takeable && ItemObject.withItem && !QuestManager.questManager.talking)
        {

            QuestManager.questManager.Objective = "null";

            //constraints desactivadas menos Xy Z
            rb.constraints = RigidbodyConstraints.None;
            rb.constraints = RigidbodyConstraints.FreezeRotationX | RigidbodyConstraints.FreezeRotationZ;

            //deja de ser hijo le doy gravedad, activo de nuevo su collider y le aplico una fuerza
            this.transform.parent = null;
            GetComponent<Rigidbody>().useGravity = true;
            //GetComponent<SphereCollider>().enabled = true;
            GetComponent<BoxCollider>().enabled = true;
            rb.AddForce(theDest.transform.forward * thrust * Time.deltaTime * 10);

            //control
            ItemObject.withItem = false;
            onHead = false;
        }

        if (onHead)
        {
            timeToDestroy = Time.time;
        }

        if (onHead && QuestManager.questManager.DestroyObHead)
        {
            Destroy(gameObject);
            QuestManager.questManager.DestroyObHead = false;
        }

        if (timeDif>=10)
        {
            Destroy(gameObject);
        }

    }

    void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "Player")
        {
            inTrigger = true;
        }
    }

    void OnTriggerExit(Collider other)
    {
        if (other.gameObject.tag == "Player")
        {
            inTrigger = false;
        }
    }
}
