using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerControls : MonoBehaviour
{
    //inputs
    public Control controles;
    Vector2 inputs;
    public Transform mesh;
    public GameObject questWindows;

    //velocity
    public Vector3 velocity;
    public float speed = 6.0f;

    //interactive
    public static bool interactiveNPC, interactiveItem;
    public static bool withItem;

    //jumping
    bool jumping;
    float gravity = -18, velocityY, terminalVelocity = -25;
    Vector3 jumpDirection;

    //rotation
    public float smooth = 8;
    public float angulo;
    public Quaternion target;

    //component references
    CharacterController controller;

    public Text MonedasText;

    public Animator Hacha;

    void Start()
    {
        //Application.targetFrameRate = 60;

        controller = GetComponent<CharacterController>();
    }

    void Update()
    {
        if (!QuestUIManager.uiManager.Freeze)
        {
            GetInputs();
            Locomotion();
        }

        MonedasText.text = "Monedas: " + Interfaz.monedas;

        if (Interfaz.withAxe)
        {
            Hacha.SetBool("HachaConseguida", true);
        }
        else
        {
            Hacha.SetBool("HachaConseguida", false);
        }

    }

    void Locomotion()
    {
        Vector2 inputNormalized = inputs;

        //rotating
        Vector3 rotationVector = new Vector3(mesh.transform.rotation.x, angulo, transform.rotation.z);
        target = Quaternion.Euler(rotationVector);
        mesh.transform.rotation = Quaternion.Slerp(mesh.transform.rotation, target, Time.deltaTime * smooth);

        //gravity
         if (!controller.isGrounded && velocityY> terminalVelocity)
         {
             velocityY += gravity * Time.deltaTime;
         }

         //Applying inputs
         velocity = (transform.forward * inputNormalized.y + transform.right * inputNormalized.x) * speed + Vector3.up * velocityY;

         //moving controller
         controller.Move(velocity*Time.deltaTime);

    }

    void GetInputs()
    {
        //FORWARDS BACKWARDS CONTROLS
        //forwards
        if (Input.GetKey(controles.forwards))
        {
            if (Input.GetKey(controles.backwards))
            {
                inputs.y = Mathf.Lerp(inputs.y, 0, 0.25f);
            }
            else
            {
                angulo = 0;
                inputs.y = 1;
            }
        }

        //backwards
        if (Input.GetKey(controles.backwards))
        {
            if (Input.GetKey(controles.forwards))
            {
                inputs.y = Mathf.Lerp(inputs.y, 0, 0.25f);
            }
            else
            {
                angulo = 180;
                inputs.y =  -1;
            }
        }

        //ROTATE LEFT RIGHT
        //Rotate right
        if (Input.GetKey(controles.R_right))
        {
            if (Input.GetKey(controles.R_left))
            {
                inputs.x = Mathf.Lerp(inputs.x, 0, 0.25f);
            }
            else
            {
                angulo = 90;
                inputs.x = 1;
            }
        }

        //rotate left
        if (Input.GetKey(controles.R_left))
        {
            if (Input.GetKey(controles.R_right))
            {
                inputs.x = Mathf.Lerp(inputs.x, 0, 0.25f);
            }
            else
            {
                angulo = -90;
                inputs.x = -1;
            }
        }

        //FW nothing
        if (!Input.GetKey(controles.R_left) && !Input.GetKey(controles.R_right))
        {
            inputs.x = 0;
        }
        //FW nothing
        if ( !Input.GetKey(controles.forwards) && !Input.GetKey(controles.backwards))
        {
            inputs.y = 0;
        }

        //rotaciones diagonales
        if (Input.GetKey(controles.R_left) && Input.GetKey(controles.forwards))
        {
            angulo = -45;
        }
        if (Input.GetKey(controles.R_left) && Input.GetKey(controles.backwards))
        {
            angulo = -135;
        }
        if (Input.GetKey(controles.R_right) && Input.GetKey(controles.forwards))
        {
            angulo = 45;
        }
        if (Input.GetKey(controles.R_right) && Input.GetKey(controles.backwards))
        {
            angulo = 135;
        }

        //interact 
        if (interactiveItem && withItem == false)
        {
            if (Input.GetKeyDown(controles.interact))    // cojo objeto
            {

            }
            else
            {

            }
        }
        else if (interactiveItem && withItem)
        {
            if (Input.GetKeyDown(controles.interact))    // no puedes coger objeto
            {

            }
        }

        if (interactiveNPC)
        {
            if (Input.GetKeyDown(controles.interact) && withItem)    // cojo mision
            {

            }
            else if (Input.GetKeyDown(controles.interact) && !withItem)
            {

            }
        }


        if (Input.GetKeyDown(controles.interact) && withItem)   //Lanzar Objetos
        {

        }

    }

    public void OnTriggerExit(Collider other)
    {
        if (other.gameObject.tag == "Item" || other.gameObject.tag == "arbol")
        {
            interactiveItem = false;
        }
        if (other.gameObject.tag == "NPC" || other.gameObject.tag == "herrero" || other.gameObject.tag == "pescador" )
        {
            interactiveNPC = false;
            QuestManager.questManager.talking = false;
        }

    }

    public void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "Item" || other.gameObject.tag == "arbol")
        {
            interactiveItem = true;
        }
        if (other.gameObject.tag == "NPC" || other.gameObject.tag == "herrero" || other.gameObject.tag == "pescador")
        {
            QuestManager.questManager.talking = true;

            interactiveNPC = true;
        }

    }

}

