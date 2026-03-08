using UnityEngine;

namespace PlanA_Assets.Scripts
{
    /// <summary>
    /// Typical Unity code: tightly coupled, makes test super hard
    /// It's ok early on, but becomes hard at scale
    /// </summary>
    public class Player: MonoBehaviour
    {
        // direct reference to concrete classes
        public Weapon weapon;

        private void Update()
        {
            if (Input.GetButtonDown("Fire1"))
            {
                weapon.Fire();
            }
        }
    }

    public class Weapon : MonoBehaviour
    {
        public int damage = 10;
        // direct reference
        public Enemy enemy;

        public void Fire()
        {
            enemy.TakeDamage(damage);
        }
    }

    public class Enemy : MonoBehaviour
    {
        private int _health;
        
        public void TakeDamage(int damage)
        {
            _health -= damage;
        }
    }
}