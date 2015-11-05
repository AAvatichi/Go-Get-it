using UnityEngine;
using System.Collections;

public class EnemyHealth : MonoBehaviour {
    public Transform blood;
    public int startingHealth = 100;            // The amount of health the enemy starts the game with.
    public int currentHealth;                   // The current health the enemy has.
    public float sinkSpeed = 0.5f;              // The speed at which the enemy sinks through the floor when dead.
    public int scoreValue = 10;                 // The amount added to the player's score when the enemy dies.
    public AudioClip deathClip;                 // The sound to play when the enemy dies.
    public float dyingDueation = 0.4f;                    // How long to wait until start sinking

    float timer = 0;
    Animator anim;                              // Reference to the animator.
    AudioSource enemyAudio;                     // Reference to the audio source.
    CapsuleCollider capsuleCollider;            // Reference to the capsule collider.
    bool isDead;                                // Whether the enemy is dead.


    void Awake()
    {
        // Setting up the references.
        anim = GetComponent<Animator>();
        enemyAudio = GetComponent<AudioSource>();
        capsuleCollider = GetComponent<CapsuleCollider>();

        // Setting the current health when the enemy first spawns.
        currentHealth = startingHealth;
    }

    void Update () {
        if(isDead)
        {
            //start sinking only afte few seconds
            timer += Time.deltaTime;
            // If the enemy should be sinking...
            if (timer >= dyingDueation)
            {
                // ... move the enemy down by the sinkSpeed per second.
                transform.Translate(-Vector3.up * sinkSpeed * Time.deltaTime);
            }

        }

    }

    public void TakeDamage(int amount, Vector3 hitPoint)
    {
        
        Transform blood2 = Instantiate(blood.transform);
        if (blood2 != null)
        {
            blood2.transform.position = hitPoint;
            blood2.transform.rotation = transform.rotation;
            Destroy((blood2 as Transform).gameObject, 0.5f);
        }

        // If the enemy is dead...
        if (isDead)
            // ... no need to take damage so exit the function.
            return;

        // Play the hurt sound effect.
        if(enemyAudio != null)
            enemyAudio.Play();

        currentHealth -= amount;

        if (currentHealth <= 0)
        {
            // ... the enemy is dead.
            Death();
        }
    }

    void Death()
    {
        // The enemy is dead.
        isDead = true;

        // Turn the collider into a trigger so shots can pass through it.
        capsuleCollider.isTrigger = true;
        //capsuleCollider.enabled = false;
        // Tell the animator that the enemy is dead.
        anim.SetTrigger("Dead");

        // Change the audio clip of the audio source to the death clip and play it (this will stop the hurt clip playing).
        if (enemyAudio != null)
        {
            enemyAudio.clip = deathClip;
            enemyAudio.Play();
        }

        StartSinking();
    }


    public void StartSinking()
    {
        // Find and disable the Nav Mesh Agent.
        GetComponent<NavMeshAgent>().enabled = false;

        // Find the rigidbody component and make it kinematic (since we use Translate to sink the enemy).
        GetComponent<Rigidbody>().isKinematic = true;


        // Increase the score by the enemy's score value.
        //ScoreManager.score += scoreValue;

        // After 2 seconds destory the enemy.
        Destroy(gameObject, 2f + dyingDueation);
    }

    public bool IsDead()
    {
        return isDead;
    }
}
