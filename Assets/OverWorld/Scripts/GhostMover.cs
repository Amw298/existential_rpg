using System;
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
    private GameObject interactable;
    private bool fading;
    public void SetInteractable(GameObject gameObject)
    {
        interactable = gameObject;
    }
    public bool isCoffee()
    {
        Debug.Log(interactable.name.Equals("CoffeeMachine"));
        return interactable.name.Equals("CoffeeMachine");
    }
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
        AkSoundEngine.PostEvent("GameStart", this.gameObject);
        fading = false;
        pi = gameObject.GetComponent<PlayerInput>();
        introManager = FindObjectOfType<IntroManager>();
        SetInteractable(this.gameObject);
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        Debug.Log(interactable);
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
        dialogManager = FindObjectOfType<DialogManager>();

        if (dialogManager.isTyping && context.performed)
        {
            dialogManager.textSpeed = 0.001f;
        }
        else
        if (context.started && inputState == InputState.Overworld)
        {
            AkSoundEngine.PostEvent("DialogStart" + interactable.name, interactable);
            if (!dialogManager.isTyping)
            {
                //dialogManager.StartDialog(interactable.GetComponent<InteractableNPC>().dialog);
                //AkSoundEngine.PostEvent("StartDialogMisc", interactable);

                if (interactableType == InteractableType.Laptop)
                {
                    //Debug.Log("interacting with laptop");
                    dialogManager.StartDialog(dialog);
                    //AkSoundEngine.PostEvent("StartDialogLaptop",this.gameObject);

                    // richie do stuff
                    // make a Laptop manager or something and use the laptop input state to make you able to use the laptop. 
                }
                else if (interactableType == InteractableType.CoffeeMachine)
                {
                    dialogManager.StartDialog(dialog, coffeeMachine);
                    Debug.Log("happening");
                   // AkSoundEngine.PostEvent("StartDialogCoffeeMachine", this.gameObject);
                }
                else
                {
                    dialogManager.StartDialog(dialog);

                }
            }
        }
        else if (context.started && inputState == InputState.Interrogation)
        {
            moveSelector.SelectMove();
        }
        else if (context.started && inputState == InputState.Dialog)
        {
            if (!dialogManager.isTyping)
            {
                decisionSelector.SelectDecision();
            }
        }
        else if (context.started && inputState == InputState.Intro)
        {
            if (fading == false)
            {
                introManager.FadeOutIntro();
                fading = true;
            }
            if (introManager.backround.color.a < 0.1f)
            {
                inputState = InputState.Overworld;
            }
        }
        if (context.canceled)
        {
            FindObjectOfType<DialogManager>().textSpeed = 0.02f;

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
