using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    [SerializeField] int health;
    [SerializeField] int maxHealth;

    [SerializeField] int damageToDeal;

    [SerializeField] AudioClip firstInjury;
    [SerializeField] AudioClip secondInjury;
    [SerializeField] AudioClip death;
    AudioSource vocalization;
    
    // Start is called before the first frame update
    void Start()
    {
        vocalization = GetComponent<AudioSource>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void TakeDamage(int damage){
            Debug.Log("Enemy took damage");
            health -= damage;
            if (health <= 0) {
                vocalization.PlayOneShot(death, 1.0f);
               Invoke("Die", death.samples / death.frequency);
            } else if (health/maxHealth <= .5){
                vocalization.PlayOneShot(secondInjury, .75f);
            } else {
                vocalization.PlayOneShot(firstInjury, .75f);
            }
    }

    void Die(){
         Destroy(gameObject);
    }

    void OnTriggerEnter(Collider other){
         if(other.CompareTag("Player") ){
            other.GetComponent<Player>().TakeDamage(damageToDeal); 
         }
    }
}
