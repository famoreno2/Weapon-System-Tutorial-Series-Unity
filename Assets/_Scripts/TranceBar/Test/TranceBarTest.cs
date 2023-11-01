using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine.UI;


public class TranceBarTest : MonoBehaviour
{
    public Transform spawnLocation; // The position where the prefab should initially spawn.
    public Transform targetLocation; // The position where the prefab should move to.

    public GameObject prefabToSpawn; // The 2D sprite prefab to spawn.
    public float moveSpeed = 2.0f; // Increase the move speed.
    public float spawnInterval = 1.0f; // Set the spawn interval to 1 second.

    private List<GameObject> spawnedPrefabs = new List<GameObject>();
    private float timeSinceLastSpawn = 0.0f;

    public BoxCollider2D perfect;
    public BoxCollider2D miss;

    public int comboCount;
    public float comboTimer = 5f;
    public int tranceLevel = 0;
    public float missShield = 2;
    public TextMeshProUGUI comboText;
    public TextMeshProUGUI tranceLevelText;

    private float timer = 0.0f;
    private bool timerRunning = false;
    public Slider timerSlider;
    public TextMeshProUGUI TimingText;

    public PlayerInputHandler playerInputHandler;

    public Image timerFillImage;

    private void Start()
    {
        SpawnPrefab();
        Debug.Log("hi ");
        StartCoroutine(ComboTimerCountdown());

        playerInputHandler = FindObjectOfType<PlayerInputHandler>();

        if (playerInputHandler != null)
        {
            // Subscribe to the PlayerInputHandler's events
            playerInputHandler.OnPrimaryAttackInputAction += HandlePrimaryAttackInput;
        }
        else
        {
            Debug.LogError("PlayerInputHandler not found in the scene.");
        }
    }

    private void Update()
    {
        comboText.text = "Combo " + comboCount.ToString();




        if (comboCount >= 5)
        {
            tranceLevel = 1;
            Debug.Log("trancelevel 2 ");

            if (tranceLevel == 1)
            {
                moveSpeed = 2f;
                spawnInterval = 0.7f;
                tranceLevelText.text = "Trance Level " + tranceLevel.ToString();
            }

        }

        if (comboCount >= 10)
        {
            tranceLevel = 2;
            Debug.Log("trancelevel 2 ");
            if (tranceLevel == 2)
            {
                moveSpeed = 2.0f;
                spawnInterval = 0.5f;
                tranceLevelText.text = "Trance Level " + tranceLevel.ToString();
            }
        }

        if (comboCount >= 15)
        {
            tranceLevel = 3;
            Debug.Log("trancelevel 3 ");
            if (tranceLevel == 3)
            {
                moveSpeed = 3.0f;
                spawnInterval = 0.4f;
                tranceLevelText.text = "Trance Level " + tranceLevel.ToString();
            }

        }


        if (tranceLevel < 0)
        {
            tranceLevel = 0;
        }


        // Check if it's time to spawn a new prefab.
        timeSinceLastSpawn += Time.deltaTime;
        if (timeSinceLastSpawn >= spawnInterval)
        {
            SpawnPrefab();
            timeSinceLastSpawn = 0.0f;
        }

        if (comboCount <= 4)
        {
            //playerData.movementVelocity = 10;

            moveSpeed = 2.0f;
            spawnInterval = 1f;
        }

        // Iterate through all spawned prefabs and move them towards the target.
        for (int i = spawnedPrefabs.Count - 1; i >= 0; i--)
        {
            GameObject prefab = spawnedPrefabs[i];
            if (!IsPrefabAtTarget(prefab.transform))
            {
                MovePrefabTowardsTarget(prefab.transform);
            }
            else
            {
                // Destroy the prefab if it has reached its destination.
                Destroy(prefab);
                spawnedPrefabs.RemoveAt(i);
            }
        }

        if (timerRunning)
        {
            timer -= Time.deltaTime;

            float timeRemaining = timer % 1.0f;

            if ((timeRemaining >= 0.9f && timeRemaining <= 1.1f))
            {
               // TimingText.text = "Perfect!";
                timerFillImage.color = Color.red;
            }
            else if ((timeRemaining >= 0.6f && timeRemaining <= 0.89f))
            {
               // TimingText.text = "Great!";
                timerFillImage.color = new Color(1.0f, 0.5f, 0.0f); // Orange color
            }
            else if ((timeRemaining >= 0.29f && timeRemaining <= 0.6f) || (timeRemaining >= 1.11f && timeRemaining <= 1.3f))
            {
               // TimingText.text = "Miss!";
                timerFillImage.color = Color.blue;
            }
            timerSlider.value = timer / 2.0f; // Assuming a 2-second timer
        }

        if (timer <= 0.0f)
        {
           // HandleMiss();
        }

    }

    private void SpawnPrefab()
    {
        // Instantiate the prefab at the spawn location.
        GameObject spawnedPrefab = Instantiate(prefabToSpawn, spawnLocation.position, Quaternion.identity);

        // Set the parent of the spawned prefab to the object with the script.
        spawnedPrefab.transform.parent = transform;

        spawnedPrefabs.Add(spawnedPrefab);
    }

    private bool IsPrefabAtTarget(Transform prefabTransform)
    {
        // Check if the prefab has reached or passed the target horizontally.
        return Mathf.Abs(targetLocation.position.x - prefabTransform.position.x) <= 0.1f;
    }

    private void MovePrefabTowardsTarget(Transform prefabTransform)
    {
        // Calculate the movement direction.
        Vector3 moveDirection = (targetLocation.position - prefabTransform.position).normalized;
        moveDirection.y = 0; // Make sure there's no vertical movement.

        // Move the prefab towards the target.
        prefabTransform.Translate(moveDirection * moveSpeed * Time.deltaTime);
    }

    IEnumerator ComboTimerCountdown()
    {


        while (comboTimer > 0)
        {
            yield return new WaitForSeconds(1);
            comboTimer--;




            if (comboTimer == 0 && comboCount > 0)
            {
                // Reset the combo count and reduce the trance level to 1 when the timer reaches 0.
                comboCount = 0;
                tranceLevel--;
                comboTimer = 5f; // Reset the combo timer to 5 seconds.
                tranceLevelText.text = "Trance Level " + tranceLevel.ToString();

            }

            if (comboTimer == 0 && tranceLevel > 0)
            {
                // Reset the combo count and reduce the trance level to 1 when the timer reaches 0.
                //comboCount = 0;
                tranceLevel--;
                comboTimer = 5f; // Reset the combo timer to 5 seconds.

                tranceLevelText.text = "Trance Level " + tranceLevel.ToString();
            }
        }
    }

    private void StartTimer()
    {
        timer = 2.0f; // 2-second timer
        timerRunning = true;
    }

    private void HandlePerfect()
    {
        TimingText.text = "Perfect!";
        Debug.Log("Perfect");
        comboCount = 0; // Reset combo count to 0 on Perfect
        comboText.text = "Combo " + comboCount.ToString() + " - Perfect!";
        timerRunning = false;
        timerSlider.value = 0.0f;
        comboTimer = 5f; // Reset the combo timer to 5 seconds.
        StartTimer();
    }

    private void HandleGreat()
    {
        TimingText.text = "Great!";
        Debug.Log("Great");
        comboCount = 0; // Reset combo count to 0 on Great
        comboText.text = "Combo " + comboCount.ToString() + " - Great!";
        timerRunning = false;
        timerSlider.value = 0.0f;
        comboTimer = 5f; // Reset the combo timer to 5 seconds.
        StartTimer();
    }

    private void HandleMiss()
    {
        TimingText.text = "Miss!";
        Debug.Log("Miss");

        if (timerRunning)
        {
            float timeRemaining = timer % 1.0f;

            if ((timeRemaining >= 0.29f && timeRemaining <= 0.6f) || (timeRemaining >= 1.11f && timeRemaining <= 1.3f))
            {
                // This is a miss within the miss timing
                comboCount++;
                comboText.text = "Combo " + comboCount.ToString() + " - Miss!";
            }
        }

        timerRunning = false;
        timerSlider.value = 0.0f;

        // Decrease missShield and check if it reaches 0
        missShield--;

        if (missShield <= 0)
        {
            missShield = 0; // Ensure it doesn't go negative
            tranceLevel--;

            // Ensure tranceLevel doesn't go below 0
            if (tranceLevel < 0)
            {
                tranceLevel = 0;
            }

            comboTimer = 5f; // Reset the combo timer to 5 seconds.
            tranceLevelText.text = "Trance Level " + tranceLevel.ToString();
        }
    }

    private void HandlePrimaryAttackInput()
    {
        if (timerRunning)
        {
            float timeRemaining = timer % 1.0f;

            if ((timeRemaining >= 0.9f && timeRemaining <= 1.1f) || (timeRemaining >= 0.6f && timeRemaining <= 0.89f))
            {
                HandleGreat();
            }
            else if ((timeRemaining >= 0.29f && timeRemaining <= 0.6f) || (timeRemaining >= 1.11f && timeRemaining <= 1.3f))
            {
                HandleMiss();
            }
            else
            {
                HandlePerfect();
            }

            comboCount++;
        }

        StartTimer();
    }


}