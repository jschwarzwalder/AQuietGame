using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    [SerializeField] int health;
    [SerializeField] int maxHealth;

    [SerializeField] AudioClip firstInjury;
    [SerializeField] AudioClip secondInjury;
    [SerializeField] AudioClip death;
    [SerializeField] AudioClip fullHealth;
    AudioSource vocalization;

    public ArrayList<Weapon> weapons;
    
    // Start is called before the first frame update
    void Start()
    {
        vocalization = GetComponent<AudioSource>();
        health = GameObject.Find("Game Manager").GetComponent<PlayerManager>().health;
        if (health <= 0){
            health = maxHealth;
        }
        GameObject.Find("Game Manager").GetComponent<PlayerManager>().health = 0;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public int getHealth(){
        return health;
    }

    public ArrayList<String> getWeapons(){
        ArrayList<string> weaponList;
        foreach (Weapon weapon in weapons){
            weaponList.Add(weapon.getName());
        }
        return weaponList;
    }

    public void TakeDamage(int damage){
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

    public void Heal(int healingAmount){
        health += healingAmount;
        if (health > maxHealth){
            health = maxHealth;
             vocalization.PlayOneShot(fullHealth, .75f);
        }
    }

    void Die(){
        
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }

    public void PickupWeapon(Weapon weapon){
        weapons.Add(weapon);

    }

   
}
