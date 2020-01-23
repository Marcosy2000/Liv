using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Granjero : MonoBehaviour
{
    private bool inTrigger = false;

    //public List<int> availableQuestIDs = new List<int>();
    //public List<int> receivableQuestIDs = new List<int>();
    public Quest quest;

    public GameObject questMarker;
    public Image theImage;

    public Sprite questAvailableSprite;
    public Sprite questReceivableSprite;

    public Transform target;

    string activeSentence;
    public float typingSpeed;
    Queue<string> sentences;

    void Start()
    {
        sentences = new Queue<string>();
    }


    void Interactable()
    {
        if (inTrigger && Input.GetKeyDown(KeyCode.Space))
        {

            //LOOKING PLAYER
            if (QuestManager.questManager.talking)
            {
                Vector3 targetPosition = new Vector3(target.transform.position.x, transform.position.y, target.transform.position.z);
                transform.LookAt(targetPosition);
            }

            //DIALOGO
            if (!QuestUIManager.uiManager.startedConvers)
            {
                //QuestUIManager.uiManager.StartDialogue();
                print("entro aquí");

                QuestUIManager.uiManager.startedConvers = true;
            }
            else if (QuestUIManager.uiManager.startedConvers)
            {
                //QuestUIManager.uiManager.DisplayNextSentence();
            }
        }
    }

    public void StartDialogue()
    {
        sentences.Clear();

        foreach (string sentence in quest.description)
        {
            sentences.Enqueue(sentence);
        }

        DisplayNextSentence();
    }

    public void DisplayNextSentence()
    {
        if (sentences.Count <= 0)
        {
            QuestUIManager.uiManager.questDescription.text = activeSentence;
            StopAllCoroutines();
            print("Oculto el panel");
            QuestUIManager.uiManager.HideQuestPanel();
            QuestUIManager.uiManager.startedConvers = false;

            return;
        }

        activeSentence = sentences.Dequeue();
        QuestUIManager.uiManager.questDescription.text = activeSentence;

        StopAllCoroutines();
        StartCoroutine(TypeTheSentence(activeSentence));
    }

    IEnumerator TypeTheSentence(string sentence)
    {
        QuestUIManager.uiManager.questDescription.text = "";

        foreach (char letter in sentence.ToCharArray())
        {
            QuestUIManager.uiManager.questDescription.text += letter;
            //myAudio.PlayOneShot(speakSound);
            yield return new WaitForSeconds(typingSpeed);
        }
    }


    void OnTriggerStay(Collider other)
    {
        if (other.tag == "Player")
        {
            inTrigger = true;
            //QuestManager.questManager.talking = true;

            Interactable();
        }
    }

    void OnTriggerEnter(Collider other)
    {
        if (other.tag == "item" || other.tag == "arbol")
        {

        }
    }

    void OnTriggerExit(Collider other)
    {
        if (other.tag == "Player")
        {
            inTrigger = false;
            QuestUIManager.uiManager.HideQuestPanel();
            //Objective = "none";
            //QuestManager.questManager.talking = false;

            QuestUIManager.uiManager.startedConvers = false;
            QuestUIManager.uiManager.StopAllCoroutines();
        }
    }
}
