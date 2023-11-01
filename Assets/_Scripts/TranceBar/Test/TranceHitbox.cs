using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

namespace Bardent
{
    public class TranceHitbox : MonoBehaviour
    {
        public int comboCount;
        public float comboTimer = 5f;
        public int tranceLevel = 0;
        public int comboBreaker = 0;
        public TextMeshProUGUI comboText;
        public TextMeshProUGUI tranceLevelText;
        public float statRefresher;

      //  public PlayerData playerData ;

        // Start is called before the first frame update
        void Start()
        {
            Debug.Log("hi ");
            StartCoroutine(ComboTimerCountdown());

           // statRefresher = playerData.movementVelocity;
        }

        // Update is called once per frame
        void Update()
        {
            comboText.text = "Combo " + comboCount.ToString();
           if (comboBreaker >= 2) {
            comboCount = 0;
            }

            if (comboCount >= 15)
            {
                tranceLevel = 3;
                Debug.Log("trancelevel 3 ");
                tranceLevelText.text = "Trance Level " + tranceLevel.ToString();
            }
            else if (comboCount >= 10)
            {
                tranceLevel = 2;
                Debug.Log("trancelevel 2 ");
                tranceLevelText.text = "Trance Level " + tranceLevel.ToString();
            }
            else if (comboCount >= 5)
            {
                tranceLevel = 1;
                tranceLevelText.text = "Trance Level " + tranceLevel.ToString();
            }
            else if (tranceLevel > 1)
            {
               
               // playerData.movementVelocity = 20;
            }
            else if (tranceLevel == 1)
            {
                 //playerData.movementVelocity = 10;

            }
        }

        private void OnTriggerEnter2D(Collider2D other)
        {
            // Check if the entering collider's GameObject has the "TranceBeat" tag.
            if (other.CompareTag("TranceBeat"))
            {
                // Log a message when a game object with the "TranceBeat" tag enters this collider.
                Debug.Log("Perfect");
                comboCount++;

                // Reset the combo timer when a collision is detected.
                comboTimer = 5f;
            }
        }

        IEnumerator ComboTimerCountdown()
        {
            while (comboTimer > 0)
            {
                yield return new WaitForSeconds(1);
                comboTimer--;

                if (comboTimer == 0)
                {
                    // Reset the combo count and reduce the trance level to 1 when the timer reaches 0.
                    comboCount = 0;
                    tranceLevel = 1;
                    comboTimer = 5f; // Reset the combo timer to 5 seconds.
                }
            }
        }
    }
}
