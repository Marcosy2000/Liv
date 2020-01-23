using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class QuestObject : MonoBehaviour
{
    private bool inTrigger = false;

    public List<int> availableQuestIDs = new List<int>();
    public List<int> receivableQuestIDs = new List<int>();

    public GameObject questMarker;
    public Image theImage;

    public Sprite questAvailableSprite;
    public Sprite questReceivableSprite;

    public Transform target;

    void Start()
    {
        SetQuestMaker();
    }

    public void SetQuestMaker()
    {
        if (QuestManager.questManager.CheckCompletedQuests(this))
        {
            questMarker.SetActive(true);
            theImage.sprite = questReceivableSprite;
            theImage.color = Color.yellow;
        }
        else if (QuestManager.questManager.CheckAvailableQuests(this))
        {
            questMarker.SetActive(true);
            theImage.sprite = questAvailableSprite;
            theImage.color = Color.yellow;
        }
        else if (QuestManager.questManager.CheckAcceptedQuests(this))
        {
            questMarker.SetActive(true);
            theImage.sprite = questReceivableSprite;
            theImage.color = Color.gray;
        }
        else
        {
            questMarker.SetActive(false);
        }
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

            if (!QuestUIManager.uiManager.questPanelActive)
            {
                //quest ui manager
                QuestUIManager.uiManager.CheckQuests(this);
                QuestManager.questManager.QuestRequest(this);
            }

            //DIALOGO
            if (!QuestUIManager.uiManager.startedConvers)
            {
                QuestUIManager.uiManager.StartDialogue(this);

                QuestUIManager.uiManager.startedConvers = true;
            }
            else if (QuestUIManager.uiManager.startedConvers)
            {
                QuestUIManager.uiManager.DisplayNextSentence(this);
            }

        }

    }

    void OnTriggerStay(Collider other)
    {
        if (other.tag == "Player")
        {
            inTrigger = true;

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

            QuestUIManager.uiManager.startedConvers = false;
            QuestUIManager.uiManager.StopAllCoroutines();
        }

    }
}
