using UnityEngine;

[CreateAssetMenu(menuName = "Card")]
public class CardData : ScriptableObject
{
    public string id;
    public string cardName;
    public int atk;
    public int hp;
    public string abilityName;
    public string abilityDescription;
    public Sprite artwork;
}