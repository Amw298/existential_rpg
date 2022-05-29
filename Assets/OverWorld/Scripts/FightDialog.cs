using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using TMPro;
[System.Serializable]
[CreateAssetMenu(menuName = "FightDialog")]
public class FightDialog : ScriptableObject
{
    public string enemyName;
    [TextArea(3, 10)]
    public string[] sentences;
}