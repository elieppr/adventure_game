using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Pickup : MonoBehaviour, Interactable
{
    [SerializeField] ItemBase item;
    [SerializeField] public bool Used { get; set; } = false;
    [SerializeField] public static bool created = false;
    
    void Awake() {
        //Debug.Log("is awake called");
        //Debug.Log("used " + Used);
        //if (Used || created) 
        //{
        //    //DontDestroyOnLoad(this.gameObject);
        //    Debug.Log("why not doing this");
        //    GetComponent<SpriteRenderer>().enabled = false;
        //    GetComponent<BoxCollider2D>().enabled = false;

        //}
    }
    public IEnumerator Interact(Transform initiator)
    {
        
        if (!Used)
        {
            initiator.GetComponent<Inventory>().AddItem(item);

            Used = true;
            created = true;
            //Destroy(gameObject);
            GetComponent<SpriteRenderer>().enabled = false;
            GetComponent<BoxCollider2D>().enabled = false;
            Debug.Log("used " + Used);
            Dialogue d = new Dialogue();
            //d.Lines = new List<string>();
            d.Lines.Add($"Player found {item.Name}");
            yield return DialogueManager.Instance.ShowDialogue(d);
        }
        //if (Used)
        //{
        //    DontDestroyOnLoad(this.gameObject);

        //}
    }
}
