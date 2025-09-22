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
        string input = EditorUtility.DisplayDialogComplex("안내", "하단 Console에 URL을 입력하는 UI 대신 간단 입력창을 띄우는 버전으로 교체할게다 해.", "OK", "Cancel", "") == 0
            ? EditorUtility.DisplayDialog("URL 입력 방법", "다음 메뉴를 사용: Cards/Pull CSV (처음 호출 시 URL 입력 창이 떠) ~", "확인").ToString()
            : cur;
    }

    [MenuItem("Cards/Pull CSV")]
    public static async void Pull()
    {
        string url = EditorPrefs.GetString(PrefKey, "");
        if (string.IsNullOrEmpty(url))
        {
            url = EditorUtility.DisplayDialogComplex("CSV URL 미설정", "구글시트 CSV 퍼블리시 URL을 입력하라 해.", "입력", "취소", "") == 0
                ? EditorUtility.DisplayDialog("입력 방법", "Project → Preferences… 가 아니라, 이번엔 팝업 Input으로 바로 받는다 해.", "확인").ToString()
                : null;
        }
        // 간편 input
        url = EditorUtility.DisplayDialogComplex("URL 입력", "아래 입력 창에 CSV URL 붙여넣어라 해.", "OK", "Cancel", "") == 0
            ? EditorUtility.OpenFilePanel("여긴 파일패널이지만 URL도 붙일 수 있다 해(붙여넣고 Enter)", "", "") // 편법: 경로칸에 URL 붙이기
            : url;

        if (string.IsNullOrEmpty(url)) { Debug.LogWarning("취소됨"); return; }
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
            Debug.LogError($"[Cards] CSV 다운로드 실패: {req.error}");
            return;
        }
        Directory.CreateDirectory(Path.GetDirectoryName(SavePath)!);
        File.WriteAllBytes(SavePath, req.downloadHandler.data);
        AssetDatabase.ImportAsset(SavePath);
        Debug.Log($"[Cards] CSV 갱신 완료: {SavePath}");
    }
}
#endif
