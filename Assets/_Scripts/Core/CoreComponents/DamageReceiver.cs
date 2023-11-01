using Bardent.Combat.Damage;
using Bardent.ModifierSystem;
using UnityEngine;

namespace Bardent.CoreSystem
{
    public class DamageReceiver : CoreComponent, IDamageable
    {
        [SerializeField] private GameObject damageParticles;

        /*
         * Modifiers allows us to perform some custom logic on our DamageData before we apply it here. An example where this is being used is by the Block weapon component.
         * Blocking works by assigning a modifier during the active block window of the shield that reduces the amount of damage the player will take. For example: If a shield
         * has a damage absorption property of 0.75 and we deal 10 damage, only 2.5 will actually end up getting removed from player stats after applying the modifier.
         */
        public Modifiers<Modifier<DamageData>, DamageData> Modifiers { get; } = new();

        private Stats stats;
        private ParticleManager particleManager;
        public TranceBar tranceBarScript;


        public void Damage(DamageData data)
        {
            if (tranceBarScript != null)
            {
               // int tranceLevel = tranceBarScript.tranceLevel;

                int damageIncrease = 0;
                //if (tranceBarScript.tranceLevel == 2)
               // {
                //    damageIncrease = 50;
               // }
               // else if (tranceLevel == 3)
               // {
                 //   damageIncrease = 100;
               // }

                // Create a new instance of DamageData with the modified amount.
                DamageData modifiedData = new DamageData(data.Amount + damageIncrease, data.Source); // Pass the required 'source' parameter.

                print($"Damage Amount Before Modifiers: {modifiedData.Amount}");

                // Apply modifiers to the modified data.
                modifiedData = Modifiers.ApplyAllModifiers(modifiedData);

                print($"Damage Amount After Modifiers: {modifiedData.Amount}");

                if (modifiedData.Amount <= 0f)
                {
                    return;
                }

                stats.Health.Decrease(data.Amount);
                particleManager.StartWithRandomRotation(damageParticles);
            }
        }

        protected override void Awake()
        {
            base.Awake();

            stats = core.GetCoreComponent<Stats>();
            particleManager = core.GetCoreComponent<ParticleManager>();
        }
    }
}