using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class TextUpdater : MonoBehaviour
{


    public string stringId;

    void Start()
    {
        if (GetComponent<Text>() != null)
            GetComponent<Text>().text = LocalizationText.GetText(stringId);
    }

}
