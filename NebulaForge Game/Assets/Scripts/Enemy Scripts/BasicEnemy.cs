using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BasicEnemy : MonoBehaviour
{

    [SerializeField]
    private float speed;
    [SerializeField]
    private Transform playerRef;
    [SerializeField]
    private float currHealth;
    [SerializeField]
    private float maxHealth;
    [SerializeField]
    private float damage;

    void BehaviourUpdate() {
        // Moves to player
        transform.position += (playerRef.position - transform.position).normalized * speed * Time.deltaTime;

    }

    // Start is called before the first frame update
    void Start()
    {
        currHealth = maxHealth;
        playerRef = PlayerStats.instance.gameObject.transform;
    }

    // Update is called once per frame
    void Update()
    {
        BehaviourUpdate();
    }

    void OnTriggerEnter(Collider o) {
        if (o.gameObject.tag == "PlayerProjectile") {
            PlayerProjectile p = o.GetComponent<PlayerProjectile>();
            currHealth -= p.GetDamage();

            if (currHealth <= 0) {
                Die();
            }

            p.Reset();
        }
    }
    void OnCollisionStay(Collision col) {
        Debug.Log("Touched " + col.gameObject.name);
        if (col.gameObject.tag == "Player") {
            PlayerStats.instance.LoseHealth(damage);
        }
    }

    void Die() {
        gameObject.SetActive(false);
        PlayerStats.instance.GainExp(1);
    }
}
