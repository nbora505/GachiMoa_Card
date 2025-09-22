using System;
using System.Collections.Generic;
using UnityEngine;

public enum Rarity { Common, Rare, Epic, Legendary }

[Serializable]
public class CardData
{
    public string id;
    public string name;
    public Rarity Rarity;
    public int at;
    public int hp;
    public string abilityName;
    public string abilityDescription;
    //public Sprite artwork;
}