using System.Collections;
using System.Collections.Generic;
using UnityEngine.SceneManagement;
using UnityEngine;

public class OutroManager : MonoBehaviour
{
    public static int i = 0;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.name == "PlayerCharacter")

        {
            i++;
            if (i > 1)
            {
                i = 0;
            }
            Debug.Log(i);
            SceneManager.LoadScene(i);
            //   FindObjectOfType<DialogManager>().SetInteracting(true);
        }
    }
}
