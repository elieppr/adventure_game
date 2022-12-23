using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class npcController : MonoBehaviour, Interactable
{
    [SerializeField] Dialogue dialogue;
    //[SerializeField] List<Sprite> sprites;

    //SpriteAnimator spriteAnimator;

    private void Start()
    {
        //spriteAnimator = new SpriteAnimator(sprites, GetComponent<SpriteRenderer>());
        //spriteAnimator.Start();
    }

    public void Interact()
    {
        StartCoroutine(DialogueManager.Instance.ShowDialogue(dialogue));
    }
    
    // Update is called once per frame
    void Update()
    {
        //spriteAnimator.HandleUpdate();
    }
}
