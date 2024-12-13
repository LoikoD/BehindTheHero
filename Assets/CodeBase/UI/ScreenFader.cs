using DG.Tweening;
using UnityEngine;
using UnityEngine.UI;

public class ScreenFader : MonoBehaviour
{
    [SerializeField] private Image _image;

    private void Awake()
    {
        DontDestroyOnLoad(this);
    }
    public void ScreenFadeOut(bool reverse = false)
    {
        if (reverse)
            _image.DOFade(1, 0.3f);
        else
            _image.DOFade(0, 0.3f);
    }

    public void SetBlack()
    {
        _image.color = Color.black;
    }
}
