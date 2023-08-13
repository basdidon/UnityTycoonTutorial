using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Card")]
public class CardData : ScriptableObject
{ 
    [field: SerializeField] public Sprite Sprite { get; set; }    
}
