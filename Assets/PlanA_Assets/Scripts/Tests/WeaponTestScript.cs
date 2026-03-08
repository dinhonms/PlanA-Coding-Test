using NUnit.Framework;

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
            var weapon = new WeaponLogic(10); // Tests are now dependent on logic not concrete objects
            var target = new FakeDamageable();
            
            // Act (run code logic)
            weapon.Fire(target);
            
            // Assert (test working logic)
            Assert.AreEqual(10, target.DamageTaken);
        }
    }
}
