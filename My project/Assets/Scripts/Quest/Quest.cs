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
        if (Status == QuestStatus.Completed)
        {
            yield break;
        }
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
        if(Base.RequiredItems != null && Base.RequiredItems.Count > 0)
        {
            foreach (ItemBase requireditem in Base.RequiredItems)
            {
                inventory.RemoveItem(requireditem);
            }
            //inventory.RemoveItem(Base.RequiredItem);
        }
        if(Base.RewardItem != null)
        {
            inventory.AddItem(Base.RewardItem);
            Dialogue d = new Dialogue();
            d.Lines.Add($"You recieved {Base.RewardItem}");
            yield return DialogueManager.Instance.ShowDialogue(d);
        }

        var questList = QuestList.GetQuestList();
        questList.AddQuest(this);
    }
    public bool CanBeCompleted()
    {
        var inventory = Inventory.GetInventory();

        if(Base.RequiredItems != null && Base.RequiredItems.Count > 0)
        {
            foreach (ItemBase requiredItem in Base.RequiredItems)
            {
                if (!inventory.HasItem(requiredItem))
                {
                    
                    return false;
                }
                else
                {
                    var cnt1 = inventory.Slots.Find(slots => slots.Item == requiredItem);
                    int cnt11 = cnt1.Count;
                    var cnt2 = Base.RequiredItems.FindAll(item => item == requiredItem);
                    var cnt22 = cnt2.Count;
                    if (cnt11 < cnt22)
                    {
                        return false;
                    }
                }
            }
            //if (!inventory.HasItem(Base.RequiredItem))
            //{
            //    return false;
            //}
        }
        return true;
    }
}

public enum QuestStatus { None, Started, Completed }
