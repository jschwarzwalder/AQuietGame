using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LevelSelect : MonoBehaviour
{
    [SerializeField] float range = 10f;
    [SerializeField] AudioClip doorOpen;

    [SerializeField] AudioClip hasCard;
    [SerializeField] AudioClip doesNotHaveCard;

    Transform target;
    AudioSource loadingNextLevelEnglish;

    bool exitingLevel = false;
    PlayerMovement player;

    // Start is called before the first frame update
    void Start()
    {
        target   = PlayerManager.instance.player.transform;
        loadingNextLevelEnglish = GetComponent<AudioSource>();
        player = PlayerManager.instance.player.GetComponent<PlayerMovement>();

    }

    // Update is called once per frame
    void Update()
    {
         float distance = Vector3.Distance(target.position, transform.position);

        if(distance <= range){
            if (!player.hasKey && distance <= range/2){
                if (!loadingNextLevelEnglish.isPlaying) {
                    loadingNextLevelEnglish.clip = doesNotHaveCard;
                    loadingNextLevelEnglish.Play();    
                }
            } else if(player.hasKey) {
                 if (loadingNextLevelEnglish.clip == hasCard) {
                    exitingLevel = true;
                } else if (!loadingNextLevelEnglish.isPlaying) {
                    loadingNextLevelEnglish.PlayOneShot(doorOpen, 0.5f);
                    loadingNextLevelEnglish.clip = hasCard;  
                    loadingNextLevelEnglish.Play();  
                }
            }
            
               
        if (!loadingNextLevelEnglish.isPlaying && exitingLevel){
                GameObject.Find("Game Manager").GetComponent<PlayerManager>().health = player.GetComponent<Player>().getHealth();
                GameObject.Find("Game Manager").GetComponent<PlayerManager>().weapons = player.GetComponent<Player>().getWeapons();
                SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
            }
        }
    }

    void OnDrawGizmosSelected(){
        Gizmos.color = Color.magenta;
        Gizmos.DrawWireSphere(transform.position, range);
    }
}
