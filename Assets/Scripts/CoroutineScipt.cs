using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CoroutineScipt : MonoBehaviour
{
    // Delays Animation
    public void DelayAnimation(float delayT, Animator anim, string animName)
    {
        // Calling the StartCoroutine Function
        // Cleaner :)
        StartCoroutine(DelayAnimationCoroutine(delayT, anim, animName));
    }
    public IEnumerator DelayAnimationCoroutine(float delayT, Animator anim, string animName)
    {
        // Waits waitTime Seconds
        yield return new WaitForSeconds(delayT);
        // Plays the animation named animName
        anim.Play(animName);
    }
}
