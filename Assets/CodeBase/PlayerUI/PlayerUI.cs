using CodeBase.Knight;
using UnityEngine;
using UnityEngine.UI;

public class PlayerUI : MonoBehaviour
{
    [SerializeField] private Image _sprite;
    private KnightMain _knight;

    public void Construct(KnightMain defender)
    {
        _knight = defender;
        _knight.HealthChanged += FillBar;
    }

    void FillBar()
    {
        _sprite.fillAmount = _knight.CurrentHealth / _knight.MaxHealth;
    }
}
