using UnityEngine;

[RequireComponent(typeof(AudioSource))]
public class AudioController : MonoBehaviour
{
    [System.Serializable]
    public enum AudioControllerType : int
    {
        MusicAudioController,
        EffectAudioController,
        InterfaceAudioController
    }

    public AudioControllerType type;
    private AudioSource _audioSource;
    private float _normalVolume;

    public void InitializeAudioController(float value)
    {
        if (!_audioSource) return;
        //value = Mathf.Clamp01(value);
        value = Mathf.Clamp(value, 0, 1.5f);
        _audioSource.volume = _normalVolume * value;
    }

    void Start()
    {
        _audioSource = GetComponent<AudioSource>();
        _normalVolume = _audioSource.volume;

        GameController gameController = GameObject.FindGameObjectWithTag("GameController").GetComponent<GameController>();
        float volume = 1;
        if (type == AudioControllerType.MusicAudioController)
        {
            volume = gameController.musicVolume;
        }
        else
        if (type == AudioControllerType.EffectAudioController)
        {
            volume = gameController.effectsVolume;
        }
        else if (type == AudioControllerType.InterfaceAudioController)
        {
            volume = gameController.interfaceVolume;
        }
        InitializeAudioController(volume);
    }


    void Update()
    {

    }
}
