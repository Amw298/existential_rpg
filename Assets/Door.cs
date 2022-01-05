using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Door : MonoBehaviour
{
    private GameObject outdoors;
    private GameObject indoors;

    private GameObject[] locks;
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
                Debug.Log("not all unlocked" + lockcount);
            }
            else
            {
                GetComponent<Collider2D>().isTrigger = true;
            }
        }
    }
    private void OnTriggerExit2D(Collider2D collision)
    {
        if(collision.name == "PlayerCharacter")
        {
            if(area == Area.Inside)
            {
                indoors.SetActive(false);
                outdoors.SetActive(true);
                area = Area.Outside;
            }
            else
            {
                indoors.SetActive(true);
                outdoors.SetActive(false);
                area = Area.Inside;
            }
        }
    }

}
