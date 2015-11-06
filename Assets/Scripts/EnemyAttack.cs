using UnityEngine;
using System.Collections;

public class EnemyAttack : MonoBehaviour {

    public float timeBetweenAttacks = 0.5f;     // The time in seconds between each attack.
    public int attackDamage = 10;               // The amount of health taken away per attack.


    Animator anim;                              // Reference to the animator component.
    GameObject fance;                          // Reference to the fance GameObject.
    //PlayerHealth playerHealth;                  // Reference to the fance's health.
    //EnemyHealth enemyHealth;                    // Reference to this enemy's health.
    bool fanceInRange;                         // Whether player is within the trigger collider and can be attacked.
    float timer;                                // Timer for counting up to the next attack.


    void Awake()
    {
        // Setting up the references.
        fance = GameObject.FindGameObjectWithTag("Fance");
        //playerHealth = player.GetComponent<PlayerHealth>();
        //enemyHealth = GetComponent<EnemyHealth>();
        anim = GetComponent<Animator>();
    }


    void OnTriggerEnter(Collider other)
    {
        
        // If the entering collider is the player...
        if (other.gameObject == fance)
        {
            // ... the player is in range.
            fanceInRange = true;
        }
    }


    void OnTriggerExit(Collider other)
    {
        // If the exiting collider is the player...
        if (other.gameObject == fance)
        {
            //anim.SetBool("AttackingRange", false);
            // ... the player is no longer in range.
            fanceInRange = false;
        }
    }


    void Update()
    {
        // Add the time since Update was last called to the timer.
        timer += Time.deltaTime;

        // If the timer exceeds the time between attacks, the player is in range and this enemy is alive...
        if (timer >= timeBetweenAttacks && fanceInRange) //&& enemyHealth.currentHealth > 0)
        {
            // ... attack.
            Attack();
        }

        /*
        // If the player has zero or less health...
        if (playerHealth.currentHealth <= 0)
        {
            // ... tell the animator the player is dead.
            anim.SetTrigger("PlayerDead");
        }
        */
    }


    void Attack()
    {
        anim.SetBool("AttackingRange", true);
        // Reset the timer.
        timer = 0f;
        /*
        // If the player has health to lose...
        if (playerHealth.currentHealth > 0)
        {
            // ... damage the player.
            playerHealth.TakeDamage(attackDamage);
        }
        */
    }

}
