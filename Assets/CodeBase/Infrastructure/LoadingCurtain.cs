using System.Collections;
using UnityEngine;

namespace CodeBase.Infrastructure
{
    public class LoadingCurtain : MonoBehaviour
    {
        [SerializeField] private CanvasGroup _curtain;
        
        private float _tick = 0.03f;
        private WaitForSeconds _timer;

        private void Awake()
        {
            DontDestroyOnLoad(this);

            _timer = new WaitForSeconds(_tick);
        }

        public void Show()
        {
            gameObject.SetActive(true);
            _curtain.alpha = 1f;
        }

        public void Hide() => 
            StartCoroutine(FadeIn());

        private IEnumerator FadeIn()
        {
            while (_curtain.alpha > 0)
            {
                _curtain.alpha -= _tick;

                yield return _timer;
            }
            
            gameObject.SetActive(false);
        }
    }
}