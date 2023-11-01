using Bardent.CoreSystem;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Bardent.CoreSystem;
using TMPro;


namespace Bardent
{
    public class HealthBar : MonoBehaviour
    {
        public Stats stats; // Reference to the Stats script
        public Slider healthSlider; // Reference to the Slider UI element
        public GameObject playerObject;
        public Transform respawnLocation;
        public float currentHealth;
        public float maxHealth;
        private bool isPlayerDead = false;
        public Death death;
        public int healingItem = 5;
        public int healingLevel = 0;
        public TextMeshProUGUI itemText;

        private void Start()
        {
            if (stats != null)
            {
                // Initialize current health and max health from Stats
                currentHealth = stats.Health.CurrentValue;
                maxHealth = stats.Health.MaxValue;
            }
        }

        private void Update()
        {
            if (stats != null && healthSlider != null)
            {
                currentHealth = stats.Health.CurrentValue;
                maxHealth = stats.Health.MaxValue;

                // Update the slider value to reflect the current health percentage
                healthSlider.value = currentHealth / maxHealth;
            }

            if (currentHealth <= 0 && !isPlayerDead)
            {
                isPlayerDead = true; // Set the flag to true

                // Use Invoke to delay the respawn for 3 seconds
                Invoke("RespawnPlayer", 3f);

                healingItem = 5;
            }

            if (currentHealth > 0)
            {
                isPlayerDead = false; // Reset the flag when the player's health is not zero or less
            }
        }

        private void RespawnPlayer()
        {
            // Move the player to the respawn location
            playerObject.transform.position = respawnLocation.position;

            // Set the player's health to the maximum value
            stats.Health.CurrentValue = maxHealth;

            // Set the player GameObject to active
            playerObject.SetActive(true);
        }
    }
}
