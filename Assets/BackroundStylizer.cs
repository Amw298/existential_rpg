using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BackroundStylizer : MonoBehaviour
{
    // Start is called before the first frame update
    private Image backround;
    public bool done = true;
    void Start()
    {
        backround = GetComponent<Image>();

    }
    void OnEnable()
    {
        done = true;
       backround = GetComponent<Image>();

    }

    // Update is called once per frame
    void Update()
    {
        if (done)
        {
            StartCoroutine(ColorChange());
        }
    }
    IEnumerator ColorChange()
    {
        done = false;
        Color newColor = new Color(Random.value, Random.value, Random.value, 255);
        backround.CrossFadeColor(newColor, 2, false, false);
        yield return new WaitForSeconds(2.2f);
        done = true;
        yield return null;
        
    }
}
