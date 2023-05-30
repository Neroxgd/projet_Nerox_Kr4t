using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class ChooseCharacter : MonoBehaviour
{
    public static int IndexCharacter { get; private set; }
    public TextMeshProUGUI text;

    void Start()
    {
        text.text = IndexCharacter.ToString();
    }

    public void SetIndexCharacter(bool substract)
    {
        if (!substract)
            IndexCharacter++;
        else
            IndexCharacter--;
        text.text = IndexCharacter.ToString();    }
}
