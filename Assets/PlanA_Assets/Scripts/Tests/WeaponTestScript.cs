using NUnit.Framework;
using UnityEngine;

namespace PlanA_Assets.Scripts.Tests
{
    public class FakeDamageable : IDamageable
    {
        public int DamageTaken;

        public void TakeDamage(int damage)
        {
            DamageTaken += damage;
        }
    }
    
    public class WeaponTestScript
    {
        [Test]
        public void Fire_DealsDamageToTarget()
        {
            // Arrange (prepare test conditions)
            var go = new GameObject();
            var weapon = go.AddComponent<Weapon>();
            
            var target = new FakeDamageable();
            
            // Act (run code logic)
            weapon.Fire(target);
            
            // Assert (test working logic)
            Assert.AreEqual(10, target.DamageTaken);
        }
    }
}
