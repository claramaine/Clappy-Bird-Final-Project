using System.Linq;
using UnityEngine;

[RequireComponent(typeof(AudioSource))]
public class SoundListener : MonoBehaviour
{
    private AudioSource _audioSource;
    private float[] _data;
    
    [SerializeField] private float duration = 0.1f;
    [SerializeField] private float threshold = 0.1f;
    
    private void Start()
    {
        _audioSource = GetComponent<AudioSource>();
        _audioSource.clip = Microphone.Start(Microphone.devices[0], true, 1, 16000);
        _audioSource.Play();
        _data = new float[(int) (duration * 16000)];
    }

    public bool Listen()
    {
        var offset = Microphone.GetPosition(Microphone.devices[0]) - _data.Length;
        _audioSource.clip.GetData(_data, offset < 0 ? offset + 16000 : offset);
        var sound = _data.Any(sample => Mathf.Abs(sample) > threshold);
        //Debug.Log(sound);
        return sound;
    }
}