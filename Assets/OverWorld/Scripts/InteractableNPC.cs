﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InteractableNPC : MonoBehaviour
{
    public Dialog dialog;
    public Dialog defaultDialog;
    public void TriggerDialog()
    {
        FindObjectOfType<DialogManager>().StartDialog(dialog);
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.name == "PlayerCharacter")
           
        {
            FindObjectOfType<GhostMover>().SetDialog(dialog);
         //   FindObjectOfType<DialogManager>().SetInteracting(true);
        }
    }
    private void OnTriggerExit2D(Collider2D collision)
    {
        if(collision.name== "PlayerCharacter")
        {
            
            FindObjectOfType<GhostMover>().SetDialog(defaultDialog);
           //FindObjectOfType<DialogManager>().SetInteracting(false);
        }
    }
}
