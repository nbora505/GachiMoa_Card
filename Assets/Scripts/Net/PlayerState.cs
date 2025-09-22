using Photon.Pun;
using Photon.Realtime;
using ExitGames.Client.Photon;

public static class PlayerKeys
{
    public const string Hand = "hand";
    public const string HP = "hp";
}

public static class PlayerState
{
    public static string[] GetHand(Player p)
    {
        return System.Array.Empty<string>();
    }

    public static void SetHand(Player p, string[] ids)
    {
        var h = new Hashtable { { PlayerKeys.Hand, string.Join(",", ids) } };
        p.SetCustomProperties(h);
    }
}
