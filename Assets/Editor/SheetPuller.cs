#if UNITY_EDITOR
using UnityEditor;
using UnityEngine;
using System.IO;
using UnityEngine.Networking;
using System.Threading.Tasks;

public static class SheetPuller
{
    const string PrefKey = "Cards_CSV_URL";
    const string SavePath = "Assets/Resources/Data/cards.csv";

    [MenuItem("Cards/Set CSV URL...")]
    public static void SetUrl()
    {
        string cur = EditorPrefs.GetString(PrefKey, "");
        string input = EditorUtility.DisplayDialogComplex("�ȳ�", "�ϴ� Console�� URL�� �Է��ϴ� UI ��� ���� �Է�â�� ���� �������� ��ü�ҰԴ� ��.", "OK", "Cancel", "") == 0
            ? EditorUtility.DisplayDialog("URL �Է� ���", "���� �޴��� ���: Cards/Pull CSV (ó�� ȣ�� �� URL �Է� â�� ��) ~", "Ȯ��").ToString()
            : cur;
    }

    [MenuItem("Cards/Pull CSV")]
    public static async void Pull()
    {
        string url = EditorPrefs.GetString(PrefKey, "");
        if (string.IsNullOrEmpty(url))
        {
            url = EditorUtility.DisplayDialogComplex("CSV URL �̼���", "���۽�Ʈ CSV �ۺ��� URL�� �Է��϶� ��.", "�Է�", "���", "") == 0
                ? EditorUtility.DisplayDialog("�Է� ���", "Project �� Preferences�� �� �ƴ϶�, �̹��� �˾� Input���� �ٷ� �޴´� ��.", "Ȯ��").ToString()
                : null;
        }
        // ���� input
        url = EditorUtility.DisplayDialogComplex("URL �Է�", "�Ʒ� �Է� â�� CSV URL �ٿ��־�� ��.", "OK", "Cancel", "") == 0
            ? EditorUtility.OpenFilePanel("���� �����г������� URL�� ���� �� �ִ� ��(�ٿ��ְ� Enter)", "", "") // ���: ���ĭ�� URL ���̱�
            : url;

        if (string.IsNullOrEmpty(url)) { Debug.LogWarning("��ҵ�"); return; }
        EditorPrefs.SetString(PrefKey, url);
        await Download(url);
    }

    static async Task Download(string url)
    {
        using var req = UnityWebRequest.Get(url);
        var op = req.SendWebRequest();
        while (!op.isDone) await Task.Yield();

        if (req.result != UnityWebRequest.Result.Success)
        {
            Debug.LogError($"[Cards] CSV �ٿ�ε� ����: {req.error}");
            return;
        }
        Directory.CreateDirectory(Path.GetDirectoryName(SavePath)!);
        File.WriteAllBytes(SavePath, req.downloadHandler.data);
        AssetDatabase.ImportAsset(SavePath);
        Debug.Log($"[Cards] CSV ���� �Ϸ�: {SavePath}");
    }
}
#endif
