using System.Collections;
using System.Collections.Generic;
using UnityEngine.SceneManagement;
using UnityEngine;
using TMPro;
using UnityEngine.UI;
using System;

public class OutroManager : MonoBehaviour
{
    public TMP_Text heading;
    public TMP_Text description;
    public Image backround;
    private GhostMover gm;
    private AudioManager audioManager;
    public static int i = 1;
    // Start is called before the first frame update
    void Start()
    {
        gm = FindObjectOfType<GhostMover>();
        audioManager = FindObjectOfType<AudioManager>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.name == "PlayerCharacter")
        {
            gm.Disable();
            audioManager.SceneEnd();
            StartCoroutine(Outro(7));
           
            //   FindObjectOfType<DialogManager>().SetInteracting(true);
        }
    }

    private IEnumerator Outro(float time)
    {
        backround.color = new Color(backround.color.r, backround.color.g, backround.color.b, backround.color.a);
        while (backround.color.a < 1.0f)
        {

            backround.color = new Color(backround.color.r, backround.color.g, backround.color.b, backround.color.a + Time.deltaTime / time);
            yield return null;

        }
        yield return new WaitForSeconds(1);
        i++;
        if (i > 3)
        {
            i = 1;
        }
        SceneManager.LoadScene(i);
    }
}
