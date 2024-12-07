using DG.Tweening;
using UnityEngine;
using UnityEngine.UI;

public class ScreenFade : MonoBehaviour
{
    private Image image;
    private void Start()
    {
        image = GetComponent<Image>();
        image.color = Color.black;
        ScreenBlackout(true);
    }
    public void ScreenBlackout(bool reverse = false)
    {
        if (reverse)
            image.DOFade(0, 0.3f);
        else
            image.DOFade(1, 0.3f);
    }
}
