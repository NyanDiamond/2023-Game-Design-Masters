using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using FMODUnity;
using FMOD.Studio;

public class AudioManager : MonoBehaviour
{
    public static AudioManager instance { get; private set;}

    private EventInstance musicEventInstance;
    private EventInstance ambEventInstance;
    private List<EventInstance> eventInstances;
    //private List<StudioEventEmitter> eventEmitters;

   // [SerializeField] private AmbianceArea ambiance;
  //  [SerializeField] private MusicArea bgmusic;

    private void Awake()
    {
        if(instance != null)
        {
            Debug.LogError("Found more than one Audio Manager in the scene");
        }
        instance = this;
        eventInstances = new List<EventInstance>();
        //eventEmitters = new List<StudioEventEmitter>();

    }

    public void PlayOneShot(EventReference sound, Vector3 worldPos)
    {
        RuntimeManager.PlayOneShot(sound, worldPos);
    }

    public void PlayOneShotAttached(EventReference sound, GameObject parent)
    {
        RuntimeManager.PlayOneShotAttached(sound, parent);
    }
    private void InitializeMusic(EventReference musicEventReference)
    {
        musicEventInstance = CreateInstance(musicEventReference);
        musicEventInstance.start();
    }

    private void InitializeAmbiance(EventReference ambEventReference)
    {
        ambEventInstance = CreateInstance(ambEventReference);
        ambEventInstance.start();
    }

    public EventInstance CreateInstance(EventReference eventReference)
    {
        EventInstance eventInstance = RuntimeManager.CreateInstance(eventReference);
        eventInstances.Add(eventInstance);
        return eventInstance;
    }
    
    // Start is called before the first frame update
    void Start()
    {
       // InitializeMusic(FMODEvents.instance.music);
       // InitializeMusic(FMODEvents.instance.ambiance);

     //   AudioManager.instance.SetAmbianceArea(ambiance);
      //  AudioManager.instance.SetMusicArea(bgmusic);
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void SetAmbianceArea(AmbianceArea ambiance)
    {
        musicEventInstance.setParameterByName("ambiance", (float) ambiance);
    }

    public void SetMusicArea(MusicArea bgmusic)
    {
        musicEventInstance.setParameterByName("bgmusic", (float) bgmusic);
    }
}
