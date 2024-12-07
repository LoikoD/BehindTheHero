using CodeBase.Player;
using DG.Tweening;
using System.Collections;
using System.Security.Claims;
using TMPro;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.UI;

public class DialogueSystem : MonoBehaviour
{
    [SerializeField] private int _dialogueNum;
    [SerializeField] private string[] _names;
    [SerializeField] private string[] _texts;
    [SerializeField] private Sprite[] _icons;
    [SerializeField] private int _flashBackStart;
    [SerializeField] private int _flashBackEnd;
    [SerializeField] private float _typingDelay = 0.05f;

    [Space]
    [Space]
    [SerializeField] private TextAsset _textAsset;

    [SerializeField] private TMP_Text _dialogueTitle;
    [SerializeField] private TMP_Text _dialogueText;
    [SerializeField] private Image _dialogueIcon;

    [SerializeField] private Image _flashbackImage;
    [SerializeField] private AudioClip _audioClip;

    [SerializeField] private SceneLoader _sceneLoader;


    private PlayerInputActions _playerInputActions;
    private AudioSource _audioSource;
    private readonly string _symbolsToDelay = ".?!";
    private bool _isSkipPressed = false;


    private void Start()
    {
        _audioSource = GetComponent<AudioSource>();


        _playerInputActions = new PlayerInputActions();
        _playerInputActions.Dialogue.Enable();
        _playerInputActions.Dialogue.Skip.performed += OnSkip;

        StartCoroutine(StartDialogue());
    }
    private void OnDisable()
    {
        _playerInputActions.Dialogue.Skip.performed -= OnSkip;
        _playerInputActions.Dialogue.Disable();
    }

    public IEnumerator StartDialogue()
    {
        for (int i = 0; i < _names.Length; i++)
        {
            _isSkipPressed = false;

            if (i != 0)
                if (_flashBackStart == i)
                    _flashbackImage.DOFade(1, 1);
                else if (_flashBackEnd == i)
                    _flashbackImage.DOFade(0, 1);
            _dialogueTitle.text = _names[i];
            _dialogueIcon.overrideSprite = _icons[i];
            _dialogueText.text = "";
            for (int j = 0; j < _texts[i].Length; j++)
            {
                _audioSource.PlayOneShot(_audioClip);
                if (_isSkipPressed)
                {
                    _dialogueText.text = _texts[i];
                    yield return new WaitForSeconds(_typingDelay * 10);
                    break;
                }
                else
                {
                    _dialogueText.text += _texts[i][j];
                    yield return new WaitForSeconds(_symbolsToDelay.Contains(_texts[i][j]) ? _typingDelay * 10 : _typingDelay);
                }

            }
            while (!_playerInputActions.Dialogue.Skip.IsPressed())
                yield return null;
        }
        
        _sceneLoader.SceneChange(_dialogueNum);
    }

    private void OnSkip(InputAction.CallbackContext context)
    {
        _isSkipPressed = true;
    }
}
