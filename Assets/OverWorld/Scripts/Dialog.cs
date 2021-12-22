using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using TMPro;
[System.Serializable]
[CreateAssetMenu(menuName = "DialogObject")]
public class Dialog :ScriptableObject
{
    public string npcName;
    [TextArea(3,10)]
    public string[] sentences;
    public GameObject[] choiceButtons;
    public bool isLock;
    public string uniqueID;
    public UnityEvent OnSpecialEvent;
   
        public Dialog[] AlternativeDialog;

    
}