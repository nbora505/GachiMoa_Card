using System;
using System.Collections.Generic;
using UnityEngine;

/// <summary>You must approach through `GoogleSheetManager.SO<GoogleSheetSO>()`</summary>
public class GoogleSheetSO : ScriptableObject
{
	public List<Character> CharacterList;
}

[Serializable]
public class Character
{
	public string ID;
	public string Name;
	public string Rarity;
	public int AT;
	public int HP;
	public string SkillName;
	public string SkillScript;
	public string When;
	public string Who;
	public bool isGet;
}

