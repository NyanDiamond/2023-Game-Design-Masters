using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.SceneManagement;

/// <summary>
/// Purpose: Changes the music track or ambience track.
/// This can also be used to automatically replace the music track without being touched or anything if musicIntro is true
/// Author: Sean Lee 2/28/24
/// 
/// NOTE: No support for replacement tracks with intros currently.
/// </summary>

public class TriggerMusicChanger : MonoBehaviour
{

    [Header("NOTE: INTRO AUDIO WILL NOT MATCH UP ON FIRST LOAD!\nThis is because replaceIntroTrack begins before the scene is fully loaded.\nI am working on a fix but this should be mostly fine as-is for now.")]

    

    [SerializeField] private AudioClip newSound;

    //Obsolete because we're setting the desired mixer group manually.
    //If we can have this be automatic in the future, uncomment this.
    //[SerializeField] private bool ambience;

    [Tooltip("If set to true, this object will instead find the track it is supposed to replace and replace it immediately upon the track ending. This allows for intros to the music.")]
    [SerializeField] private bool musicIntro;

    [Tooltip("If set to true, this object will delete itself after firing.")]
    [SerializeField] private bool onlyOnce;

    //This is utterly fucking absurd, but Unity won't let me just do a comparison on mixer groups with strings, so we need to set the group manually.
    [Tooltip("Set this to Music or Ambience")]
    [SerializeField] AudioMixerGroup desiredGroup;

    [Tooltip("Intro Only: Getting Exact Lengths of some sound formats is difficult and leads to inaccuracies.\nAdd the difference in time between when the intro ends and main track begins here and the number will be subtracted from the wait time.")]
    [SerializeField] private float timeAdjustment;

    [Space(10)]
    [Header("Activator Options")]
    [Tooltip("Do we want to limit the objects that activate this trigger to specific gameobjects from a list?\nIf False, uses accepted Tags instead.")]
    [SerializeField] private bool useAcceptedList;
    [Tooltip("The accepted objects that can activate this trigger.\nRequires useAcceptedList to be enabled")]
    [SerializeField] private List<GameObject> acceptedObjects;
    [Tooltip("The accepted gameobject tags that can activate this trigger")]
    [SerializeField] private List<string> acceptedTags;

    //The player
    private GameObject player;
    //The audio source that music plays from (or whatever we're replacing, anyway)
    private AudioSource music;
    //all of the audio source objects in the player
    private AudioSource[] tracks;

    //We use this to make sure that the trigger has been run once already.
    private bool hasRun = false;
    //We use this to make sure the toggle only happens when something enters the trigger for the first time.
    //This should prevent the toggle from happening again if two accepted objects are inside
    private bool inTrigger = false;

    [Space(10)]
    [Header("Enable logs")]
    [SerializeField] private bool debug = false;

    //We use Awake instead of Start for 2 reasons:
    //1. We need to get the audio source as quickly as possible
    //2. We need to pause the music until the scene is fully loaded
    void Awake()
    {
        //Get the player, who has the audio sources we're looking for
        player = FindObjectOfType<IsoPlayerController>().gameObject;
        //Debug.Log("Music Changer: Player is set to " + player);
        //Make a list of audio sources on the player (since we have multiple)
        tracks = player.GetComponents<AudioSource>();


        Debug.Log("Music Changer: Length of tracks array: " + tracks.Length);

        //Find whichever one is the music track
        foreach (AudioSource i in tracks)
        {
            //Old Code that tries to dynamically search the audio mixer.
            //Keeping here in case I can get it working because hoo boy it would be nicer than having to manually set it

            /*
            Debug.Log("Music Changer: Checking an audiosource. mixer group for this one is " + i.outputAudioMixerGroup);

            //if the track is in the Music group, set music to it.
            if (i.outputAudioMixerGroup.Equals("Music")) music = i;
            //If the track is in the Ambience group and ambience is on, set music to this instead.
            if (i.outputAudioMixerGroup.Equals("Ambience") && ambience) music = i;

            Debug.Log("Music Changer: Run group setter. Music's mixer group is now " + music.outputAudioMixerGroup);
            */

            //New code, just use desiredGroup
            if (i.outputAudioMixerGroup == desiredGroup) music = i;
        }

        Debug.Log("Music Changer: Music length is allegedly " + music.clip.length);

        //Now we stop the music track until the scene is fully loaded. This will prevent desync caused by scene loading.
        music.Stop();

        
    }

    //OnEnable *allegedly* only begins AFTER the scene is fully loaded.
    //We stop the music in Awake and then start it here so that it's consistent between loads.
    private void OnEnable()
    {
        //Start the music
        music.Play();
        //Now run musicIntro
        if (musicIntro) StartCoroutine(replaceIntroTrack(music.clip.length - timeAdjustment));
    }

    private void OnTriggerEnter(Collider other)
    {
        //First off, make sure we're an accepted object and the trigger is empty
        if (CheckAccepted(other.gameObject) && (!inTrigger))
        {
            inTrigger = true;
            music.clip = newSound;
            hasRun = true;
        }
        //If we're set to only run once and the run was valid, destroy ourselves
        if (onlyOnce && hasRun) Destroy(this);
    }

    private void OnTriggerExit(Collider other)
    {
        //Check to make sure the object exiting our trigger is accepted and inTrigger is true
        if (CheckAccepted(other.gameObject) && inTrigger)
        {
            //disable inTrigger so we know the trigger is empty
            inTrigger = false;
        }
    }

    //check to see if this object is acceptable
    private bool CheckAccepted(GameObject obj)
    {
        //If the object is on the accepted list, return true
        if (useAcceptedList && acceptedObjects.Contains(obj)) return true;
        //If we're going by tags and the object's tag is on the tag list, return true
        if ((!useAcceptedList) && acceptedTags.Contains(obj.gameObject.tag)) return true;
        //If neither of those checks run, return false
        return false;
    }

    IEnumerator replaceIntroTrack(float waitTime)
    {
        Debug.Log("Music Changer: replaceIntroTrack begun. Should wait for " + waitTime);
        yield return new WaitForSecondsRealtime(waitTime);
        Debug.Log("Music Changer: Wait time is over. Stopping, setting, starting again.");
        //music.Stop();
        music.clip = newSound;
        music.Play();
    }
}
