using System.Collections;
using CodeBase.StaticData;
using UnityEngine;
using System;

namespace CodeBase.ThrowableObjects.Components.Disappearing
{
    public class DisappearableObject : MonoBehaviour, IDisappearable
    {
        [SerializeField] private DisappearableObjectStaticData _staticData;
        
        private Color _originColor;
        private SpriteRenderer _spriteRenderer;
        private Coroutine _disappearCoroutine;

        public event Action Disappeared;

        private void Awake()
        {
            _spriteRenderer = GetComponent<SpriteRenderer>();
            _originColor = _spriteRenderer.color;
        }

        public void StartDisappear()
        {
            _disappearCoroutine = StartCoroutine(DisappearRoutine());
        }

        public void StopDisappear()
        {
            if (_disappearCoroutine != null)
            {
                StopCoroutine(_disappearCoroutine);
                _disappearCoroutine = null;
            }

            ResetColor();
        }

        private void ResetColor() => 
            _spriteRenderer.color = _originColor;

        private IEnumerator DisappearRoutine()
        {
            float disappearTime = 0;
            bool isClear = false;

            while (disappearTime < _staticData.DisappearTime)
            {
                _spriteRenderer.color = isClear ? _originColor : Color.clear;
                isClear = !isClear;

                yield return new WaitForSeconds(_staticData.TimeBetweenFlashes);
                disappearTime += _staticData.TimeBetweenFlashes;
            }

            ResetColor();
            Disappeared?.Invoke();
        }
    }
}