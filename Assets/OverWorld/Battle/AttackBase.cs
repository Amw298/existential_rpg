using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[System.Serializable]
[CreateAssetMenu(menuName = "Attacks")]
public class AttackBase : ScriptableObject
{
   [SerializeField] string moveName;
    [TextArea]
     [SerializeField]  string description;
   
    public string MoveName
    {
        get { return moveName; }
    }
    public string Description
    {
        get { return description; }
    }

}
