using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class npcController : MonoBehaviour, Interactable
{
    [SerializeField] Dialogue dialogue;
    public void Interact()
    {
        StartCoroutine(DialogueManager.Instance.ShowDialogue(dialogue));
    }
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
