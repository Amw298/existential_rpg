using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CoffeeMachine : InteractableNPC
{
    public Dialog[] dialogs;
    public int numberOfTimesInteracted = 0;
    private void FixedUpdate()
    {
        GhostMover gm = FindObjectOfType<GhostMover>();

        BoxCollider2D bc =  this.GetComponent<BoxCollider2D>();
        GameObject player = GameObject.FindGameObjectWithTag("PlayerCharacter");
       if(bc.IsTouching(player.GetComponent<Collider2D>()))
        {
            gm.SetDialog(dialogs[numberOfTimesInteracted]);
        }
    }
    // Start is called before the first frame update
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.name == "PlayerCharacter")

        {
            GhostMover gm = FindObjectOfType<GhostMover>();
            gm.SetDialog(dialogs[numberOfTimesInteracted]);
            gm.EnableCoffee();
            gm.SetInteractable(this.gameObject);

            //   FindObjectOfType<DialogManager>().SetInteracting(true);
        }
    }
    private void OnTriggerStay2D(Collider2D collision)
    {
        if (collision.name == "PlayerCharacter")

        {
            GhostMover gm = FindObjectOfType<GhostMover>();

            gm.SetDialog(dialogs[numberOfTimesInteracted]);
            gm.EnableCoffee();

            //   FindObjectOfType<DialogManager>().SetInteracting(true);
        }
    }
    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.name == "PlayerCharacter")
        {


            GhostMover gm = FindObjectOfType<GhostMover>();
            gm.SetDialog(defaultDialog);
            gm.resetMisc();
            gm.SetInteractable(collision.gameObject);

            //FindObjectOfType<DialogManager>().SetInteracting(false);
        }
    }
    public void incrementInteracted()
    {
        if (numberOfTimesInteracted < dialogs.Length-1) { numberOfTimesInteracted++; }
      
    }
    
    
}
