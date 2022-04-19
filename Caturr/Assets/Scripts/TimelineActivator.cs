using System.Collections;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.Playables;

[RequireComponent(typeof(Collider))]

public class TimelineActivator : MonoBehaviour
{
    public PlayableDirector playableDirector;
    public string playerTAG;
    public Transform interactionLocation;
    public bool autoActivate = false;

    public bool interact { get; set; }

    [Header("Activation Zone Events")]
    public UnityEvent OnPlayerEnter;
    public UnityEvent OnPlayerExit;

    [Header("Timeline Events")]
    public UnityEvent OnTimeLineStart;
    public UnityEvent OnTimeLineEnd;

    private bool isPlaying;
    private bool playerInside;
    private Transform playerTransform;

    public void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag.Equals(playerTAG))
        {
            playerInside = true;
            playerTransform = other.transform;
            OnPlayerEnter.Invoke();
        }
    }
    public void OnTriggerExit(Collider other)
    {
        if (other.gameObject.tag.Equals(playerTAG))
        {
            playerInside = false;
            playerTransform = null;
            OnPlayerExit.Invoke();
        }
    }

    private void PlayTimeline()
    {
        //Place the character at the correct interaction position
        if (playerTransform && interactionLocation)
            playerTransform.SetPositionAndRotation(interactionLocation.position, interactionLocation.rotation);

        // Avoid infinite interaction loop
        if (autoActivate)
            playerInside = false;

        //Play the Timeline
        if (playableDirector)
            playableDirector.Play();

        //Set Variables
        isPlaying = true;
        interact = false;

        //Wait for Timeline to end
        StartCoroutine(waitForTimeLineToEnd());
    }
        private IEnumerator waitForTimeLineToEnd()
        {
        //Invoke the methods linked to the beginning of the cinematic
        OnTimeLineStart.Invoke();

        //Get the duration of the timeline from the playable Director
        float timeLineDuration = (float)playableDirector.duration;

        //Wait until the cinematic playing is over
        while(timeLineDuration > 0)
        {
            timeLineDuration -= Time.deltaTime;
            yield return null;
        }

        //Reset variable
        isPlaying = false;

        //Invoke the methods linked to the end of the cinematic
        OnTimeLineEnd.Invoke();
        }
    private void Update()
    {
        if (playerInside && !isPlaying)
        {
            if (interact || autoActivate)
            {
                PlayTimeline();
            }
        }
    }
}
