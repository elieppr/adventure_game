using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    //public float moveSpeed;
    private Vector2 input;
    
    private Character character;

    private void Awake()
    {
        
        character = GetComponent<Character>();
    }

    // Start is called before the first frame update
    void Start()
    {
        
    }

    public void HandleUpdate()
    {
        if (!character.IsMoving)
        {
            input.x = Input.GetAxisRaw("Horizontal");
            input.y = Input.GetAxisRaw("Vertical");


            if (input.x != 0) input.y = 0;

            if (input != Vector2.zero)
            {
                StartCoroutine(character.Move(input, OnMoveOver));
            }
        }

        character.HandleUpdate();

        if (Input.GetKeyDown(KeyCode.Z))
        {
            StartCoroutine(Interact());
        }

        if (Input.GetKeyDown(KeyCode.X))
        {
            GameController.Instance.OnBagSelected();

        }
    }

    IEnumerator Interact()
    {
        Debug.Log("Start to interact");
        var facingDir = new Vector3(character.Animator.MoveX, character.Animator.MoveY);
        var interactPos = transform.position + facingDir;

        Debug.DrawLine(transform.position, interactPos, Color.white, 1f);
        var collider = Physics2D.OverlapCircle(interactPos, 0.3f, GameLayers.i.Interactable);
        if (collider != null)
        {
            Debug.Log("Collider not null");
            var x = collider.GetComponent<Interactable>();
            Debug.Log("22222222"+x.ToString());
            Debug.Log(transform.position.x.ToString());
            yield return collider.GetComponent<Interactable>()?.Interact(transform);
        }
    }

    IPlayerTriggerable currentlyTrigger;
    private void OnMoveOver() 
    {
        var colliders = Physics2D.OverlapCircleAll(transform.position - new Vector3(0, character.OffsetY), 0.2f, GameLayers.i.TriggerableLayers);
        IPlayerTriggerable triggerable = null;
        foreach (var collider in colliders) 
        {   
            triggerable = collider.GetComponent<IPlayerTriggerable>();
            if (triggerable != null)
            {
                if (triggerable == currentlyTrigger && triggerable.TriggerRepeatedly)
                {
                    break;
                }
                triggerable.OnPlayerTriggered(this);
                currentlyTrigger = triggerable;
                break;
            }
        }
        if (colliders.Count() == 0 || triggerable != currentlyTrigger)
        {
            currentlyTrigger = null;
        }
    }

    public Character Character => character;

}
