using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Laptop : InteractableNPC
{
     private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.name == "PlayerCharacter")
        {
            GhostMover gm = FindObjectOfType<GhostMover>();
            gm.SetDialog(dialog);
            gm.EnableLaptop();
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.name == "PlayerCharacter")
        {
            GhostMover gm = FindObjectOfType<GhostMover>();
            
              gm.SetDialog(defaultDialog);
            gm.DisableLaptop();

        }
    }
}
