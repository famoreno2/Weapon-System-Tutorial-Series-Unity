using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace Bardent
{
    public class ResetCounterManager : MonoBehaviour
    {
        public GameObject resetCounter1;
        public GameObject resetCounter2;
        public GameObject resetCounter3;
        public GameObject resetCounter4;
        public GameObject resetCounter5;
        public TranceBar tranceBar;


        public Color blueColor;
        public Color yellowColor;
        public Color orangeColor;
        public Color redColor;

        // Start is called before the first frame update
        void Start()
        {
        
        }

        // Update is called once per frame
        void Update()
        {

            if (tranceBar != null)
            {
                int tranceLevel = tranceBar.tranceLevel;
                int resetsRemaining = tranceBar.resetsRemaining;

                // Set color of Image components based on tranceLevel
                Color counterColor = GetCounterColor(tranceLevel);

                // Activate or deactivate counters based on resetsRemaining and set color
                ActivateCounter(resetCounter1, 1, resetsRemaining, counterColor);
                ActivateCounter(resetCounter2, 2, resetsRemaining, counterColor);
                ActivateCounter(resetCounter3, 3, resetsRemaining, counterColor);
                ActivateCounter(resetCounter4, 4, resetsRemaining, counterColor);
                ActivateCounter(resetCounter5, 5, resetsRemaining, counterColor);
            }

        }

        private void SetCounterColor(GameObject counter, Color color)
        {
            Image image = counter.GetComponent<Image>();
            if (image != null)
            {
                image.color = color;
            }
        }

        // Helper method to activate or deactivate counter based on resetsRemaining and set color
        private void ActivateCounter(GameObject counter, int counterNumber, int resetsRemaining, Color color)
        {
            SetCounterColor(counter, color);
            counter.SetActive(counterNumber <= resetsRemaining);
        }

        // Helper method to get color based on tranceLevel
        private Color GetCounterColor(int tranceLevel)
        {
            switch (tranceLevel)
            {
                case 0:
                    return Color.blue;
                case 1:
                    return Color.yellow;
                case 2:
                    return new Color(1.0f, 0.5f, 0.0f); // Orange
                case 3:
                    return Color.red;
                default:
                    return Color.white; // Default to white or some other color
            }
        }

    }
}
