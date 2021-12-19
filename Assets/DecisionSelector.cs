using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class DecisionSelector : MonoBehaviour
{
    public List<TMP_Text> decisionsText;
    public TMP_Text continueButton;
    public DialogManager dialogManager;
    private bool deciding;
    public Color color;
    private int currentDecision;

    private void OnEnable()
    { 
        currentDecision = 0;
    }
    private void Update()
    {
        HighlightText(currentDecision);
    }

    public void ChooseDecision(Vector2 input)
    {
        float y = input.y;
        float x = input.x;
        if (deciding)
        {
            if (x == 1)
            {
                currentDecision = 1;
            }
            else if (x == -1)
            {
                currentDecision = 0;
            }
        }
        else
        {
            currentDecision = 0;
        }
    }
    public void HighlightText(int HighlightedMove)
    {
        
        for (int i = 0; i < decisionsText.Count; i++)
        {
            if (i == HighlightedMove)
            {
                decisionsText[i].color = color;
            }
            else
            {
               decisionsText[i].color = Color.white;
            }
        }
    }
    public void SelectDecision()
    {
        Debug.Log( "i am deciding" +deciding);
        if (deciding)
        {
            
            dialogManager.SelectDecision(currentDecision);
          //  dialogManager.DisplayNextSentence();
            
        }
        else
        {
            dialogManager.DisplayNextSentence();
            // decisionsText[currentMove].text = "I was selected";
        }

    }
    public void EnableDecisions(bool d)
    {
        deciding = d;
        currentDecision = 0;
        decisionsText[0].text = "Yes";
        decisionsText[1].text = "No";
    }
    public void EnableContinue()
    {
        deciding = false;
        currentDecision = 0;
        foreach (TMP_Text t in decisionsText){
            t.text = "";
        }
        decisionsText[0].text = "";
    }
    
}