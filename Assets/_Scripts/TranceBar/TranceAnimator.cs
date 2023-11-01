using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Bardent
{
    public class TranceAnimator : MonoBehaviour
    {
        public Animator animator;
        public TranceBar tranceBar; // Reference to the TranceBar script

        // Add a public property to access tranceLevel
        public int TranceLevel
        {
            get
            {
                if (tranceBar != null)
                {
                    return tranceBar.tranceLevel;
                }
                else
                {
                    Debug.LogError("TranceBar reference is not set in TranceAnimator!");
                    return 0; // Return a default value
                }
            }
        }

        private void Update()
        {
            if (tranceBar != null)
            {
                if (tranceBar.tranceLevel > 0)
                {
                    // Double the animation speed
                    animator.SetFloat("Sword_Attack_1_SpeedMultiplier", 2.0f);
                    animator.SetFloat("Sword_Attack_2_SpeedMultiplier", 2.0f);
                    animator.SetFloat("Sword_Attack_3_SpeedMultiplier", 2.0f);
                }
                else
                {
                    // Reset animation speed to normal
                    animator.SetFloat("Sword_Attack_1_SpeedMultiplier", 1.0f);
                    animator.SetFloat("Sword_Attack_2_SpeedMultiplier", 1.0f);
                    animator.SetFloat("Sword_Attack_3_SpeedMultiplier", 1.0f);
                }
            }
            else
            {
                Debug.LogError("TranceBar reference is not set in TranceAnimator!");
            }
        }




        // You can use this property like this:
        // int currentTranceLevel = TranceLevel;
    }
}
