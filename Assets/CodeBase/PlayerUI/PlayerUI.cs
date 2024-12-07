using CodeBase.Knight;
using UnityEngine;
using UnityEngine.UI;

public class PlayerUI : MonoBehaviour
{
    [SerializeField] private Image _sprite;
    private KnightDefender _knight;

    public void Construct(KnightDefender defender)
    {
        _knight = defender;
        _knight.HealthChanged += FillBar;
    }

    void FillBar()
    {
        _sprite.fillAmount = _knight.Current / _knight.Max;
    }
}
