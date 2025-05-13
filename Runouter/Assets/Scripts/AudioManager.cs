using UnityEngine;

public class AudioManager : MonoBehaviour
{
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    public static AudioManager instance;
    [SerializeField]
    private AudioSource effectAudioSource;
    [SerializeField]
    private AudioClip jumpClip;
    [SerializeField]
    private AudioClip tapClip;
    [SerializeField]
    private AudioClip hurtClip;
    [SerializeField]
    private AudioClip giftClip;
    [SerializeField]
    private AudioClip fireClip;
    private bool hasPlaySource = false;




    private void Awake()
    {
        if(instance == null)
        {
            instance = this;

        }
    }
    public bool HasPlaySource()
    {
        return hasPlaySource; 
    }
    public void SetPlaySource(bool value)
    {
        hasPlaySource = value;
    }

    void Start()
    {
        effectAudioSource.Stop();
        hasPlaySource = true;
        
    }
    public void PlayJumpSound()
    {
        effectAudioSource.PlayOneShot(jumpClip);

    }
    public void PlayTapSound()
    {
        effectAudioSource.PlayOneShot(tapClip);
    }
    public void PlayHurtSound()
    {
        effectAudioSource.PlayOneShot(hurtClip);

    }
    public void PlayGiftSound()
    {
        effectAudioSource.PlayOneShot(giftClip);

    }
    public void PlayFireSound()
    {
        effectAudioSource.PlayOneShot(fireClip);
    }
    // Update is called once per frame
    void Update()
    {
        
    }
}
