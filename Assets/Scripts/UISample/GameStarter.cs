using System;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameStarter : MonoBehaviour
{
    public event Action StartGame;

    public void Play()
    {
        StartGame?.Invoke();
    }
}
