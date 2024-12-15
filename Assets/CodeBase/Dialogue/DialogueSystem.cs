using CodeBase.StaticData;
using DG.Tweening;
using System;
using System.Collections;
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

        private void OnEnable()
        {
            _playerInputActions = new();
            _playerInputActions.Dialogue.Enable();
            _playerInputActions.Dialogue.Skip.performed += OnSkip;
        }

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
        }

        public void StartDialogue()
        {
            _dialogueTitle.gameObject.SetActive(true);
            _dialogueText.gameObject.SetActive(true);
            _dialogueIcon.gameObject.SetActive(true);
            StartCoroutine(StartDialogueRoutine());
        }

        public IEnumerator StartDialogueRoutine()
        {
            for (int i = 0; i < _dialogueData.Blocks.Count; i++)
            {
                DialogueBlock block = _dialogueData.Blocks[i];

                SetBlockUI(i, block);

                yield return TypeText(block.Text);

                _isSkipPressed = false;
                while (!_isSkipPressed)
                    yield return null;
            }

            EndScene?.Invoke();
        }

        private void SetBlockUI(int blockNumber, DialogueBlock block)
        {
            if (blockNumber != 0)
                if (_dialogueData.FlashBackStart == blockNumber)
                    _flashbackImage.DOFade(1, 1);
                else if (_dialogueData.FlashBackEnd == blockNumber)
                    _flashbackImage.DOFade(0, 1);

            _dialogueTitle.text = block.Character.Name;
            _dialogueIcon.overrideSprite = block.Character.Icon;
            _dialogueText.text = "";
        }

        private IEnumerator TypeText(string text)
        {
            _isSkipPressed = false;
            _typingSoundsController.StartTypingSounds();

            string[] words = text.Split(' ');
            for (int j = 0; j < words.Length; j++)
            {
                yield return PrintWord(words[j]);
            }

            _typingSoundsController.StopTypingSounds();
        }

        private IEnumerator PrintWord(string word)
        {
            if (!string.IsNullOrEmpty(_dialogueText.text))
            {
                string prevText = _dialogueText.text;
                string testText = _dialogueText.text + word + " ";
                _dialogueText.text = IsWordOutOfBounds(testText) ? prevText.TrimEnd() + "\n" : prevText;
            }

            if (_isSkipPressed)
            {
                _dialogueText.text += word;
            }
            else
            {
                foreach (char ch in word)
                {
                    yield return PrintChar(ch);
                }
            }

            _dialogueText.text += " ";
        }

        private IEnumerator PrintChar(char ch)
        {
            _dialogueText.text += ch;

            if (_isSkipPressed)
            {
                yield break;
            }

            if (EndSentenceSymbols.Contains(ch))
            {
                _typingSoundsController.StopForSeconds(_endSentenceDelay);
                yield return WaitWithSkip(_endSentenceDelay);
            }
            else
            {
                yield return WaitWithSkip(_typingDelay);
            }
        }
        private IEnumerator WaitWithSkip(float delay)
        {
            float elapsed = 0f;
            while (elapsed < delay)
            {
                if (_isSkipPressed)
                {
                    yield break;
                }

                elapsed += Time.deltaTime;
                yield return null;
            }
        }

        private bool IsWordOutOfBounds(string testText)
        {
            _dialogueText.text = testText;
            _dialogueText.ForceMeshUpdate();

            TMP_TextInfo textInfo = _dialogueText.textInfo;
            float currentLineWidth = textInfo.lineCount > 0
                ? textInfo.lineInfo[textInfo.lineCount - 1].length
                : 0;

            return currentLineWidth > _dialogueText.rectTransform.rect.width;
        }

        private void OnSkip(InputAction.CallbackContext context)
        {
            _isSkipPressed = true;
        }
    }
}