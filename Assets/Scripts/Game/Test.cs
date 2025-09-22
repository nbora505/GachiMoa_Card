using UnityEngine;

public class Test : MonoBehaviour
{
    GoogleSheetSO SO;

    void Start()
    {
        SO = GoogleSheetManager.SO<GoogleSheetSO>();

        for (int i = 0; i < SO.CharacterList.Count; i++)
        {
            Debug.Log(SO.CharacterList[i].Name);
        }
    }
}