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
    private InteractableType interactableType;
    //private int counter=0;
    private InputState inputState;
    private IntroManager introManager;
    private CoffeeMachine coffeeMachine;

    
    private enum InputState
    {
        Overworld,
        Dialog,
        Interrogation,
        Laptop,
        Intro,
        Disable,
    }
    private enum InteractableType
    {
        Laptop,
        Misc,
        CoffeeMachine
    }
    void Start()
    {
        // inputState = InputState.Interrogation;
        //transform.position = savedPosition;

        pi = gameObject.GetComponent<PlayerInput>();
        introManager = FindObjectOfType<IntroManager>();
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        if (inputState == InputState.Overworld)
        {
            transform.position += direction * 10f * Time.fixedDeltaTime;
        }
        savedPosition = transform.position;
        //inputState = InputState.Interrogation;
    }
    public void OnGhostMove(InputAction.CallbackContext context)
    {
        if(inputState == InputState.Disable)
        {
            SetGhostDirection(new Vector2(0, 0));
        }
        if (inputState == InputState.Overworld)
        {
            SetGhostDirection(context.ReadValue<Vector2>());
        }
        else if (inputState == InputState.Interrogation)
        {
            moveSelector.ChooseMove(context.ReadValue<Vector2>());
        }
        else if (inputState == InputState.Dialog)
        {
            decisionSelector.ChooseDecision(context.ReadValue<Vector2>());
        }
        else if (context.canceled && inputState == InputState.Intro)
        {
            if (introManager.backround.color.a < 0.4f)
            {
                inputState = InputState.Overworld;
            }
        }
    }
    public void SetGhostDirection(Vector2 direction)
    {
        this.direction = direction;
    }
    public void SetY(float y)
    {
        transform.position = new Vector3(transform.position.x, y ,transform.position.z);
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
            if (interactableType == InteractableType.Laptop)
            {
                Debug.Log("interacting with laptop");
                FindObjectOfType<DialogManager>().StartDialog(dialog);
                // richie do stuff
                // make a Laptop manager or something and use the laptop input state to make you able to use the laptop. 
            }
            else if (interactableType == InteractableType.CoffeeMachine)
            {
                FindObjectOfType<DialogManager>().StartDialog(dialog, coffeeMachine);
            }
            else
            {
                FindObjectOfType<DialogManager>().StartDialog(dialog);
            }

        }
        else if (context.canceled && inputState == InputState.Interrogation)
        {
            moveSelector.SelectMove();
        }
        else if (context.canceled && inputState == InputState.Dialog)
        {
            decisionSelector.SelectDecision();
        }
        else if (context.canceled && inputState == InputState.Intro)
        {
            introManager.FadeOutIntro();
            if (introManager.backround.color.a < 0.1f)
            {
                inputState = InputState.Overworld;
            }
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
    public void EnableLaptop()
    {
        interactableType = InteractableType.Laptop;

    }
    public void DisableLaptop()
    {
        interactableType = InteractableType.Misc;

    }
    public void EnableCoffee()
    {
        interactableType = InteractableType.CoffeeMachine;

    }
    public void resetMisc()
    {
        interactableType = InteractableType.Misc;

    }
    public void EnableIntro()
    {
        inputState = InputState.Intro;
    }
    public void setCoffeeMachine(CoffeeMachine incomingNPC)
    {
        coffeeMachine = incomingNPC;
    }
    public bool CoffeeMachine()
    {
        return interactableType == InteractableType.CoffeeMachine;
    }
    public void Disable()
    {
        inputState = InputState.Disable;
    }
    public void Enable()
    {
        inputState = InputState.Overworld;
    }
}
