using UnityEngine;

public class AudioManager : MonoBehaviour
{
    [SerializeField] private AudioSource mainAudio;
    [SerializeField] private AudioSource water_splash;
    [SerializeField] private AudioSource attackSound;
    [SerializeField] private AudioSource collectSound;
    [SerializeField] private AudioSource competitionSound;
    private bool isCompetition = false;
    public static AudioManager instance { get; private set; }

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

    public void PlayCollectSound()
    {
        if (!collectSound.isPlaying)
            collectSound.Play();
    }

    public void PlayCompetitionSound()
    {
        if (!competitionSound.isPlaying)
            competitionSound.Play();
    }

    public void StopMainAudio()
    {
        if (mainAudio.isPlaying)
            mainAudio.Stop();
    }

    public void PlayMainAudio()
    {
        if (!mainAudio.isPlaying)
            mainAudio.Play();
    }

    public void ChangeStateForCompetition()
    {
        isCompetition = !isCompetition;
        if (isCompetition)
        {
            StopMainAudio();
            PlayCompetitionSound();
        }
        else
        {
            PlayMainAudio();
            competitionSound.Stop();
        }
    }
}
