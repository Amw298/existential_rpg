using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.SceneManagement;

public class GhostMover : MonoBehaviour
{
    public PlayerInput pi;
    public Vector3 direction;
    private static Vector3 savedPosition;
    public Dialog dialog;
    public DialogManager dialogManager;
    public DecisionSelector decisionSelector;
    public MoveSelector moveSelector;
    //private int counter=0;
    private InputState inputState;

 private enum InputState
    {
        Overworld,
        Dialog,
        Interrogation
    }
    void Start()
    {
       // inputState = InputState.Interrogation;
        transform.position = savedPosition;
 
        pi = gameObject.GetComponent<PlayerInput>();
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        Debug.Log(inputState);
        if (inputState == InputState.Overworld)
        {
            transform.position += direction * 10f * Time.fixedDeltaTime;
        }
        savedPosition = transform.position;
        //inputState = InputState.Interrogation;
    }
  public  void OnGhostMove(InputAction.CallbackContext context)
    {
        
        if (inputState == InputState.Overworld)
        {
            SetGhostDirection(context.ReadValue<Vector2>());
        } else if(inputState == InputState.Interrogation){
            moveSelector.ChooseMove(context.ReadValue<Vector2>());
        }else if(inputState == InputState.Dialog)
        {
         decisionSelector.ChooseDecision(context.ReadValue<Vector2>());
        }
    }
   public  void SetGhostDirection(Vector2 direction)
    {
        this.direction = direction;
    }
    public void OnLoadFightScene()
    {
        SceneManager.LoadScene(1);
        gameObject.SetActive(false);
    }
    public void OnInteract(InputAction.CallbackContext context)
    {
        if (context.canceled && inputState == InputState.Overworld)
        {
            FindObjectOfType<DialogManager>().StartDialog(dialog);
            //dialogManager.StartDialog(dialog);
        }else if(context.canceled && inputState == InputState.Interrogation)
        {
            moveSelector.SelectMove();
        }else if(context.canceled && inputState == InputState.Dialog)
        {
            decisionSelector.SelectDecision();
        }
            

    }
    public void SetDialog(Dialog d)
    {
        dialog = d;
    }
    public void EnableDialog()
    {
        inputState = InputState.Dialog;
    }
    public void EnableOverworld()
    {
        inputState = InputState.Overworld;
    }
    public void EnableInterogation()
    {
        inputState = InputState.Interrogation;
    }
}
