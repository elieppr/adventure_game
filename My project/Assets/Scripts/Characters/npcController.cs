using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class npcController : MonoBehaviour, Interactable
{
    [SerializeField] QuestBase questToStart;
    [SerializeField] QuestBase questToComplete;
    [SerializeField] Dialogue dialogue;
    [SerializeField] List<Vector2> MovementPattern;
    [SerializeField] float timeBetweenPattern;
    private Character character;
    NPCState state;
    float idleTimer = 0f;
    int currentPattern = 0;
    ItemGiver itemGiver;
    Quest activeQuest;
    private void Awake()
    {
        character = GetComponent<Character>();
        itemGiver = GetComponent<ItemGiver>();
    }

    private void Start()
    {
        //questToStart = new QuestBase();
        //spriteAnimator = new SpriteAnimator(sprites, GetComponent<SpriteRenderer>());
        //spriteAnimator.Start();
    }

    public IEnumerator Interact(Transform initiator)
    {
        Debug.Log("interact function");
        state = NPCState.Idle;
        if (state == NPCState.Idle)
        {

            Debug.Log("quest to start" + questToStart);
            state = NPCState.Dialogue;
            character.LookTowards(initiator.position);
            
            if (questToComplete != null)
            {
                var quest  = new Quest(questToComplete);
                yield return quest.CompleteQuest();
                questToComplete = null;
                Debug.Log("quest completed");
            }

            if (itemGiver != null && itemGiver.CanBeGiven())
            {
                yield return itemGiver.GiveItem(initiator.GetComponent<PlayerController>());
            }
            else if (questToStart != null)
            {
                Debug.Log("is quest null"); 
                activeQuest = new Quest(questToStart);
                yield return activeQuest.StartQuest();
                questToStart = null;

                if (activeQuest.CanBeCompleted())
                {
                    yield return activeQuest.CompleteQuest();
                    activeQuest = null;
                }

            }
            else if (activeQuest != null)
            {
                Debug.Log("stuff is going well");
                if (activeQuest.CanBeCompleted())
                {
                    yield return activeQuest.CompleteQuest();
                    activeQuest = null;
                }
                else
                {
                    yield return DialogueManager.Instance.ShowDialogue(activeQuest.Base.InProgressDialogue);
                }
            }
            else
            {
                yield return DialogueManager.Instance.ShowDialogue(dialogue);

            }
            idleTimer = 0f;
            state = NPCState.Idle;
        }
        //yield break;
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