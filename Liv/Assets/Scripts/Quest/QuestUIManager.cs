using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class QuestUIManager : MonoBehaviour
{
    public static QuestUIManager uiManager;

    //BOOLS
    public bool questAvailable = false;
    public bool questRunning = false;
    public bool questPanelActive = false;
    private bool questLogPanelActive = false;
    private bool MenuGameActive = false;
    private bool AjustesGameActive = false;
    private bool ConfirmationGameActive = false;
    private bool TutorialControllerActive = false;

    public bool startedConvers = false;
    public bool Freeze = false;
    public bool startGame = false;

    //PANELS
    public GameObject questPanel;
    public GameObject questLogPanel;
    public GameObject MenuGame;
    public GameObject AjustesGame;
    public GameObject ConfirmationGame;
    public GameObject TutorialController;

    //QUESTOBJECT
    private QuestObject currentQuestObject;

    //QUESTLISTS
    public List<Quest> availableQuests = new List<Quest>();
    public List<Quest> activeQuests = new List<Quest>();

    //BUTTONS
    public GameObject qButton;
    public GameObject qLogButton;
    private List<GameObject> qButtons = new List<GameObject>();

    public GameObject acceptButton;
    public GameObject deliverButton;
    public GameObject completeButton;

    //SPACER
    public Transform qButtonSpacer1;
    public Transform qButtonSpacer2;
    public Transform qLogButtonSpacer;

    //QUEST INFO
    public Text questTitle;
    public Text questDescription;
    public Text questSummary;

    //QUEST LOG INFO
    public Text questLogTitle;
    public Text questLogDescription;
    public Text questLogSummary;

    public QButtonScript acceptButtonScript;
    public QButtonScript deliverButtonScript;
    public QButtonScript completeButtonScript;
 
    //DIALOGO
    Queue<string> sentences;
    string activeSentence;
    public float typingSpeed;
    int questID;

    //AudioSource myAudio;
    //public AudioClip speakSound;

    //SCENES    0 = Menu Princial / 1 = Escena principal de juego / 2 = escena de minijuegos
    public int sceneNumber = 0;  

    void Awake()
    {
        if (uiManager == null)
        {
            uiManager = this;
        }
        else if (uiManager != this)
        {
            Destroy(gameObject);
        }
        DontDestroyOnLoad(gameObject);

        HideQuestPanel();
    }


    void Start()
    {
        acceptButtonScript = acceptButton.GetComponent<QButtonScript>();

        deliverButtonScript = deliverButton.GetComponent<QButtonScript>();

        completeButtonScript = completeButton.GetComponent<QButtonScript>();

        sentences = new Queue<string>();
        //myAudio = GetComponent<AudioSource>();

        acceptButton.SetActive(false);
        deliverButton.SetActive(false);
        completeButton.SetActive(false);

    }
    

    void Update()
    {
        //TUTORIAL
        if (startGame)
        {
            TutorialControllerActive = true;
            StartGame();
        }
        if (!startGame)
        {
            TutorialControllerActive = false;
            StartGame();
        }

        if (sceneNumber == 1)
        {
            if (Input.GetKeyDown(KeyCode.Escape) && !questLogPanelActive && !AjustesGameActive && !ConfirmationGameActive && !questPanelActive)
            {
                VolverMenuGame();
            }
            if (Input.GetKeyDown(KeyCode.Escape) && questLogPanelActive)
            {
                ListaMisiones();

                VolverMenuGame();
            }
            if (Input.GetKeyDown(KeyCode.Escape) && AjustesGameActive)
            {
                AjustesPanel();

                VolverMenuGame();
            }
            if (Input.GetKeyDown(KeyCode.Escape) && ConfirmationGameActive)
            {
                ConfirmationPanel();

                VolverMenuGame();
            }
            if (Input.GetKeyDown(KeyCode.Escape) && questPanelActive)
            {
                HideQuestPanel();
            }
        }
    }

    public void StartGame()
    {
        if (TutorialControllerActive)
        {
            TutorialController.SetActive(TutorialControllerActive);

            Time.timeScale = 0.0f;
            Freeze = true;
        }
        else
        {
            TutorialController.SetActive(TutorialControllerActive);

            Time.timeScale = 1.0f;
            Freeze = false;
        }
    }



    public void VolverMenuGame()
    {
        if (!MenuGameActive)
        {
            Time.timeScale = 0.0f;
            Freeze = true;
        }
        else
        {
            Time.timeScale = 1.0f;
            Freeze = false;
        }

        MenuGameActive = !MenuGameActive;
        MenuGame.SetActive(MenuGameActive);
    }

    public void ListaMisiones()
    {
        if (!questLogPanelActive)
        {
            Freeze = true;
            Time.timeScale = 0.0f;
        }
        else
        {
            Freeze = false;
            Time.timeScale = 1.0f;
        }

        questLogPanelActive = !questLogPanelActive;
        ShowQuestLogPanel();
    }

    public void AjustesPanel()
    {
        if (!AjustesGameActive)
        {
            Freeze = true;
            Time.timeScale = 0.0f;
        }
        else
        {
            Freeze = false;
            Time.timeScale = 1.0f;
        }

        //SCRIPT MARCOS
       /* public AudioMixer audioMixer;

        public void AjusteVolumen(float volumen) //SUBIR Y BAJAR EL VOLUMEN
        {
            audioMixer.SetFloat("volumen", volumen);
        }

        public void Mutear() //MUTEAR LA MUSICA
        {
            AudioListener.pause = !AudioListener.pause;
        }
    */

        AjustesGameActive = !AjustesGameActive;
        AjustesGame.SetActive(AjustesGameActive);
    }

    public void ConfirmationPanel()
    {
        if (!ConfirmationGameActive)
        {
            Time.timeScale = 0.0f;
            Freeze = true;
        }
        else
        {
            Time.timeScale = 1.0f;
            Freeze = false;
        }

        ConfirmationGameActive = !ConfirmationGameActive;
        ConfirmationGame.SetActive(ConfirmationGameActive);
    }

    //CALLED FROM QUEST OBJECT
    public void CheckQuests (QuestObject questObject)
    {
        currentQuestObject = questObject;
        QuestManager.questManager.QuestRequest(questObject);

        if ((questRunning || questAvailable) && !questPanelActive)
        {
            ShowQuestPanel();
        }
        else
        {
            Debug.Log("No quests available");
        }
    }

    //SHOW PANEL
    public void ShowQuestPanel()
    {
        acceptButton.SetActive(false);
        deliverButton.SetActive(false);
        completeButton.SetActive(false);

        questPanelActive = true;
        questPanel.SetActive(questPanelActive);

        //FILL IN DATA
        FillQuestButtons();
    }

    public void ShowQuestLogPanel()
    {
        questLogPanel.SetActive(questLogPanelActive);

        if (questLogPanelActive && !questPanelActive)
        {
            foreach (Quest curQuest in QuestManager.questManager.currentQuestList)
            {
                GameObject questButton = Instantiate(qLogButton);
                QLogButtonScript qbutton = questButton.GetComponent<QLogButtonScript>();

                qbutton.questID = curQuest.id;
                qbutton.questTitle.text = curQuest.title;

                questButton.transform.SetParent(qLogButtonSpacer, false);
                qButtons.Add(questButton);
            }
        }
        else if (!questLogPanelActive && !questPanelActive)
        {
            HideQuestLogPanel();
        }

    }

    public void ShowQuestLog(Quest activeQuest)
    {
        questLogTitle.text = activeQuest.title;
        
        if (activeQuest.progress==Quest.QuestProgress.ACCEPTED)
        {
            //questLogDescription.text = activeQuest.hint;
            questLogSummary.text = activeQuest.questObjective + " : " + activeQuest.questObjectiveCount + " / " + activeQuest.questObjectiveRequirement;
        }
        else if (activeQuest.progress ==Quest.QuestProgress.COMPLETE)
        {
            //questLogDescription.text = activeQuest.congratulation;
            questLogSummary.text = activeQuest.questObjective + " : " + activeQuest.questObjectiveCount + " / " + activeQuest.questObjectiveRequirement;
        }
    }

    //HIDE QUEST PANEL
    public void HideQuestPanel()
    {
        questPanelActive = false;
        questAvailable = false;
        questRunning = false;

        //CLEAR TEXT
        questTitle.text = " ";
        questDescription.text = " ";
        questSummary.text = " ";

        //CLEAR LISTS
        availableQuests.Clear();
        activeQuests.Clear();

        //CLEAR BUTTON LIST
        for (int i = 0; i < qButtons.Count; i++)
        {
            Destroy(qButtons[i]);
        }
        qButtons.Clear();

        //HIDE PANEL
        questPanel.SetActive(questPanelActive);
    }

    //HIDE QUEST LOG PANEL
    public void HideQuestLogPanel()
    {
        Time.timeScale = 1.0f;

        questLogPanelActive = false;

        questLogTitle.text = "";
        questLogDescription.text = "";
        questLogSummary.text = "";

        //CLEAR BUTTON LIST
        for (int i = 0; i<qButtons.Count; i++)
        {
            Destroy(qButtons[i]);
        }
        qButtons.Clear();
        questLogPanel.SetActive(questLogPanelActive);
    }

    //FILL BUTTONS FOR QUEST PANEL  fijar botones en el panel
    void FillQuestButtons()
    {
        foreach(Quest availableQuest in availableQuests)
        {
            GameObject questButton = Instantiate(qButton);
            QButtonScript qBScript = questButton.GetComponent<QButtonScript>();

            qBScript.questID = availableQuest.id;
            qBScript.questTitle.text = availableQuest.title;

            questButton.transform.SetParent(qButtonSpacer1, false);
            qButtons.Add(questButton);
        }

        foreach (Quest activeQuest in activeQuests)
        {
            GameObject questButton = Instantiate(qButton);
            QButtonScript qBScript = questButton.GetComponent<QButtonScript>();

            qBScript.questID = activeQuest.id;
            qBScript.questTitle.text = activeQuest.title;

            questButton.transform.SetParent(qButtonSpacer2, false);
            qButtons.Add(questButton);
        }
    }

    //SHOW QUEST ON BUTTON PRESS IN QUEST PANEL
    public void ShowSelectedQuest (int questID)
    {
        for (int i = 0; i < availableQuests.Count; i++)
        {
            if (availableQuests[i].id == questID)
            {
                questTitle.text = availableQuests[i].title;
                //questTitle.text = "PRUEBA";
                if (availableQuests[i].progress == Quest.QuestProgress.AVAILABLE)
                {
                    /*if (startedConvers)
                    {
                        DisplayNextSentence();
                    }
                    if (!startedConvers)
                    {
                        StartDialogue();
                        startedConvers = true;
                    }*/

                    //questDescription.text = availableQuests[i].description[i];
                    //questSummary.text = availableQuests[i].questObjective + " : " + availableQuests[i].questObjectiveCount + " / " + availableQuests[i].questObjectiveRequirement;
                }
            }
        }

        for (int i = 0; i < activeQuests.Count; i++)
        {
            if (activeQuests[i].id == questID)
            {
                questTitle.text = activeQuests[i].title;
                if(activeQuests[i].progress == Quest.QuestProgress.ACCEPTED)
                {
                    //questDescription.text = activeQuests[i].hint;
                    questSummary.text = activeQuests[i].questObjective + " : " + activeQuests[i].questObjectiveCount + " / " + activeQuests[i].questObjectiveRequirement;
                }
                else if (activeQuests[i].progress == Quest.QuestProgress.COMPLETE)
                {
                    //questDescription.text = activeQuests[i].congratulation;
                    questSummary.text = activeQuests[i].questObjective + " : " + activeQuests[i].questObjectiveCount + " / " + activeQuests[i].questObjectiveRequirement;
                }
            }
        }
    }

    public void StartDialogue(QuestObject NPCQuestObject)
    {
        sentences.Clear();

        //COGER NPC
        if (NPCQuestObject.availableQuestIDs.Count >= 1)
        {
            foreach (int values in NPCQuestObject.availableQuestIDs)
            {
                for (int i = 0; i < QuestManager.questManager.questList.Count; i++)
                {
                    if (values == QuestManager.questManager.questList[i].id && QuestManager.questManager.questList[i].progress == Quest.QuestProgress.AVAILABLE)
                    {
                        foreach (string sentence in QuestManager.questManager.questList[i].description)
                        {
                            //if (QuestManager.questManager.questList[j].id == NPCQuestObject.availableQuestIDs[j])
                            //{
                            sentences.Enqueue(sentence);
                            //}
                        }
                    }
                }
            }
        }


        //COMPLETAR NPC
        if (NPCQuestObject.receivableQuestIDs.Count >= 1)
        {
            foreach (int values in NPCQuestObject.receivableQuestIDs)
            {
                for (int i = 0; i < QuestManager.questManager.questList.Count; i++)
                {
                    if (values == QuestManager.questManager.questList[i].id && QuestManager.questManager.questList[i].progress == Quest.QuestProgress.COMPLETE)
                    {
                        foreach (string sentence in QuestManager.questManager.questList[i].hint)
                        {
                            //if (QuestManager.questManager.questList[j].id == NPCQuestObject.receivableQuestIDs[j])
                            //{
                            sentences.Enqueue(sentence);
                            //}
                        }
                    }
                }
            }
        }


        //ENTREGAR NPC
        if (NPCQuestObject.receivableQuestIDs.Count >= 1)
        {
            foreach (int values in NPCQuestObject.receivableQuestIDs)
            {
                for (int i = 0; i < QuestManager.questManager.questList.Count; i++)
                {
                    if (values == QuestManager.questManager.questList[i].id && QuestManager.questManager.questList[i].progress == Quest.QuestProgress.ACCEPTED)
                    {
                        foreach (string sentence in QuestManager.questManager.questList[i].congratulation)
                        {
                            //if (QuestManager.questManager.questList[j].id == NPCQuestObject.receivableQuestIDs[j])
                            //{
                            sentences.Enqueue(sentence);
                            //}
                        }
                    }
                }
            }
        }


        DisplayNextSentence(NPCQuestObject);
    }

    public void DisplayNextSentence(QuestObject NPCQuestObject)
    {
        if (sentences.Count <= 0)
        {
            questDescription.text = activeSentence;
            StopAllCoroutines();
            startedConvers = false;

            //aceptar la mision automáticamente
            if (NPCQuestObject.availableQuestIDs.Count >= 1)
            {
                foreach (int values in NPCQuestObject.availableQuestIDs)
                {
                    for (int i = 0; i < QuestManager.questManager.questList.Count; i++)
                    {
                        if (values == QuestManager.questManager.questList[i].id && QuestManager.questManager.questList[i].progress == Quest.QuestProgress.AVAILABLE)
                        {
                            QuestManager.questManager.AcceptQuest(values);
                        }
                    }
                }
            }

            //completar la mision automáticamente
            if (NPCQuestObject.receivableQuestIDs.Count >= 1)
            {
                foreach (int values in NPCQuestObject.receivableQuestIDs)
                {
                    //ENTREGAR NPC
                    for (int i = 0; i < QuestManager.questManager.questList.Count; i++)
                    {
                        if (values == QuestManager.questManager.questList[i].id &&  QuestManager.questManager.questList[i].progress == Quest.QuestProgress.COMPLETE)
                        {
                            //Completar la mision automáticamente
                            QuestManager.questManager.CompleteQuest(values);
                        }
                    }
                }
            }


            //entregar objeto de la mision automáticamente
            if (NPCQuestObject.receivableQuestIDs.Count >= 1)
            {
                foreach (int values in NPCQuestObject.receivableQuestIDs)
                {
                    //ENTREGAR NPC
                    for (int i = 0; i < QuestManager.questManager.questList.Count; i++)
                    {
                        if (values == QuestManager.questManager.questList[i].id && QuestManager.questManager.questList[i].progress == Quest.QuestProgress.ACCEPTED)
                        {
                            //entregar objeto de la mision automáticamente
                            QuestManager.questManager.deliverItemQuest(questID);
                        }
                    }
                }
            }


            questDescription.text = activeSentence;
            StopAllCoroutines();
            HideQuestPanel();
            startedConvers = false;





            //UPDATE ALL NPC
            QuestObject[] currentQuestGuys = FindObjectsOfType(typeof(QuestObject)) as QuestObject[];

            foreach (QuestObject obj in currentQuestGuys)
            {
                obj.SetQuestMaker();
            }

            return;
        }

        activeSentence = sentences.Dequeue();
        questDescription.text = activeSentence;

        StopAllCoroutines();
        StartCoroutine(TypeTheSentence(activeSentence));
    }

    IEnumerator TypeTheSentence(string sentence)
    {
        questDescription.text = "";

        foreach(char letter in sentence.ToCharArray())
        {
            questDescription.text += letter;
            //myAudio.PlayOneShot(speakSound);
            yield return new WaitForSeconds(typingSpeed);
        }
    }

    /*private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "Player")
        {
            //ShowQuestLog(Quest activeQuest);
            StartDialogue();
        }
    }

    private void OnTriggerStay(Collider other)
    {
        if (other.gameObject.tag == "Player")
        {
            if (Input.GetKeyDown(KeyCode.Space) && questDescription.text==activeSentence)
            {
                DisplayNextSentence();
            }
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject.tag == "Player")
        {
            HideQuestPanel();
            StopAllCoroutines();
        }
    }*/


}
