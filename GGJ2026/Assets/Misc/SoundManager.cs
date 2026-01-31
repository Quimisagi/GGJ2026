using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class Sound
{
    public string name;
    public AudioClip clip;
}

public class SoundManager : MonoBehaviour
{
    public static SoundManager Instance;

    [Header("All Sounds")]
    public List<Sound> sounds;

    [Header("Punch Sounds (length 3)")]
    public List<Sound> punchSounds = new List<Sound>(3);

    private Dictionary<string, AudioClip> soundDictionary;
    private List<AudioClip> punchSoundClips;
    private int maxSimultaneousSounds = 5;
    private List<GameObject> activeSoundObjects = new List<GameObject>();

    void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }

        // Build main sound dictionary
        soundDictionary = new Dictionary<string, AudioClip>();
        foreach (var sound in sounds)
        {
            if (!soundDictionary.ContainsKey(sound.name))
                soundDictionary.Add(sound.name, sound.clip);
        }

        // Build punch sounds list
        punchSoundClips = new List<AudioClip>();
        foreach (var sound in punchSounds)
        {
            if (sound.clip != null)
                punchSoundClips.Add(sound.clip);
        }
    }

    public void PlaySound(string soundName, float volume = 1f)
    {
        if (!soundDictionary.TryGetValue(soundName, out AudioClip clip))
            return;

        if (activeSoundObjects.Count >= maxSimultaneousSounds)
            return;

        GameObject soundObj = new GameObject("Sound_" + soundName);
        AudioSource source = soundObj.AddComponent<AudioSource>();
        source.clip = clip;
        source.volume = volume;
        source.Play();
        activeSoundObjects.Add(soundObj);
        Destroy(soundObj, clip.length);
        StartCoroutine(RemoveAfterFinish(soundObj, clip.length));
    }

    public void PlayRandomPunch(float volume = 1f)
    {
        if (punchSoundClips.Count == 0 || activeSoundObjects.Count >= maxSimultaneousSounds)
            return;

        // Pick a random punch sound
        AudioClip clip = punchSoundClips[Random.Range(0, punchSoundClips.Count)];

        GameObject soundObj = new GameObject("PunchSound");
        AudioSource source = soundObj.AddComponent<AudioSource>();
        source.clip = clip;
        source.volume = volume;
        source.Play();
        activeSoundObjects.Add(soundObj);
        Destroy(soundObj, clip.length);
        StartCoroutine(RemoveAfterFinish(soundObj, clip.length));
    }

    private System.Collections.IEnumerator RemoveAfterFinish(GameObject obj, float delay)
    {
        yield return new WaitForSeconds(delay);
        activeSoundObjects.Remove(obj);
    }
}
