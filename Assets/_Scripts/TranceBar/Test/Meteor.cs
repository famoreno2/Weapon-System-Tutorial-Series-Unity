using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

namespace Bardent
{
    public class Meteor : MonoBehaviour
    {
        public string playerTag = "Player";  // Set the player tag in the Inspector
        private HealthBar healthBar; // Reference to the HealthBar script

        private void Start()
        {
            // Try to find the HealthBar component in the scene at runtime
            healthBar = GameObject.Find("CanvasTrance/HealthBar").GetComponent<HealthBar>();
            if (healthBar == null)
            {
                Debug.LogError("HealthBar component not found. Make sure the path is correct.");
            }
        }

        private void OnTriggerEnter2D(Collider2D other)
        {
            if (other.CompareTag(playerTag))
            {
                if (healthBar != null)
                {
                    healthBar.currentHealth -= 10;
                }
                else
                {
                    Debug.LogError("HealthBar reference is missing. Make sure to set it in the Inspector or check the path.");
                }
            }

            // Find the Tilemap collider by its path
            TilemapCollider2D tilemapCollider = GameObject.Find("Grid/Platforms").GetComponent<TilemapCollider2D>();

            if (tilemapCollider != null && tilemapCollider.OverlapPoint(transform.position))
            {
                Destroy(gameObject); // Destroy the Meteor object
            }
        }
    }
}
