using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;


public class MoveSelector : MonoBehaviour
{
    public List<TMP_Text> movesText;
   
    public Color color;
    public int  currentMove;

    private void Start()
    {
       
        currentMove = 0;
    }
    private void Update()
    {
        HighlightText(currentMove);
    }
    
    public void ChooseMove(Vector2 input)
    {
        float y = input.y;
        float x = input.x;
        Debug.Log(input.x);
        Debug.Log("current" + currentMove);
        if (y == -1)
        {
            if (currentMove ==0)
            {
                ++currentMove;
            }else if (currentMove == 2)
            {
                ++currentMove;
            }
        }
        else if (y == 1)
        {
            if (currentMove == 1)
            {
                --currentMove;

            }else if (currentMove == 3)
            {
                --currentMove;
            }
        }


            if (x == 1)
            {
                if(currentMove <= 1)
                {
                    currentMove +=2;
                }
            }else if (x==-1)
        {
            if (currentMove >= 2)
            {
                currentMove -= 2;
            }
        }
        
    }
    public void HighlightText(int HighlightedMove)
    {
        for (int i = 0; i < movesText.Count; i++) {
            if (i == HighlightedMove)
            {
                movesText[i].color = color;
            }
            else
            {
                movesText[i].color = Color.white;
            }
        }
    }
    public void SelectMove()
    {
        movesText[currentMove].text = "I was selected" ;
    }
}
