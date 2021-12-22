using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Door : MonoBehaviour
{
    public GameObject[] locks;
    int lockcount;
    // Start is called before the first frame update
    void Start()
    {
        locks = GameObject.FindGameObjectsWithTag("Lock");
        lockcount = locks.Length;
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

}
