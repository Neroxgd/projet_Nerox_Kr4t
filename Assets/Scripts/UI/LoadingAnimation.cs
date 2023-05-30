using UnityEngine;
using TMPro;
using System.Collections;

public class LoadingAnimation : MonoBehaviour
{
    [SerializeField] private float speed;
    [SerializeField] private string[] chars;
    private TextMeshProUGUI textMeshPro;
    private int compt;
    private string baseText;

    void Start()
    {
        textMeshPro = GetComponent<TextMeshProUGUI>();
        baseText = textMeshPro.text;
        StartCoroutine(AnimationText());
    }

    IEnumerator AnimationText()
    {
        if (compt == chars.Length)
        {
            compt = 0;
            textMeshPro.text = baseText;
        }
        else
        {
            textMeshPro.text += chars[compt];
            compt++;
        }
        yield return new WaitForSeconds(speed);
        StartCoroutine(AnimationText());
    }
}
