using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

namespace Bardent
{
    public class MissHitBox : MonoBehaviour
    {
       
            public TranceBarTest tranceBarTest;
            public TextMeshProUGUI textIndicator;



            private void OnTriggerEnter2D(Collider2D other)
            {
                if (other.CompareTag("TranceBeat"))
                {
                    // The "TranceBeat" object has triggered the collider.
                    Debug.Log("Miss");
                   // tranceBarTest.comboCount++;
                    textIndicator.text = "Miss ".ToString();
                    // You can perform any actions or logic here.
                }
            }
        }
    }


