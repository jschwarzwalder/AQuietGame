using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
public class EnemyController : MonoBehaviour
{
    [SerializeField] float lookRadius = 10f;
    [SerializeField] AudioClip detectPlayer;

    Transform target;
    NavMeshAgent agent;
    AudioSource vocalization;
    

    
    // Start is called before the first frame update
    void Start()
    {
        agent = GetComponent<NavMeshAgent>();
        target = PlayerManager.instance.player.transform;
        vocalization = GetComponent<AudioSource>();
    }

    // Update is called once per frame
    void Update()
    {
        float distance = Vector3.Distance(target.position, transform.position);

        if(distance <= lookRadius && target.GetComponent<PlayerMovement>().soundEmitted > 0){
            agent.SetDestination(target.position);
            vocalization.clip = detectPlayer;

            if(!vocalization.isPlaying){
                vocalization.Play();
            }

        }
    }

    void OnDrawGizmosSelected(){
        Gizmos.color = Color.magenta;
        Gizmos.DrawWireSphere(transform.position, lookRadius);
    }
}
