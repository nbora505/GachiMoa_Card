using System.Collections.Generic;
using UnityEngine;

public class CardDB : MonoBehaviour
{
    public static CardDB I;
    readonly Dictionary<string, CardRuntime> _map = new();
    readonly Dictionary<string, Sprite> _spriteCache = new();

    void Awake()
    {
        I = this;
        BuildFromGoogleSheetSO();
    }

    void BuildFromGoogleSheetSO()
    {
        var so = GoogleSheetManager.SO<GoogleSheetSO>();
        if (so == null) { Debug.LogError("GoogleSheetSO is null. Run FetchGoogleSheet in Editor."); return; }

        // GoogleSheetClass.cs 의 생성 결과(예: Cards 클래스/ CardsList)를 사용
        foreach (var row in so.CharacterList)
        {
            var id = row.ID;
            if (string.IsNullOrEmpty(id)) continue;

            //Sprite art = null;
            //if (!string.IsNullOrEmpty(row.ArtworkPath))
            //{
            //    if (!_spriteCache.TryGetValue(row.ArtworkPath, out art))
            //    {
            //        art = Resources.Load<Sprite>(row.ArtworkPath);
            //        _spriteCache[row.ArtworkPath] = art;
            //    }
            //}

            _map[id] = new CardRuntime
            {
                ID = id,
                Name = row.Name,
                Rarity = row.Rarity,
                AT = row.AT,
                HP = row.HP,
                When = row.When,
                Who = row.Who,
                SkillName = row.SkillName,
                SkillScript = row.SkillScript,
                isGet = row.isGet
            };
        }

        Debug.Log($"CardDBFromSheet: loaded cards = {_map.Count}");
    }

    public bool TryGet(string id, out CardRuntime card) => _map.TryGetValue(id, out card);
    public CardRuntime Get(string id) => _map[id];
}
