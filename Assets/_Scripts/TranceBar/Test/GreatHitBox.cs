using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

namespace Bardent
{
    public class GreatHitBox : MonoBehaviour
    {
        public TranceBarTest tranceBarTest;
       // public TextMeshProUGUI textIndicator;
        public BeatCheck beatCheck;



        private void OnTriggerEnter2D(Collider2D other)
        {
            if (other.CompareTag("BeatActivator"))
            {
                // The "TranceBeat" object has triggered the collider.
               beatCheck.beathitbox.enabled = true;
                beatCheck.isActive = true;
               // beatCheck.enablerHitbox.enabled = false;
                // You can perform any actions or logic here.
            }
        }
    }
}
