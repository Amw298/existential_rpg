using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BattleManager : MonoBehaviour
{
   
    public enum BattleState { Start, PlayerAction, Won, Lost, EnemyTurn}
    [SerializeField] BattleNPC enemy;
    public BattleState state;
    int currentMove;




    public void NavigateMenu(Vector2 input)
    {
        float y = input.y;
        if(y == -1)
        {
            if (currentMove < 1)
            {
                ++currentMove;
            }
        }else if (y==1){
            if (currentMove > 1)
            {
                --currentMove;
            }
        }
    }
}
