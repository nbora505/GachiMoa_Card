using Photon.Pun;
using ExitGames.Client.Photon;
using UnityEngine;
using System;
using System.Linq;

public static class RoomKeys
{
    public const string Seed = "seed";
    public const string Turn = "turn";   // 0 = Master, 1 = Other
    public const string DeckId = "deckId";
}

public class MatchBootstrap : MonoBehaviourPunCallbacks
{
    public static string[] ShuffledDeck;

    [SerializeField] string defaultDeckId = "Basic";

    void Start()
    {
        if (PhotonNetwork.IsMasterClient)
        {
            int seed = UnityEngine.Random.Range(0, int.MaxValue);
            string deckId = defaultDeckId;

            PhotonNetwork.CurrentRoom.SetCustomProperties(new Hashtable{
                { RoomKeys.Seed, seed },
                { RoomKeys.Turn, 0   },
                { RoomKeys.DeckId, deckId }
            });
        }

        //int s = (int)PhotonNetwork.CurrentRoom.CustomProperties[RoomKeys.Seed];
        //string deckIdProp = (string)PhotonNetwork.CurrentRoom.CustomProperties[RoomKeys.DeckId];

        //var baseDeck = DeckProviderFromSheet.GetDeckById(deckIdProp);
        //if (baseDeck.Length == 0)
        //{
        //    Debug.LogError($"Deck '{deckIdProp}' is empty. Check spreadsheet.");
        //    ShuffledDeck = Array.Empty<string>();
        //    return;
        //}

        //var rng = new System.Random(s);
        //ShuffledDeck = baseDeck.OrderBy(_ => rng.Next()).ToArray();
    }
}

