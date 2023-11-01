using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using Bardent.Weapons;
using static Bardent.Utilities.CombatDamageUtilities;

public class TranceBar : MonoBehaviour
{
    public Slider timerSlider;
    public TextMeshProUGUI timingText;
    public PlayerInputHandler playerInputHandler; // Reference to the PlayerInputHandler script

    public float timer = 2f;
    private bool timerRunning = false;
    public int resetsRemaining = 1; // Number of resets allowed
    private float perfectRangeMin = 0.5f;
    private float perfectRangeMax = 0.6f;

    private float secondPerfectRangeMin = 0.7f;
    private float secondPerfectRangeMax = 0.8f;

    private float greatRangeMin = 0.2f;
    private float greatRangeMax = 0.39f;

    private float secondGreatRangeMin = 0.61f;
    private float secondGreatRangeMax = 0.8f;

    private float thirdGreatRangeMin = 0.61f;
    private float thirdGreatRangeMax = 0.8f;

    public int tranceLevel = 0;
    private int comboCount = 0;
    private int perfectCount = 0;
    private float comboTimer = 0f;
    private float resetRegen = 5;
    private bool regenTimerRunning = true;

    public float missShield = 3f;
    public TextMeshProUGUI comboText;
    public TextMeshProUGUI tranceText;
    public TextMeshProUGUI timerText;

    private const float SpeedIncreaseLevel1 = 0.01f;  // 20% speed increase
    private const float SpeedIncreaseLevel2 = 0.01f;  // 35% speed increase
    private const float SpeedIncreaseLevel3 = 0.02f;  // 45% speed increase

    
    public int beatCount = 0;


    public GameObject handle;

    public Weapon weapon;
    public TextMeshProUGUI currentAttackText;
    public int currentAttackCounter;
    public GameObject prefabWhenCounterIs1OrLess;
    public GameObject prefabWhenCounterIs2OrMore;
    public GameObject great1CounterIs1OrLess;
    public GameObject great2CounterIs1OrLess;
    public GameObject greatWhenCounterIs2OrMore;

    public AudioClip perfectSound;
    public AudioClip greatSound;
    public GameObject audioSourceContainer;

    public PlayerData playerData;


    private void Start()
    {
        //timingText = GetComponent<TextMeshProUGUI>();

        if (weapon != null)
        {
            weapon.OnUseInput += HandleWeaponUseInput;
        }


        UpdatePrefabsVisibility();


        if (playerInputHandler == null)
        {
            Debug.LogError("PlayerInputHandler is not assigned to the TranceBar script!");
        }
        else
        {
            playerInputHandler.OnPrimaryAttackInputAction += HandlePrimaryAttack;
            playerInputHandler.OnSecondaryAttackInputAction += HandleSecondaryAttack;
        }
    }

    private void OnDestroy()
    {
        if (playerInputHandler != null)
        {
            playerInputHandler.OnPrimaryAttackInputAction -= HandlePrimaryAttack;
            playerInputHandler.OnSecondaryAttackInputAction += HandleSecondaryAttack;
        }
    }

    private void Update()
    {

        float newPerfectRangeMin = 0.5f;
        float newPerfectRangeMax = 0.6f;
        float newGreatRangeMin = 0.3f;
        float newGreatRangeMax = 0.49f;
        float newSecondGreatRangeMin = 0.61f;
        float newSecondGreatRangeMax = 0.8f;


        if (weapon != null && beatCount == 2 )
        {
            newPerfectRangeMin = 0.7f;
            newPerfectRangeMax = 0.8f;
            newGreatRangeMin = 0.5f;
            newGreatRangeMax = 0.69f;
            newSecondGreatRangeMin = 0.48f;
            newSecondGreatRangeMax = 0.49f;
        }

        perfectRangeMin = newPerfectRangeMin;
        perfectRangeMax = newPerfectRangeMax;
        greatRangeMin = newGreatRangeMin;
        greatRangeMax = newGreatRangeMax;
        secondGreatRangeMin = newSecondGreatRangeMin;
        secondGreatRangeMax = newSecondGreatRangeMax;


        //

        timerText.text = "Timer: " + timer.ToString("F1") + "s";

        float timerReductionPercentage = 0.0f;


        if (regenTimerRunning)
        {
            // Update the reset regen timer
            resetRegen -= Time.deltaTime;
            if (resetRegen <= 0)
            {
                resetsRemaining++;  // Increase resetRemaining when the timer reaches 0
                resetRegen = 5.0f;  // Reset the timer
            }
        }

        if (tranceLevel == 0)
        {
            playerData.movementVelocity = 10f ;
        }

        if (tranceLevel == 1)
        {
            timerReductionPercentage = 0.01f; // 20% reduction
            playerData.movementVelocity = 10f + 1f;
            //resetsRemaining =+ 2;
        }
        else if (tranceLevel == 2)
        {
            timerReductionPercentage = 0.03f; // 30% reduction
            playerData.movementVelocity = 10f + 2f;
            //  resetsRemaining = 3;
        }
        else if (tranceLevel == 3)
        {
            timerReductionPercentage = 0.06f; // 40% reduction
            playerData.movementVelocity = 10f + 5f;
            // resetsRemaining = 5;
        }

        float originalTimer = 2f;
        float newTimer = originalTimer - (originalTimer * timerReductionPercentage);

        if (!timerRunning)
        {
            // Enable the regen timer when the timer is not running
            regenTimerRunning = true;
        }
        else
        {
            // Disable the regen timer when the timer is running
            regenTimerRunning = false;
        }

        if (timerRunning)
        {

           

            timer -= Time.deltaTime * (originalTimer / newTimer); // Decrease the timer based on the reduction percenta



            if (beatCount == 2)
            {
                perfectRangeMin = 0.6f;
                perfectRangeMax = 0.9f;
            }


            timerSlider.value = timer / originalTimer; // Update the slider
            handle.SetActive(true);

            if (timer <= 0f)
        {
            if (resetsRemaining > 0)
            {
                ResetTimerAndSlider();
                resetsRemaining--;
                perfectCount--;
            }
            else if (resetsRemaining == 0 && tranceLevel == 1)
            {
                ResetTimerAndSlider();
                tranceLevel--;
               
                perfectCount = 0;
                    resetsRemaining = 2;
                  
                }

                else if (resetsRemaining == 0 && tranceLevel == 2)
                {
                    ResetTimerAndSlider();
                    tranceLevel--;

                    perfectCount = 0;
                    resetsRemaining = 3;

                }

                else if (resetsRemaining == 0 && tranceLevel == 3)
                {
                    ResetTimerAndSlider();
                    tranceLevel--;

                    perfectCount = 0;
                    resetsRemaining = 4;

                }

               

                else
            {
                timer = 0f;
                timerRunning = false;
                    handle.SetActive(false);
                }
        }




            float timerPercentage = timer / 2f;
            if (timerPercentage >= perfectRangeMin && timerPercentage <= perfectRangeMax)
            {
                //timingText.text = "Perfect!";
            }
            else if (timerPercentage >= greatRangeMin && timerPercentage <= greatRangeMax)
            {
                // timingText.text = "Great!";
            }
            else if (timerPercentage >= secondGreatRangeMin && timerPercentage <= secondGreatRangeMax)
            {
                // timingText.text = "Great (Second Range)!";
            }
            else
            {
                //timingText.text = "Miss!";
            }
        }


        if (perfectCount >= 5)
        {

            perfectCount = 0;
            tranceLevel++;
            missShield = 3;
            resetsRemaining++;

            if (resetsRemaining > 5) { resetsRemaining = 5; }
            



        }

        if (missShield <= 0)

        {
            comboCount = 0;
            tranceLevel--;
            missShield = 3;
            comboText.text = "Combo: " + comboCount;

        }


        if (comboCount > 0)
        {
            comboTimer -= Time.deltaTime;
            comboText.text = "Combo: " + comboCount;

            if (comboTimer <= 0f)
            {
                comboCount = 0;
                //resetsRemaining = 0;
                comboText.text = "Combo: " + comboCount;


            }
        }



        if (tranceLevel < 0)
        {
            tranceLevel = 0;

        }


        if (tranceLevel > 3f)
        { tranceLevel = 3; }





        if (resetsRemaining == 0)
        {
            resetsRemaining = 0;

            // resetsRemaining = 0;
            // Reduce tranceLevel by 1, but don't let it go below 0
            // tranceLevel = Mathf.Max(0, tranceLevel - 1);
        }

        tranceText.text = "Trance Level: " + tranceLevel;
    }

    private void HandlePrimaryAttack()
    {


        if (weapon != null)
        {


            beatCount++; // Increment beatCount on each attack input

            // Reset beatCount to 0 when it reaches more than 3
            if (beatCount > 2)
            {
                beatCount = 0;
            }

            int currentAttackCounter = weapon.CurrentAttackCounter;

            // Update the TextMeshProUGUI component with the currentAttackCounter value
            currentAttackText.text = "Current Attack Counter: " + currentAttackCounter;

            // Update the prefab visibility based on the currentAttackCounter
            UpdatePrefabsVisibility();
        }




        if (!timerRunning)
        {
            timer = 2f;
            timerRunning = true;
            timerSlider.value = 1f;

            regenTimerRunning = false;
        }

        else
        {
            float timerPercentage = timer / 2f;
            if (timerPercentage >= perfectRangeMin && timerPercentage <= perfectRangeMax)
            {
                timingText.text = "PERFECT!";
               // SetTextVertexColor(timingText, Color.red);
                ResetTimerAndSlider();
                comboCount++; // Increment comboCount on a perfect hit
                perfectCount+=2;
                missShield += 1;
                comboTimer = 5f; // Set comboTimer to 5 seconds on a perfect hit
               // ResetTimerAndSlider();
                

                if (perfectSound != null)
                {
                    PlayAudioClip(perfectSound);
                }

            }
            else if (timerPercentage >= greatRangeMin && timerPercentage <= greatRangeMax)
            {
                timingText.text = "GREAT!";
               // SetTextVertexColor(timingText, new Color(1.0f, 0.5f, 0.0f));
                comboTimer = 5f;
                comboCount++; // Increment comboCount on a perfect hit
                perfectCount ++ ;
                ResetTimerAndSlider();

                if (greatSound != null)
                {
                    PlayAudioClip(greatSound);
                }

            }
            else if (timerPercentage >= secondGreatRangeMin && timerPercentage <= secondGreatRangeMax)
            {
               // timingText.text = "Great!";
                timingText.text = "GREAT (Second Range)!";
               // SetTextVertexColor(timingText, new Color(1.0f, 0.5f, 0.0f));
                comboCount++; // Increment comboCount on a perfect hit
                perfectCount ++ ;
                comboTimer = 5f;


                ResetTimerAndSlider();


                if (greatSound != null)
                {
                    PlayAudioClip(greatSound);
                }

            }
            else
            {
                timingText.text = "MISS!";
                //SetTextVertexColor(timingText, Color.blue);
                ResetTimerAndSlider();
                missShield--;
                perfectCount = 0;


                resetRegen = 5.0f;
            }




        }
    }
    private void HandleSecondaryAttack()
    {
        if (weapon != null)
        {


            beatCount++; // Increment beatCount on each attack input

            // Reset beatCount to 0 when it reaches more than 3
            if (beatCount > 2)
            {
                beatCount = 0;
            }

            int currentAttackCounter = weapon.CurrentAttackCounter;

            // Update the TextMeshProUGUI component with the currentAttackCounter value
            currentAttackText.text = "Current Attack Counter: " + currentAttackCounter;

            // Update the prefab visibility based on the currentAttackCounter
            UpdatePrefabsVisibility();
        }




        if (!timerRunning)
        {
            timer = 2f;
            timerRunning = true;
            timerSlider.value = 1f;

            regenTimerRunning = false;
        }

        else
        {
            float timerPercentage = timer / 2f;
            if (timerPercentage >= perfectRangeMin && timerPercentage <= perfectRangeMax)
            {
                timingText.text = "PERFECT!";
                // SetTextVertexColor(timingText, Color.red);
                ResetTimerAndSlider();
                comboCount++; // Increment comboCount on a perfect hit
                perfectCount += 2;
                missShield += 1;
                comboTimer = 5f; // Set comboTimer to 5 seconds on a perfect hit
                                 // ResetTimerAndSlider();


                if (perfectSound != null)
                {
                    PlayAudioClip(perfectSound);
                }

            }
            else if (timerPercentage >= greatRangeMin && timerPercentage <= greatRangeMax)
            {
                timingText.text = "GREAT!";
                // SetTextVertexColor(timingText, new Color(1.0f, 0.5f, 0.0f));
                comboTimer = 5f;
                comboCount++; // Increment comboCount on a perfect hit
                perfectCount++;
                ResetTimerAndSlider();

                if (greatSound != null)
                {
                    PlayAudioClip(greatSound);
                }

            }
            else if (timerPercentage >= secondGreatRangeMin && timerPercentage <= secondGreatRangeMax)
            {
                // timingText.text = "Great!";
                timingText.text = "GREAT (Second Range)!";
                // SetTextVertexColor(timingText, new Color(1.0f, 0.5f, 0.0f));
                comboCount++; // Increment comboCount on a perfect hit
                perfectCount++;
                comboTimer = 5f;


                ResetTimerAndSlider();


                if (greatSound != null)
                {
                    PlayAudioClip(greatSound);
                }

            }
            else
            {
                timingText.text = "MISS!";
                //SetTextVertexColor(timingText, Color.blue);
                ResetTimerAndSlider();
                missShield--;
                perfectCount = 0;


                resetRegen = 5.0f;
            }


        }
            // HandleAttackLogic();
        }

        private void ResetTimerAndSlider()
    {
        timer = 2f;
        timerSlider.value = 1f;
    }



    private void UpdatePrefabsVisibility()
    {
        if (weapon != null)
        {
            if (beatCount < 2)
            {
                if (prefabWhenCounterIs1OrLess != null)
                {
                    prefabWhenCounterIs1OrLess.SetActive(true);
                    great1CounterIs1OrLess.SetActive(true) ;
                    great2CounterIs1OrLess.SetActive(true);
                }

                if (prefabWhenCounterIs2OrMore != null)
                {
                    prefabWhenCounterIs2OrMore.SetActive(false);
                         greatWhenCounterIs2OrMore.SetActive(false);
                }
            }
           else if (beatCount == 2)
            {
                if (prefabWhenCounterIs1OrLess != null)
                {
                    prefabWhenCounterIs1OrLess.SetActive(false);
                    great1CounterIs1OrLess.SetActive(false);
                    great2CounterIs1OrLess.SetActive(false);
                }

                if (prefabWhenCounterIs2OrMore != null)
                {
                    prefabWhenCounterIs2OrMore.SetActive(true);
                    greatWhenCounterIs2OrMore.SetActive(true);
                }
            }
        }
    }


    private void PlayAudioClip(AudioClip clip)
    {
        if (clip != null)
        {
            GameObject audioPlayer = new GameObject("TempAudioPlayer");
            AudioSource audioSource = audioPlayer.AddComponent<AudioSource>();
            audioSource.clip = clip;
            audioSource.Play();

            // Destroy the temporary GameObject when the sound finishes
            Destroy(audioPlayer, clip.length);
        }
    }

    private void SetTextVertexColor(TextMeshProUGUI text, Color color)
    {
        Material textMaterial = text.fontSharedMaterial; // Get the material associated with the TextMeshProUGUI
        textMaterial.SetColor(ShaderUtilities.ID_FaceColor, color); // Modify the vertex color
    }


    private void HandleWeaponUseInput()
    {
        if (weapon != null)
        {
            int currentAttackCounter = weapon.CurrentAttackCounter;

            // Update the TextMeshProUGUI component with the currentAttackCounter value
            currentAttackText.text = "Current Attack Counter: " + currentAttackCounter;
        }
    }


}