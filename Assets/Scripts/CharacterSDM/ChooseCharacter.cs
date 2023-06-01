using UnityEngine;
using UnityEngine.UI;

public class ChooseCharacter : MonoBehaviour
{
    public static int IndexCharacter { get; private set; }
    [SerializeField] private Sprite[] playerSprite;
    [SerializeField] private Image spriteCharacter;

    void Start()
    {
        spriteCharacter.sprite = playerSprite[IndexCharacter];
    }

    public void SetIndexCharacter(bool substract)
    {
        if (!substract)
        {
            IndexCharacter++;
            if (IndexCharacter > playerSprite.Length - 1)
                IndexCharacter = 0;
        }
        else
        {
            IndexCharacter--;
            if (IndexCharacter < 0)
                IndexCharacter = playerSprite.Length - 1;
        }
        spriteCharacter.sprite = playerSprite[IndexCharacter];
    }
}
