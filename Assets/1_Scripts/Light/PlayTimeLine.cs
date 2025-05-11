using UnityEngine;
using System.Collections.Generic;
using System.Collections;
using UnityEngine.Playables;
using Unity.VisualScripting;

public class PlayTimeLine : MonoBehaviour
{
   public PlayableDirector playableDirector;


    private void OnTriggerEnter(Collider collision)
    {

        if (collision.gameObject.tag == "Player")
        {
            Debug.Log("rentr�");
            play();
        }

    }
        public void play()
    {
        playableDirector.Play();
    }
    
}
