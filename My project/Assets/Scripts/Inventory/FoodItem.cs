using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Items/Create new recovery item")]
public class FoodItem : ItemBase
{
    [Header("Character")]
    [SerializeField] string character;
}
