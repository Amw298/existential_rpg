using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CliffManager : MonoBehaviour
{
    int suicideAttemptsStopped =1 ;
    public Dialog[] dialogs;
    GhostMover player;
    // Start is called before the first frame update
    void Start()
    {
        player = FindObjectOfType<GhostMover>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.collider.name == "PlayerCharacter")
        {
            if (suicideAttemptsStopped >= 0)
            {
                player.Disable();
                player.EnableDialog();
                FindObjectOfType<DialogManager>().StartDialog(dialogs[suicideAttemptsStopped]);
                collision.gameObject.transform.position = new Vector3(collision.gameObject.transform.position.x, collision.gameObject.transform.position.y - 1, collision.transform.position.z);
                collision.rigidbody.velocity = new Vector3(0, 0, 0);
                suicideAttemptsStopped--;
            }
            else
            {
                GetComponent<Collider2D>().isTrigger = true;
                FindObjectOfType<OutroManager>().Quit();

                Application.Quit();
            }
        }
    }
    private void OnTriggerStay2D(Collider2D collision)
    {
        if (collision.name == "PlayerCharacter")
        {
            FindObjectOfType<OutroManager>().Quit();
            Destroy(collision.gameObject);
            Application.Quit();
        }
    }
}
