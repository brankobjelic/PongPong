using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Audio : MonoBehaviour
{
    public AudioSource audioSourceSounds;

    public AudioClip wallHit;
    public AudioClip paddleHit;
    public AudioClip scored;

    public void PlayWallHitSound()
    {
        audioSourceSounds.PlayOneShot(wallHit);
    }

    public void PlayPaddleHitSound()
    {
        audioSourceSounds.PlayOneShot(paddleHit);
    }

    public void PlayScoredSound()
    {
        audioSourceSounds.PlayOneShot(scored);
    }

}
