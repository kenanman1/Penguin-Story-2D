using UnityEngine;

public class AudioManager : MonoBehaviour
{
    [SerializeField] private AudioSource water_splash;
    [SerializeField] private AudioSource attackSound;
    public static AudioManager instance;

    private void Awake()
    {
        if (instance == null)
            instance = this;
        else
            Destroy(gameObject);
    }

    public void PlayWaterSplashSound()
    {
        if (!water_splash.isPlaying)
            water_splash.Play();
    }

    public void PlayAttackSound()
    {
        if (!attackSound.isPlaying)
            attackSound.Play();
    }
}
