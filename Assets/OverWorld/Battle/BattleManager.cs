using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class BattleManager : MonoBehaviour
{
   
    private enum BattleState {PlayerTurn, EnemyTurn, Won, Lost, Transition};
    public Image enemy;
    public Sprite[] enemyStates;
    public int enemyHp;
    private BattleState battleState;
    public FightDialog[] dialogs;
    public TMP_Text text;
    public float textSpeed = 0.01f;
   private bool firstClick = false;
    public GameObject brokenTV;
    public GameObject TV;
    public GameObject enemyObject;
    public Animator flasher;
    public void OnEnable()
    {
        AkSoundEngine.SetSwitch("FightMusic", "Start", gameObject);
        AkSoundEngine.PostEvent("StartBattle", this.gameObject);
       
        enemyHp = enemyStates.Length-1;
        text.text = dialogs[0].sentences[enemyHp];
        enemy.sprite = enemyStates[enemyHp];

        battleState = BattleState.PlayerTurn;
        firstClick = true;

    }
   
    IEnumerator EnemyTurn()
    {
        battleState = BattleState.EnemyTurn;
        text.text = "";
         foreach (char letter in dialogs[0].sentences[enemyHp].ToCharArray())
         { 
                 AkSoundEngine.PostEvent("TextSound", this.gameObject);
                 text.text += letter;
             yield return new WaitForSeconds(textSpeed);
         }
        battleState = BattleState.PlayerTurn;
        yield return null;

    }
    public void Smash()
    {
        if (firstClick){
            firstClick = false;
            return;
        }
        switch(battleState){
            case BattleState.PlayerTurn:
                enemyHp--;
                if (enemyHp >= 0)
                {
                    enemy.sprite = enemyStates[enemyHp];
                    enemyObject.GetComponent<Animator>().SetTrigger("Shake");
                    flasher.SetTrigger("flash");
                    AkSoundEngine.PostEvent("Attack", this.gameObject);

                }
                else
                {
                    EndBattle();
                    AkSoundEngine.PostEvent("KillingBlow", this.gameObject);

                }
                StopAllCoroutines();
                StartCoroutine(EnemyTurn());
                break;
            case BattleState.EnemyTurn:
                Debug.Log("EnemyTurn");
                battleState = BattleState.PlayerTurn;
                StopAllCoroutines();
                text.text = "";
                text.text = dialogs[0].sentences[enemyHp];
                break;
            case BattleState.Transition:
                Debug.Log("Transitions");
                break;
            case BattleState.Lost:
                Debug.Log("Losing");
                break;
            case BattleState.Won:
                Debug.Log("Winning");
                break;
            default:
                Debug.Log("NOTHING");
                break;
        }
        
        
    }
    public void OnDisable()
    {
        TV.SetActive(false);
        brokenTV.SetActive(true);
    }
    public void EndBattle()
    {
        AkSoundEngine.SetSwitch("FightMusic", "End", gameObject);
        AkSoundEngine.PostEvent("EndBattle", this.gameObject);

        this.gameObject.SetActive(false);
        FindObjectOfType<GhostMover>().Enable();
    }
}
