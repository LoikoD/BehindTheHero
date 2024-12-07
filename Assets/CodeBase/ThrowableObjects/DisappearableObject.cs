using CodeBase.ThrowableObjects.Pool;
using System.Collections;
using System.Collections.Generic;
using CodeBase.StaticData;
using UnityEngine;

namespace CodeBase.ThrowableObjects
{
    public class DisappearableObject : MonoBehaviour
    {
        [SerializeField] private DisappearableObjectStaticData _staticData;

        private Color _originColor;
        private SpriteRenderer _spriteRenderer;
        private Coroutine _disappearCoroutine;

        public bool IsDisappearing => _disappearCoroutine != null;

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
            StopCoroutine(_disappearCoroutine);
            _spriteRenderer.color = _originColor;
        }

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

            _spriteRenderer.color = _originColor;
            
            ThrowableObjectPool.ReturnObjectToPool(gameObject);
        }
    }
}