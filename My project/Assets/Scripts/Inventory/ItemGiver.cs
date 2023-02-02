using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemGiver : MonoBehaviour
{
    [SerializeField] ItemBase item;
    [SerializeField] Dialogue dialogue;
    [SerializeField] int count = 1;
    bool used = false;
    public IEnumerator GiveItem(PlayerController player)
    {
        yield return DialogueManager.Instance.ShowDialogue(dialogue);
        player.GetComponent<Inventory>().AddItem(item, count);

        used = true;

        Dialogue d = new Dialogue();
        //d.Lines = new List<string>();
        if (count == 1)
        {
            d.Lines.Add($"Player received {item.Name}");
        }
        else d.Lines.Add($"You recieved {count} {item.Name}s");

        yield return DialogueManager.Instance.ShowDialogue(d);

    }
    public bool CanBeGiven()
    {
        return item != null && !used && count > 0;
    }
}

