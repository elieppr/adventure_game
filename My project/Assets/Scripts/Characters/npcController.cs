using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class npcController : MonoBehaviour, Interactable
{
    [SerializeField] Dialogue dialogue;
    [SerializeField] List<Vector2> MovementPattern;
    [SerializeField] float timeBetweenPattern;
    private Character character;
    NPCState state;
    float idleTimer = 0f;
    int currentPattern = 0;

    private void Awake()
    {
        character = GetComponent<Character>();
    }

    private void Start()
    {
        //spriteAnimator = new SpriteAnimator(sprites, GetComponent<SpriteRenderer>());
        //spriteAnimator.Start();
    }

    public void Interact(Transform initiator)
    {
        if (state == NPCState.Idle)
        {
            state = NPCState.Dialogue;
            character.LookTowards(initiator.position);
            StartCoroutine(DialogueManager.Instance.ShowDialogue(dialogue, () => { 
                idleTimer = 0f;
                state = NPCState.Idle;
            }));
        }
    }

    // Update is called once per frame
    void Update()
    {
        if (state == NPCState.Idle)
        {
            idleTimer += Time.deltaTime;
            if (idleTimer > timeBetweenPattern)
            {
                if (MovementPattern.Count > 0) { 
                    StartCoroutine(Walk());
                }
                idleTimer = 0f;
            }
        }
        character.HandleUpdate();

    }

    IEnumerator Walk()
    {
        state = NPCState.Walking;

        var oldPos = transform.position;
        yield return character.Move(MovementPattern[currentPattern]);
        if (transform.position != oldPos) 
        {
            currentPattern = (currentPattern + 1) % MovementPattern.Count;
        }
        state = NPCState.Idle;
    }
}

public enum NPCState { Idle, Walking, Dialogue}