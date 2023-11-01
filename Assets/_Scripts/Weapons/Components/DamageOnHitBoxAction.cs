using Bardent.Combat.Damage;
using UnityEngine;
using static Bardent.Utilities.CombatDamageUtilities; //(2)

namespace Bardent.Weapons.Components
{
    public class DamageOnHitBoxAction : WeaponComponent<DamageOnHitBoxActionData, AttackDamage>
    {
        private ActionHitBox hitBox;
        private TranceBar tranceBarScript;
        private void HandleDetectCollider2D(Collider2D[] colliders)
        {
            // Notice that this is equal to (1), the logic has just been offloaded to a static helper class. Notice the using statement (2) is static, allowing as to call the Damage function directly instead of saying
            // Bardent.Utilities.CombatUtilities.Damage(...);
            TryDamage(colliders, new DamageData(currentAttackData.Amount, Core.Root), out _);

            //(1)
            // foreach (var item in colliders)
            // {
            //     if (item.TryGetComponent(out IDamageable damageable))
            //     {
            //         damageable.Damage(new Combat.Damage.DamageData(currentAttackData.Amount, Core.Root));
            //     }
            // }

            if (tranceBarScript != null)
            {
                if (tranceBarScript.tranceLevel ==1)
                {
                 TryDamage(colliders, new DamageData(currentAttackData.Amount+5, Core.Root), out _);
                }// Access methods or properties of the tranceBarTest script
                if (tranceBarScript.tranceLevel ==2)
                {
                    TryDamage(colliders, new DamageData(currentAttackData.Amount + 15, Core.Root), out _);
                }// Access methods or properties of the tranceBarTest script
                if (tranceBarScript.tranceLevel == 3)
                {
                    TryDamage(colliders, new DamageData(currentAttackData.Amount + 25, Core.Root), out _);
                }// Access methods or properties of the tranceBarTest script
            }

        }

        protected override void Start()
        {
            base.Start();

            hitBox = GetComponent<ActionHitBox>();
            
            hitBox.OnDetectedCollider2D += HandleDetectCollider2D;

            GameObject barObject = GameObject.Find("CanvasTrance/TranceBar");

            if (barObject != null)
            {
                // Try to get the "tranceBarTest" script from the "Bar" GameObject
                tranceBarScript = barObject.GetComponent<TranceBar>();

                if (tranceBarScript == null)
                {
                    Debug.LogError("tranceBarTest script not found on the 'Bar' GameObject.");
                }
            }
            else
            {
                Debug.LogError("GameObject 'Bar' not found in the scene.");
            }

        }

        protected override void OnDestroy()
        {
            base.OnDestroy();




            hitBox.OnDetectedCollider2D -= HandleDetectCollider2D;
        }
    }
}