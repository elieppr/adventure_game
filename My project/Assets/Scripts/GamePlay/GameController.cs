using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum GameState { FreeRoam, Dialogue, Paused }
public class GameController : MonoBehaviour
{
    [SerializeField] PlayerController playerController;
    [SerializeField] Camera worldCamera;

    GameState state;
    GameState stateBeforePaused;
    private void Awake()
    {
        Instance = this;
        //ConditionsDB.Init();
    }
    public static GameController Instance { get; private set;}
    private void Start()
    {
        DialogueManager.Instance.OnShowDialogue += () =>
        {
            state = GameState.Dialogue;
        };
        DialogueManager.Instance.OnCloseDialogue += () =>
        {
            if(state == GameState.Dialogue)
            {
                state = GameState.FreeRoam;
            }
        };
    }

    public void PauseGame(bool pause) 
    {
        if (pause) 
        {
            stateBeforePaused = state;
            state = GameState.Paused;
        }
        else 
        {
            state = stateBeforePaused;
        }
    }

    private void Update()
    {
        if (state == GameState.FreeRoam)
        {
            playerController.HandleUpdate();
        }
        else if (state == GameState.Dialogue)
        {
            DialogueManager.Instance.HandleUpdate();
        }    
    }
}
