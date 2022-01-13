using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Door : MonoBehaviour
{
    private GameObject outdoors;
    private GameObject indoors;
    private GhostMover player;
    private GameObject[] locks;
    public Image backround;

    public Dialog dialog;
    int lockcount;
    private Area area;
    // Start is called before the first frame update
    private enum Area
    {
        Inside,
        Outside,
    }
    void Start()
    {
        locks = GameObject.FindGameObjectsWithTag("Lock");
        lockcount = locks.Length;
        outdoors = GameObject.FindGameObjectWithTag("outdoors");
        outdoors.SetActive(false);
        indoors = GameObject.FindGameObjectWithTag("indoors");
        area = Area.Inside;
        player = FindObjectOfType<GhostMover>();
        backround.color = backround.color = new Color(backround.color.r, backround.color.g, backround.color.b, 0);

    }

    // Update is called once per frame
    void Update()
    {
        
    }
    public void Unlock()
    {
        this.lockcount--;
    }
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.collider.name == "PlayerCharacter")
        {
            if (lockcount != 0)
            {
                player.EnableDialog();
                FindObjectOfType<DialogManager>().StartDialog(dialog);


            }
            else
            {
                GetComponent<Collider2D>().isTrigger = true;
                GoOutside();
                AkSoundEngine.PostEvent("DoorOpen", this.gameObject);
            }
        }
    }
    private void GoOutside()
    {
        indoors.SetActive(false);
        outdoors.SetActive(true);
        area = Area.Outside;
        TransitionOn();
        player.SetY(7.0f);
        player.Disable();
    }
    private void GoInside()
    {
        indoors.SetActive(true);
        outdoors.SetActive(false);
        area = Area.Inside;
        TransitionOn();
        player.SetY(5.0f);
        player.Disable();

    }
    private void TransitionOn()
    {
        backround.color = backround.color = new Color(backround.color.r, backround.color.g, backround.color.b, 1);

    }
    private void TransitionOff()
    {
        backround.color = backround.color = new Color(backround.color.r, backround.color.g, backround.color.b, 0);

    }
    private void OnCollisionExit2D(Collision2D collision)
    {
       if (lockcount != 0)
        {

        }
        else {
            StartCoroutine(LoadTheArea());

        }

    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.name == "PlayerCharacter")
        {
            if(area == Area.Inside)
            {
                GoOutside();
            }
            else
            {
                GoInside();
            }
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        StartCoroutine(LoadTheArea());
  
    }
    private IEnumerator LoadTheArea()
    {
        yield return new WaitForSeconds(1);
        TransitionOff();
        yield return new WaitForSeconds(0.2f);
        if (area == Area.Outside) {
            AkSoundEngine.SetState("Area", "Outside");
        }
        else
        {
            AkSoundEngine.SetState("Area", "Inside");
        }
        player.Enable();

    }


}
