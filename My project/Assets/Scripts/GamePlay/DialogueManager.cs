using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using System;

public class DialogueManager : MonoBehaviour
{
    [SerializeField] GameObject dialogueBox;
    [SerializeField] TMP_Text dialogueText;
    
    
    public event Action OnShowDialogue;
    public event Action OnCloseDialogue;

    public static DialogueManager Instance { get; private set; }

    private void Awake()
    {
        Instance = this;
    }

    //int currentLine = 0;
    //Action onDialogueFinished;
    //Dialogue dialogue;
    //bool isTyping;
    public bool IsShowing { get; private set; }

    public int lettersPerSecond;

    public IEnumerator ShowDialogue(Dialogue dialogue)
    {
        IsShowing = true;
        yield return new WaitForEndOfFrame();

        OnShowDialogue?.Invoke();
        //this.dialogue = dialogue;
        //onDialogueFinished = onFinished;
        dialogueBox.SetActive(true);
        //dialogueText.text = dialogue.Lines[0];

        foreach (var line in dialogue.Lines)
        {
            yield return TypeDialogue(line);
            yield return new WaitUntil(() => Input.GetKeyDown(KeyCode.Z));
        }
        dialogueBox.SetActive(false);
        IsShowing = false;
        OnCloseDialogue?.Invoke();
        //StartCoroutine(TypeDialogue(dialogue.Lines[0]));
    }

    public IEnumerator TypeDialogue(string line)
    {
        //isTyping = true;
        dialogueText.text = "";
        foreach (var letter in line.ToCharArray())
        {
            dialogueText.text += letter;
            yield return new WaitForSeconds(0.01f / lettersPerSecond);
        }
        //isTyping = false;
    }
    public void HandleUpdate()
    {
        //if (Input.GetKeyDown(KeyCode.Z) && !isTyping)
        //{
        //    ++currentLine;
        //    if (currentLine < dialogue.Lines.Count)
        //    {
        //        StartCoroutine(TypeDialogue(dialogue.Lines[currentLine]));
        //    }
        //    else
        //    {
        //        currentLine = 0;
        //        IsShowing = false;
        //        dialogueBox.SetActive(false);
        //        onDialogueFinished.Invoke();
        //        OnCloseDialogue?.Invoke();
        //    }
        //}
    }
}
