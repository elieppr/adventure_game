using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum GameState { FreeRoam, Dialogue, Paused, Bag }
public class GameController : MonoBehaviour
{
    [SerializeField] PlayerController playerController;
    [SerializeField] Camera worldCamera;
    [SerializeField] InventoryUI inventoryUI;
    public GameState state;
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
            //Debug.Log("paused?");
            stateBeforePaused = state;
            state = GameState.Paused;
        }
        else 
        {
            state = stateBeforePaused;
        }
    }

    public void OnBagSelected()
    {
        Debug.Log("bag selected");
        inventoryUI.gameObject.SetActive(true);
        state = GameState.Bag;
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
        else if (state == GameState.Bag)
        {
            Action onBack = () =>
            {
                inventoryUI.gameObject.SetActive(false);
                state = GameState.FreeRoam;
            };
            inventoryUI.HandleUpdate(onBack);
        }
        
    }
}
