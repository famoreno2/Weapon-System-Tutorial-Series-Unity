using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

namespace Bardent
{
    public class PerfectHitBox : MonoBehaviour
    {

        public TranceBarTest tranceBarTest;
        public TextMeshProUGUI textIndicator;



        private void OnTriggerEnter2D(Collider2D other)
        {
            if (other.CompareTag("TranceBeat"))
            {
                // The "TranceBeat" object has triggered the collider.
                Debug.Log("combo refreshed");
                tranceBarTest.comboCount++;
                tranceBarTest.comboTimer = 5f;
                textIndicator.text = "Perfect " .ToString();
                // You can perform any actions or logic here.
            }
        }






        
    }
}
