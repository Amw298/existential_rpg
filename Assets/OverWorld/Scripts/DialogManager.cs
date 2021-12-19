
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
    private int counter = 0;
    private Canvas canvas;
    [SerializeField]
    private GameObject ghost;
    [SerializeField]
    private Dialog defaultDialog;
    public PlayerInput pi;
    public GhostMover gm;
    public bool inTag;
    public bool isTyping;
    public bool isInteracting;
    public bool decisionToBeMade;
    public Dialog currentDialog;
    public int altDialogIndex;
    public DecisionSelector decisionSelector;

    // Start is called before the first frame update
    void Start()
    {

        ghost = GameObject.FindGameObjectWithTag("PlayerCharacter");
        pi = ghost.GetComponent<PlayerInput>();
      
        canvas = GameObject.FindGameObjectWithTag("DialogBox").GetComponent<Canvas>();
        sentences = new Queue<string>();

    }

    private void Update()
    {
        //TypeWavy();
        //OnTextEffect?.Invoke(this, EventArgs.Empty);// this is a unity event you can sub to
    }
    // Update is called once per frame


    public void StartDialog(Dialog dialog)
    {

        //   if(dialog== null || !isInteracting)
        //   {
        //   Debug.Log("this happened00");
        //     dialog = defaultDialog;
        //  }
        if(dialog.AlternativeDialog.Length ==0)
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
        //OnTextEffect = noEffect;
        dialogText.text = "";
        //int i = 0;
        // isTyping = true;
        foreach (char letter in sentence.ToCharArray())
        {
            // CheckTag(sentence, letter, i, ref inTag);
            // i++;
            //  if (!inTag)
            //  {
            dialogText.text += letter;

            yield return null;//new WaitForSeconds(1f*Time.fixedDeltaTime);
                              //    }

        }

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
                //OnTextEffect = noEffect;
                Debug.Log("No Effect");
            }
        }
        else if (j > 0 && fullText[j - 1] == '>')
        {
            inTag = false;
        }
    }


    private void noEffect(object sender, EventArgs e)
    {

    }


    public void SetInteracting(bool interacting)
    {
        isInteracting = interacting;
    }

    public void SelectDecision(int decision)
    {
        altDialogIndex = decision;
        StartDialog(currentDialog.AlternativeDialog[altDialogIndex]);
    }

}
