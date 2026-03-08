using System;
using PlanA_Assets.Scripts.Tests.Example;
using UnityEngine;

namespace PlanA_Assets.Scripts
{
    public interface IDamageable
    {
        void TakeDamage(int damage);
    }

    public interface IWeapon
    {
        void Fire(IDamageable target);
    }
    
    /// <summary>
    /// Evolved to use Interfaces instead of concrete monoBehaviours, brings decoupling
    /// Classes should rely on behavior, not concrete
    /// Easy to modify, extend (doesn't require re-wiring)
    /// Easy to test
    /// Adds reusability
    /// </summary>
    public class Player: MonoBehaviour
    {
        // Need a way to direct reference for tests
        // Interfaces doesn't provide that
        [SerializeField] private MonoBehaviour weaponBehaviour;
        [SerializeField] private MonoBehaviour targetBehaviour;
        
        // It could be any weapon
        private IWeapon _weapon;
        // It could be any target enemy
        private IDamageable _target;

        private void Awake()
        {
            // We can cast them to our internal interfaces
            _weapon = weaponBehaviour as IWeapon;
            _target = targetBehaviour as IDamageable;
        }

        private void Update()
        {
            if (Input.GetButtonDown("Fire1"))
            {
                _weapon.Fire(_target);
            }
        }
    }

    public class Weapon : MonoBehaviour, IWeapon
    {
        // Separate data from logic: Logic decides what happens, data decides the values
        // Data becomes flexible
        // Designers can tune without code changes
        [SerializeField] private WeaponConfig _weaponConfig;
        
        // MonoBehaviours owns logic, though it's outside it
        private WeaponLogic _weaponLogic;

        private void Awake()
        {
            _weaponLogic = new WeaponLogic(_weaponConfig);
        }

        public void Fire(IDamageable target)
        {
            _weaponLogic.Fire(target);
        }
    }

    /// <summary>
    /// Separate MonoBehaviour from logic. This class knows nothing about Unity objects (physics, transforms, go, etc)
    /// Adds behaviour without Unity
    /// Logic easier to test
    /// Separation of concerns (monobehaviour owns lifecycle not logic)
    /// </summary>
    public class WeaponLogic
    {
        private readonly WeaponConfig _weaponConfig;

        public WeaponLogic(WeaponConfig weaponConfig)
        {
            _weaponConfig = weaponConfig;
        }

        public void Fire(IDamageable target)
        {
            target.TakeDamage(_weaponConfig.Damage);
        }
    }

    public class Enemy : MonoBehaviour, IDamageable
    {
        private int _health;
        
        public void TakeDamage(int damage)
        {
            _health -= damage;
        }
    }
}