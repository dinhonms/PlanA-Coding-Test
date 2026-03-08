using UnityEngine;

namespace PlanA_Assets.Scripts.Tests.Example
{
    [CreateAssetMenu(menuName = "Config/WeaponConfigSO")]
    public class WeaponConfig: ScriptableObject
    {
        [SerializeField] private int damage;
        
        public int Damage
        {
            get => damage;
            set => damage = value;
        }
    }
}