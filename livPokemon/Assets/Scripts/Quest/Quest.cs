using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class Quest 
{                                  //0             1        2         3       4
    public enum QuestProgress {NOT_AVAILABLE, AVAILABLE, ACCEPTED, COMPLETE, DONE}

    public string title;                        //Titulo de la mision
    public int id;                              //numero de la ID de la mision
    public QuestProgress progress;              //estado de la mision actual (enum)

    [TextArea(3, 10)]
    public string[] description;                  //string de la mision de la mision que cogo/recibo
    [TextArea(3, 10)]
    public string[] hint;                         //string de la mision de la mision que cogo/recibo
    [TextArea(3, 10)]
    public string[] congratulation;               //string de la mision de la mision que cogo/recibo

    public string summary;                      //string de la mision de la mision que cogo/recibo
    public int nextQuest;                       //siguiente mision si hay alguna

    public string questObjective;               //nombre del objetivo de la mision (tambien para eliminar)
    public int questObjectiveCount;             //contador del objetivo de la mision
    public int questObjectiveRequirement;       //cantidad que se necesita para completar el objetivo de la mision

    public int expReward;                       //
    public int goldReward;                      //
    public string itemReward;                   //
}
