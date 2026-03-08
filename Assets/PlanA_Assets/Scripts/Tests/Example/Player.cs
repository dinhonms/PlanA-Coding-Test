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
        // It could be any weapon
        private Weapon _weapon;
        // It could be any target enemy
        private IDamageable _target;

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
        private int _damage = 10;

        public void Fire(IDamageable target)
        {
            target.TakeDamage(_damage);
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