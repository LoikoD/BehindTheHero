using CodeBase.Logic.Utilities;
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
        [SerializeField] private float _typingDelay = 0.025f;
        [SerializeField] private TMP_Text _dialogueTitle;
        [SerializeField] private TMP_Text _dialogueText;
        [SerializeField] private Image _dialogueIcon;
        [SerializeField] private Image _flashbackImage;
        [SerializeField] private List<AudioClip> _printSoundClips;

        private readonly string SymbolsToDelay = ".?!";
        private readonly string PrintKey = "print";

        private DialogueStaticData _dialogueData;
        private PlayerInputActions _playerInputActions;
        private AudioSource _audioSource;
        private SoundQueuer _soundQueuer;

        private bool _isSkipPressed = false;

        public event Action<string> EndScene;

        private void OnDisable()
        {
            _playerInputActions.Dialogue.Skip.performed -= OnSkip;
            _playerInputActions.Dialogue.Disable();
        }

        public void Construct(DialogueStaticData dialogueData)
        {
            _audioSource = GetComponent<AudioSource>();

            _dialogueData = dialogueData;

            _playerInputActions = new();
            _playerInputActions.Dialogue.Enable();
            _playerInputActions.Dialogue.Skip.performed += OnSkip;

            _soundQueuer = new();
            _soundQueuer.RegisterSoundList(PrintKey, _printSoundClips);

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
                for (int j = 0; j < block.Text.Length; j++)
                {
                    _audioSource.PlayOneShot(_soundQueuer.GetNextSound(PrintKey));

                    if (_isSkipPressed)
                    {
                        _dialogueText.text = block.Text;
                        yield return new WaitForSeconds(_typingDelay * 10);
                        break;
                    }
                    else
                    {
                        _dialogueText.text += block.Text[j];
                        yield return new WaitForSeconds(SymbolsToDelay.Contains(block.Text[j]) ? _typingDelay * 10 : _typingDelay);
                    }

                }
                while (!_playerInputActions.Dialogue.Skip.IsPressed())
                    yield return null;
            }

            EndScene?.Invoke(_dialogueData.NextLevelName);
        }

        private void OnSkip(InputAction.CallbackContext context)
        {
            _isSkipPressed = true;
        }
    }
}