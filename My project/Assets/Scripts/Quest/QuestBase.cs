using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Quest/Create a new Quest")]
public class QuestBase : ScriptableObject
{
    [SerializeField] string name;
    [SerializeField] string description;

    [SerializeField] Dialogue startDialogue;
    [SerializeField] Dialogue inProgressDialogue;
    [SerializeField] Dialogue completedDialogue;

    //[SerializeField] ItemBase requiredItem;
    [SerializeField] List<ItemBase> requiredItems;
    [SerializeField] ItemBase rewardItem;

    public string Name => name;
    public string Description => description;
    public Dialogue StartDialogue => startDialogue;
    public Dialogue InProgressDialogue => inProgressDialogue;
    public Dialogue CompletedDialogue => completedDialogue;

    public List<ItemBase> RequiredItems => requiredItems;
    public ItemBase RewardItem => rewardItem;
}
