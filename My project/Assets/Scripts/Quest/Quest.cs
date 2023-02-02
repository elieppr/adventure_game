using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class Quest 
{
    public QuestBase Base { get; private set; }
    public QuestStatus Status { get; private set; }
    public Quest(QuestBase _base)
    {
        Base = _base;
    }

    public IEnumerator StartQuest()
    {
        Debug.Log("StartQuest?");
        Status = QuestStatus.Started;

        yield return DialogueManager.Instance.ShowDialogue(Base.StartDialogue);

        var questList = QuestList.GetQuestList();
        questList.AddQuest(this);
    }
    public IEnumerator CompleteQuest()
    {
        Status = QuestStatus.Completed;
        yield return DialogueManager.Instance.ShowDialogue(Base.CompletedDialogue);
        var inventory = Inventory.GetInventory();
        if(Base.RequiredItem != null)
        {
            inventory.RemoveItem(Base.RequiredItem);
        }
        if(Base.RewardItem != null)
        {
            inventory.AddItem(Base.RewardItem);
            Dialogue d = new Dialogue();
            d.Lines.Add($"You recieved {Base.RequiredItem}");
            yield return DialogueManager.Instance.ShowDialogue(d);
        }

        var questList = QuestList.GetQuestList();
        questList.AddQuest(this);
    }
    public bool CanBeCompleted()
    {
        var inventory = Inventory.GetInventory();

        if(Base.RequiredItem != null)
        {
            if (!inventory.HasItem(Base.RequiredItem))
            {
                return false;
            }
            
        }
        return true;
    }
}

public enum QuestStatus { None, Started, Completed }
