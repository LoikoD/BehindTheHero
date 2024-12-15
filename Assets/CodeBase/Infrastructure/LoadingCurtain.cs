using DG.Tweening;
using System.Collections;
using TMPro;
using UnityEngine;

namespace CodeBase.Infrastructure
{
    public class LoadingCurtain : MonoBehaviour
    {
        [SerializeField] private CanvasGroup _curtain;
        [SerializeField] private TMP_Text _text;

        private void Awake()
        {
            DontDestroyOnLoad(this);
        }

        public void Show(TweenCallback callback, string text = "")
        {
            _text.text = text;
            Sequence sequence = DOTween.Sequence();
            sequence.Append(_curtain.DOFade(1, 0.5f));
            sequence.AppendCallback(callback);
        }

        public void Hide(TweenCallback callback)
        {
            Sequence sequence = DOTween.Sequence();
            sequence.Append(_curtain.DOFade(0, 0.5f));
            sequence.AppendCallback(callback);
        }
    }
}