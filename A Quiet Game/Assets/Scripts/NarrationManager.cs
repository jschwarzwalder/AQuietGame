using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NarrationManager : MonoBehaviour
{

    [SerializeField] AudioClip[] audioClipArray;
    [SerializeField] AudioClip keyboardInstruction;
    [SerializeField] AudioClip controllerInstructions;
    [SerializeField] int narrationDelay;
    [SerializeField] int narrationPause;
    [SerializeField] GameObject exit;
    [SerializeField] GameObject keyCard;

     [SerializeField] AudioSource[] narrationSource; 
    double clipDuration;
    int nextClip;
    int toggle;
    double  nextStartTime;

    // Start is called before the first frame update
    void Start()
    {
        exit.SetActive(false);
        keyCard.SetActive(false);
        narrationSource[toggle].PlayScheduled(AudioSettings.dspTime + narrationDelay);
        audioClipArray[1] = controllerInstructions;
        nextStartTime = AudioSettings.dspTime;
        
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetButton("Jump") ) {
            audioClipArray[1] = keyboardInstruction;
        }
        if (Input.GetButton("Skip") ) {
            narrationSource[toggle].Stop();
            exit.SetActive(true);
            keyCard.SetActive(true);

           Debug.Log("Skip Narration");
            Destroy(gameObject);
        } else if(AudioSettings.dspTime > nextStartTime - 1) {

            AudioClip clipToPlay = audioClipArray[nextClip];

            // Loads the next Clip to play and schedules when it will start
            narrationSource[toggle].clip = clipToPlay;
            narrationSource[toggle].PlayScheduled(nextStartTime);

            // Checks how long the Clip will last and updates the Next Start Time with a new value
            double duration = (double)clipToPlay.samples / clipToPlay.frequency;
            nextStartTime = nextStartTime + duration + narrationPause;

            // Switches the toggle to use the other Audio Source next
            toggle = 1 - toggle;

            // Increase the clip index number, reset if it runs out of clips
            nextClip = nextClip < audioClipArray.Length - 1 ? nextClip + 1 : 0;
        } else if (nextClip >= audioClipArray.Length) {
            exit.SetActive(true);
            keyCard.SetActive(true);
            Debug.Log("End of Narration Reached");

           Destroy(gameObject);
        }
    }
}
