using Bardent;
using System.Collections;
using UnityEngine;

public class BeatCheck : MonoBehaviour
{
    private TranceHitbox tranceHitbox;
    public BoxCollider2D beathitbox;
   //public BoxCollider2D enablerHitbox;
    public bool isActive;
    public bool missed;

    private void Start()
    {
        isActive = false;
    }

    private void Update()
    {
        if (isActive == true)
        {
            beathitbox.enabled = true;
        }
    }

    private void OnTriggerEnter(Collider other)
    {
       

        // Check if the trigger object has a "PerfectHitbox" tag
        if (other.CompareTag("PerfectHitbox"))
        {
            Debug.Log("Collided with a PerfectHitbox");
            // tranceHitbox.comboCount++;
            beathitbox.enabled = false;
            isActive = false;
        }
        else if (other.CompareTag("MissHitBox"))
        {
            Debug.Log("Collided with a MissHitbox");
            // tranceHitbox.comboCount++;
            beathitbox.enabled = false;
            missed = true;
        }
    }
}
