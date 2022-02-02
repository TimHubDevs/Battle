using System;
using UnityEngine;
using UnityEngine.UI;

public class MinionHealth : MonoBehaviour
{
    [SerializeField] private Slider _healthSlider;
    private FloatVariable _healthSo;
    public Action<GameObject> onDeath;

    public void SetSOHealth(FloatVariable health, FloatVariable minionDataMaxhealth)
    {
        _healthSo = health;
        _healthSlider.maxValue = minionDataMaxhealth.Value;
    }

    private void Update()
    {
        if (!_healthSo) return;
        if (_healthSo.Value <= 0)
        {
            onDeath.Invoke(gameObject);
            _healthSo.Value = 1;
        }
        _healthSlider.value = _healthSo.Value;
    }

    public void GetDamage(float amount)
    {
        _healthSo.Init(_healthSo.Value - amount);
    }
}
