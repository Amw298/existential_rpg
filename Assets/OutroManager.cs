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
            Debug.Log(i);
            SceneManager.LoadScene(0);
            //   FindObjectOfType<DialogManager>().SetInteracting(true);
        }
    }
}
