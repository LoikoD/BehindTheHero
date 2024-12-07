using DG.Tweening;
using System.Collections;
using UnityEngine;
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

    [SerializeField] private Text _dialogueTitle;
    [SerializeField] private Text _dialogueText;
    [SerializeField] private Image _dialogueIcon;

    [SerializeField] private Image _flashbackImage;
    [SerializeField] private AudioClip _audioClip;

    [SerializeField] private SceneLoader _sceneLoader;

    //private DialogueLoader _loader;
    //private Dialogue _dialogue;
    private AudioSource _audioSource;
    private string _symbolsToDelay = ".?!";


    private void Start()
    {
        _audioSource = GetComponent<AudioSource>();
        //_loader = gameObject.AddComponent<DialogueLoader>();
        //_dialogue = _loader.LoadDialogue(_textAsset.text);


        //
        //_dialogue = ScriptableObject.CreateInstance<Dialogue>();
        //_dialogue.Prases = new List<Phrase>();
        //Phrase _phrase1 = ScriptableObject.CreateInstance<Phrase>();
        //_phrase1.Name = "�������";
        //_phrase1.Text = "Hello World! Heh";
        //_phrase1.IconPath = "mutantExample";
        //_dialogue.Prases.Add(_phrase1);
        //_dialogue.Prases.Add(_phrase1);
        //_dialogue.Prases.Add(_phrase1);
        //

        StartCoroutine(StartDialogue());
    }

    public IEnumerator StartDialogue()
    {
        for (int i = 0; i < _names.Length; i++)
        {
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
                _dialogueText.text = _dialogueText.text + _texts[i][j];
                _audioSource.PlayOneShot(_audioClip);
                yield return new WaitForSeconds(_symbolsToDelay.Contains(_texts[i][j]) ? _typingDelay * 10 : _typingDelay);
            }
            while (!Input.GetMouseButtonDown(0))
                yield return null;
        }
        
        _sceneLoader.SceneChange(_dialogueNum);
    }
}
