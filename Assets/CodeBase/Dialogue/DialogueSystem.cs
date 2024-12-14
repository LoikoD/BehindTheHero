using CodeBase.StaticData;
using DG.Tweening;
using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.UI;

namespace CodeBase.Dialogue
{
    public class DialogueSystem : MonoBehaviour
    {
        [SerializeField] private float _typingDelay;
        [SerializeField] private float _endSentenceDelay;
        [SerializeField] private TMP_Text _dialogueTitle;
        [SerializeField] private TMP_Text _dialogueText;
        [SerializeField] private Image _dialogueIcon;
        [SerializeField] private Image _flashbackImage;

        private readonly string EndSentenceSymbols = ".?!";

        private DialogueStaticData _dialogueData;
        private PlayerInputActions _playerInputActions;
        private TypingSoundsController _typingSoundsController;

        private bool _isSkipPressed = false;

        public event Action EndScene;

        private void OnDisable()
        {
            _playerInputActions.Dialogue.Skip.performed -= OnSkip;
            _playerInputActions.Dialogue.Disable();
        }

        public void Construct(DialogueStaticData dialogueData)
        {
            _typingSoundsController = GetComponent<TypingSoundsController>();
            _typingSoundsController.Construct();

            _dialogueData = dialogueData;

            _playerInputActions = new();
            _playerInputActions.Dialogue.Enable();
            _playerInputActions.Dialogue.Skip.performed += OnSkip;

            StartCoroutine(StartDialogueRoutine());
        }

        public IEnumerator StartDialogueRoutine()
        {
            for (int i = 0; i < _dialogueData.Blocks.Count; i++)
            {
                DialogueBlock block = _dialogueData.Blocks[i];
                _isSkipPressed = false;

                if (i != 0)
                    if (_dialogueData.FlashBackStart == i)
                        _flashbackImage.DOFade(1, 1);
                    else if (_dialogueData.FlashBackEnd == i)
                        _flashbackImage.DOFade(0, 1);
                _dialogueTitle.text = block.Character.Name;
                _dialogueIcon.overrideSprite = block.Character.Icon;
                _dialogueText.text = "";

                _typingSoundsController.StartTypingSounds();

                for (int j = 0; j < block.Text.Length; j++)
                {
                    if (_isSkipPressed)
                    {
                        _dialogueText.text = block.Text;
                        break;
                    }
                    else
                    {
                        _dialogueText.text += block.Text[j];
                        if (EndSentenceSymbols.Contains(block.Text[j]))
                        {
                            _typingSoundsController.StopForSeconds(_endSentenceDelay);
                            yield return new WaitForSeconds(_endSentenceDelay);
                        }
                        else
                        {
                            yield return new WaitForSeconds(_typingDelay);
                        }
                    }
                }
                _typingSoundsController.StopTypingSounds();
                while (!_playerInputActions.Dialogue.Skip.IsPressed())
                    yield return null;
            }

            EndScene?.Invoke();
        }

        private void OnSkip(InputAction.CallbackContext context)
        {
            _isSkipPressed = true;
        }
    }
}