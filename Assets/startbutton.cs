using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class startbutton : MonoBehaviour
{
    // Start is called before the first frame update
    public void StartNewGame()
    {
        UnityEngine.SceneManagement.SceneManager.LoadScene(1);

    }
}
