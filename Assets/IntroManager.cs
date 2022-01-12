using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;


public class IntroManager : MonoBehaviour
{
    public TMP_Text heading;
    public TMP_Text description;
    public Image backround;
    private GhostMover gm;
    // Start is called before the first frame update
    void Start()
    {
        gm = FindObjectOfType<GhostMover>();
        gm.EnableIntro();
        
    }

    // Update is called once per frame
    void Update()
    {

 
    }
    public void FadeOutIntro()
    {
        StartCoroutine(FadeOutText(3, heading));
        StartCoroutine(FadeOutText(2, description));
        StartCoroutine(FadeOutBackround(7, backround));
    }
    public IEnumerator FadeOutText(float time, TMP_Text text)
    {
        text.color = new Color(text.color.r, text.color.g, text.color.b, text.color.a);
        while(text.color.a >0.0f)
        {
            text.color = new Color(text.color.r, text.color.g, text.color.b, text.color.a - Time.deltaTime/time);
            yield return null;

        }
    }
    public IEnumerator FadeOutBackround(float time, Image backround)
    {
        backround.color = new Color(backround.color.r, backround.color.g, backround.color.b, backround.color.a);
        while (backround.color.a > 0.0f)
        {

            backround.color =new Color(backround.color.r, backround.color.g, backround.color.b, backround.color.a - Time.deltaTime / time);
            yield return null;

        }
    }
}
