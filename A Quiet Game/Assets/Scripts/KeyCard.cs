using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KeyCard : MonoBehaviour
{
    [SerializeField] float lookRadius = 10f;
    PlayerMovement player;
    Transform target;
    AudioSource itemSound;
    [SerializeField] AudioClip pickupCard;

    // Start is called before the first frame update
    void Start()
    {
        target =  PlayerManager.instance.player.transform;
        player = PlayerManager.instance.player.GetComponent<PlayerMovement>();
        itemSound = GetComponent<AudioSource>();
    }

    // Update is called once per frame
    void Update()
    {
         float distance = Vector3.Distance(target.position, transform.position);
         if(distance <= lookRadius && !player.hasKey) {
            player.hasKey = true;
            itemSound.Pause();
            itemSound.clip = pickupCard;
            itemSound.Play();
         }

         if(!itemSound.isPlaying && !player.hasKey){
                itemSound.Play();
        } 

    }

    void OnDrawGizmosSelected(){
        Gizmos.color = Color.magenta;
        Gizmos.DrawWireSphere(transform.position, lookRadius);
    }
}
