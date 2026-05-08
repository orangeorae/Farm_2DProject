using UnityEngine;
using UnityEngine.UI;

public class LoadUI : MonoBehaviour
{
    [SerializeField] private Image LoadImage;

    private void OnEnable()
    {
        Sprite loadedSprite = Resources.Load<Sprite>($"Loading_Image");

        if(loadedSprite != null)
        {
            LoadImage.sprite = loadedSprite;
        }
    }
}
