using System.Collections;
using System.Collections.Generic;
using Bardent.Combat.Damage;
using UnityEngine;
using TMPro;

public class CombatTestDummy : MonoBehaviour, IDamageable
{
    [SerializeField] private GameObject hitParticles;

    private Animator anim;
    public TextMeshProUGUI totalDamage;
    public TextMeshProUGUI damageDealt;
    public float maxHealth = 100;
    public float currenthealth;
    public float health;
    public float damageTaken;

    private void Update()
    {
      

        currenthealth = health;
        totalDamage.text = currenthealth .ToString();

        if (health <=0)
        {
            health = 100;

        }
    }


    public void Damage(DamageData data)
    {
        Debug.Log(data.Amount + " Damage taken");

        damageTaken = data.Amount;
        Instantiate(hitParticles, transform.position, Quaternion.Euler(0.0f, 0.0f, Random.Range(0.0f, 360.0f)));
        anim.SetTrigger("damage");
        //Destroy(gameObject);
        health -= damageTaken;
        damageDealt.text = data.Amount + " Damage taken".ToString();

    }


    private void Awake()
    {
        anim = GetComponent<Animator>();
        health = maxHealth;
        currenthealth = health;
    }
}
