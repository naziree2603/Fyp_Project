using UnityEngine;
using UnityEngine.Playables;

public class TimelineTrigger : MonoBehaviour
{
    public PlayableDirector director;
    private bool hasPlayed = false;

    void OnTriggerEnter(Collider other)
    {
        
        if (other.CompareTag("Player") && !hasPlayed)
        {
            hasPlayed = true;

            // Disable player movement
            //var controller = other.GetComponent<PlayerMovement>();
            //if (controller != null)
            //    controller.enabled = false;
            
            // Play timeline
            director.Play();
        }
    }

    //public void EnablePlayer()
    //{
    //    GameObject player = GameObject.FindGameObjectWithTag("Player");

    //    if (player != null)
    //    {
    //        var controller = player.GetComponent<PlayerMovement>();
    //        if (controller != null)
    //            controller.enabled = true;
    //    }
    //}
}