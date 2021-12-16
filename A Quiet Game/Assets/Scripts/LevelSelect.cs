using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LevelSelect : MonoBehaviour
{
    [SerializeField] float range = 2f;

    Transform target;
    AudioSource loadingNextLevelEnglish;

    // Start is called before the first frame update
    void Start()
    {
        target   = PlayerManager.instance.player.transform;
        loadingNextLevelEnglish = GetComponent<AudioSource>();
    }

    // Update is called once per frame
    void Update()
    {
         float distance = Vector3.Distance(target.position, transform.position);

        if(distance <= range){
            if (!loadingNextLevelEnglish.isPlaying)
            {
                loadingNextLevelEnglish.Play();
            } 

            if (distance <= range/2){
                SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
            }
        }
    }
}
