using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Bardent
{
    public class BorderHandler : MonoBehaviour
    {
        public TranceBar tranceBar; // Reference to the TranceBar script
        public GameObject borderObject; // The GameObject to activate/deactivate
        public GameObject borderObject2;
        private void Update()
        {
            if (tranceBar != null)
            {
                float timerPercentage = tranceBar.timer / 2f;
                //int beatCount = tranceBar.beatCount;

                if ((timerPercentage >= 0.5f && timerPercentage <= 0.6f && tranceBar.beatCount < 2) ||
                    (timerPercentage >= 0.7f && timerPercentage <= 0.8f && tranceBar.beatCount == 2))
                {
                    borderObject.SetActive(true);
                }

                
                else
                {
                    borderObject.SetActive(false);
                }


                 if ((timerPercentage >= 0.3f && timerPercentage <= 0.49f && tranceBar.beatCount < 2) ||
                    ( timerPercentage >= 0.61f && timerPercentage <= 0.8f && tranceBar.beatCount < 2) ||
                    (timerPercentage >= 0.5f && timerPercentage <= 0.69f && tranceBar.beatCount == 2))
                {
                    borderObject2.SetActive(true);
                }

                else
                {
                    borderObject2.SetActive(false);
                }

            }
        }
    }
}