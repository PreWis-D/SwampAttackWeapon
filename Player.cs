using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.EventSystems;

[RequireComponent(typeof(Animator))]
public class Player : MonoBehaviour
{
    [SerializeField] private int _health;
    [SerializeField] private List<Weapon> _weapons;
    [SerializeField] private Transform _shootPoint;

    private Weapon _currentWeapon;
    private int _currentWeaponNumber = 0;
    private int _currentHealth;
    private Animator _animator;
    private string _pistolLabel;
    private string _wizardAxeLabel;

    public int Money { get; private set; }

    public event UnityAction<int, int> HealthChanged;
    public event UnityAction<int> MoneyChanged;

    private void Start()
    {
        ChangeWeapon(_weapons[_currentWeaponNumber]);
        _currentWeapon = _weapons[0];
        _currentHealth = _health;
        _animator = GetComponent<Animator>();
        _pistolLabel = _weapons[_currentWeaponNumber].Label.ToString();
    }

    private void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            if (!EventSystem.current.IsPointerOverGameObject())
            {
                if (_currentWeapon.Label.ToString() == _pistolLabel)
                    _animator.SetTrigger("Shoot");

                if (_currentWeapon.Label.ToString() == _wizardAxeLabel)
                    _animator.SetTrigger("ShockWave");

                _currentWeapon.Shoot(_shootPoint);
            }
        }
    }

    public void ApplyDamage(int damage)
    {
        _currentHealth -= damage;
        HealthChanged?.Invoke(_currentHealth, _health);

        if (_currentHealth <= 0)
            Destroy(gameObject);
    }

    public void OnEnemyDied(int reward)
    {
        Money += reward;
    }

    public void AddMoney(int money)
    {
        Money += money;
        MoneyChanged?.Invoke(Money);
    }

    public void BuyWeapon(Weapon weapon)
    {
        Money -= weapon.Price;
        MoneyChanged?.Invoke(Money);
        _weapons.Add(weapon);

        if (weapon.TryGetComponent(out WizardAxe wizardAxe))
            _wizardAxeLabel = weapon.Label.ToString();
    }

    public void NextWeapon()
    {
        if (_currentWeaponNumber == _weapons.Count - 1)
            _currentWeaponNumber = 0;
        else
            _currentWeaponNumber++;

        ChangeAnimationWhenCgangeWeapon(_weapons[_currentWeaponNumber]);

        ChangeWeapon(_weapons[_currentWeaponNumber]);
    }

    public void PreviousWeapon()
    {
        if (_currentWeaponNumber == 0)
            _currentWeaponNumber = _weapons.Count - 1;
        else
            _currentWeaponNumber--;

        ChangeAnimationWhenCgangeWeapon(_weapons[_currentWeaponNumber]);

        ChangeWeapon(_weapons[_currentWeaponNumber]);
    }

    private void ChangeWeapon(Weapon weapon)
    {
        _currentWeapon = weapon;
    }

    private void ChangeAnimationWhenCgangeWeapon(Weapon weapon)
    {
        if (_currentWeapon.Label.ToString() == _pistolLabel && weapon.Label.ToString() == _wizardAxeLabel)
            _animator.SetTrigger("GunToAxe");

        if (_currentWeapon.Label.ToString() == _wizardAxeLabel && weapon.Label.ToString() == _pistolLabel)
            _animator.SetTrigger("AxeToGun");
    }
}
