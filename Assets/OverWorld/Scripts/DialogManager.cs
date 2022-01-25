
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.InputSystem;


public class DialogManager : MonoBehaviour
{

    public TMP_Text nameText;
    public TMP_Text dialogText;
    private Queue<string> sentences;
    private Canvas canvas;
    [SerializeField]
    private GameObject ghost;
    [SerializeField]
    private Dialog defaultDialog;
    public PlayerInput pi;
    public GhostMover gm;
    public bool inTag;
    public bool isTyping=false;
    public bool isInteracting;
    public bool decisionToBeMade;
    public Dialog currentDialog;
    public int altDialogIndex;
    public DecisionSelector decisionSelector;
    public LinkedList<Dialog> recentlyUsedDialogs;
    public Door door;
    public CoffeeMachine cm;
    public float textSpeed = 0.2f;
    
    // Start is called before the first frame update
    void Start()
    {
        cm = FindObjectOfType<CoffeeMachine>();
        ghost = GameObject.FindGameObjectWithTag("PlayerCharacter");
        pi = ghost.GetComponent<PlayerInput>();
        door = GameObject.FindObjectOfType<Door>();
        canvas = GameObject.FindGameObjectWithTag("DialogBox").GetComponent<Canvas>();
        sentences = new Queue<string>();
        recentlyUsedDialogs = new LinkedList<Dialog>();


    }

    private void Update()
    {
        //TypeWavy();
        //OnTextEffect?.Invoke(this, EventArgs.Empty);// this is a unity event you can sub to
    }
    // Update is called once per frame

    public void StartDialog(Dialog dialog, CoffeeMachine coffeeMachine)
    {
      //  cm = coffeeMachine;
        bool usedalready = false;
        if (dialog.isLock)
        {
            foreach (Dialog dia in recentlyUsedDialogs)
            {
                if (dia.uniqueID.Equals(dialog.uniqueID))
                {
                    usedalready = true;
                }
            }
            if (!usedalready)
            {
                door.Unlock();
                recentlyUsedDialogs.AddLast(dialog);
                //coffeeMachine.incrementInteracted();
            }


        }
        else
        {
            foreach (Dialog dia in recentlyUsedDialogs)
            {
                if (dia.uniqueID.Equals(dialog.uniqueID))
                {
                    usedalready = true;
                }
            }
            if (!usedalready)
            {
                recentlyUsedDialogs.AddLast(dialog);
               // coffeeMachine.incrementInteracted();
            }
        }
        if (dialog.AlternativeDialog.Length == 0)
        {
            decisionToBeMade = false;

        }
        else
        {
            decisionToBeMade = true;
        }
        decisionSelector.EnableContinue();
        gm.EnableDialog();
        canvas.enabled = true;
        currentDialog = dialog;// set the dialog manager dialog to the dialog that was input
        nameText.text = dialog.npcName;// show the npc name
        sentences.Clear();
        foreach (string sentence in dialog.sentences)// this grabs the sentences in the the array in the dialog scriptable obj
        {
            sentences.Enqueue(sentence);// adds to the sentence queue
        }
        DisplayNextSentence();
    }
    public void StartDialog(Dialog dialog)
    {
        bool usedalready = false;
        if (dialog.isLock)
        {
            foreach (Dialog dia in recentlyUsedDialogs)
            {
                if (dia.uniqueID.Equals(dialog.uniqueID))
                {
                    usedalready = true;
                }
            }
            if (!usedalready)
            {
                door.Unlock();
                recentlyUsedDialogs.AddLast(dialog);

            }
        }
        if (dialog.AlternativeDialog.Length == 0)
        {
            decisionToBeMade = false;

        }
        else
        {
            decisionToBeMade = true;
        }
        decisionSelector.EnableContinue();
        gm.EnableDialog();
        canvas.enabled = true;
        currentDialog = dialog;// set the dialog manager dialog to the dialog that was input
                               //use this to invoke the special jawn
                               //dialog.OnSpecialEvent?.Invoke();
                               //  dialog = defaultDialog;
        nameText.text = dialog.npcName;// show the npc name

        sentences.Clear();

        foreach (string sentence in dialog.sentences)// this grabs the sentences in the the array in the dialog scriptable obj
        {
            sentences.Enqueue(sentence);// adds to the sentence queue
        }
        DisplayNextSentence();
    }
    private void EndDialog(CoffeeMachine coffeeMachine)
    {
        canvas.enabled = false;
        gm.EnableOverworld();
    }
    public void DisplayNextSentence()
    {
        if (sentences.Count == 1)
        {
            if (decisionToBeMade)
            {
                decisionSelector.EnableDecisions(true);

            }
        }

        if (sentences.Count == 0)
        {

            EndDialog();
            return;

        }
        string sentence = sentences.Dequeue();// makes the current sentence the queue
        StopAllCoroutines();
        StartCoroutine(TypeSentence(sentence));
    }
   
    IEnumerator TypeSentence(string sentence)
    {
        
        isTyping = true;
        dialogText.text = "";
        foreach (char letter in sentence.ToCharArray())
        {
            AkSoundEngine.PostEvent("TextSound", this.gameObject);
            dialogText.text += letter;
            yield return new WaitForSeconds(textSpeed);
        }
        isTyping = false;

    }
    public event EventHandler OnTextEffect;
    private void TextEffect(object sender, EventArgs e)
    {
        TypeWavy();

    }
    public void TypeWavy()//Thanks to kemble Software for making this epic format to shake tmp text
    {
        dialogText.ForceMeshUpdate();
        var textInfo = dialogText.textInfo;
        for (int i = 0; i < textInfo.characterCount; ++i)
        {
            var charInfo = textInfo.characterInfo[i];
            if (!charInfo.isVisible)
            {
                continue;
            }
            var vs = textInfo.meshInfo[charInfo.materialReferenceIndex].vertices;

            for (int j = 0; j < 4; ++j)
            {
                int position = charInfo.vertexIndex + j;
                var orig = vs[position];
                vs[position] = orig + new Vector3(0, Mathf.Sin(Time.time * 2f + orig.x * .01f) * 10f, 0);
            }
        }
        for (int i = 0; i < textInfo.meshInfo.Length; ++i)
        {
            var meshInfo = textInfo.meshInfo[i];
            meshInfo.mesh.vertices = meshInfo.vertices;
            dialogText.UpdateGeometry(meshInfo.mesh, i);
        }
    }
    private void EndDialog()
    {
        canvas.enabled = false;
        gm.EnableOverworld();
    }
    protected void CheckTag(string fullText, char c, int j, ref bool inTag)
    {
        if (c == '<')
        {
            inTag = true;
            char next = fullText[j + 1];
            if (next != '/')
            {
                switch (next)
                {
                    case 'd': OnTextEffect = TextEffect; break;
                }

            }
            else
            {
                Debug.Log("No Effect");
            }
        }
        else if (j > 0 && fullText[j - 1] == '>')
        {
            inTag = false;
        }
    }
    public void SetInteracting(bool interacting)
    {
        isInteracting = interacting;
    }

    public void SelectDecision(int decision)
    {
        altDialogIndex = decision;
       // Debug.Log(decision);
        if(altDialogIndex ==0 && gm.CoffeeMachine())// if yes increment the thing
        {
            cm.incrementInteracted();
        }
        StartDialog(currentDialog.AlternativeDialog[altDialogIndex]);
    }

}
