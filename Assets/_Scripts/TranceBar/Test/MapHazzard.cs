using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Bardent
{
    public class MapHazzard : MonoBehaviour
    {
        public HealthBar healthBar; // Assuming the HealthBar script is attached to the same game object

        private void OnTriggerEnter(Collider other)
        {
            if (other.CompareTag("Hazard")) // Check if the entering collider has the "Hazard" tag
            {
                // Reduce the player's health when the "HazardDetect" collider enters the "Hazard" object
                healthBar.currentHealth -= 10;
            }
        }

        // Rest of your code

        // Start is called before the first frame update
        void Start()
        {

        }

        // Update is called once per frame
        void Update()
        {

        }
    }
}
