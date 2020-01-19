using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LightController : MonoBehaviour
{

    #region Variables
    [SerializeField] private CoroutineScipt coRout;
    [SerializeField] private Light houseSpotLight;
    private Animator LightContAnimator = null;

    #endregion

    #region Monobehaviour
    void Start()
    {
        // Grab components
        LightContAnimator = GetComponent<Animator>();

        // Setting Spot Light Range and Intensity
        houseSpotLight.intensity = 0f;
        houseSpotLight.spotAngle = 1f;

        // Calling FadeUp
        HouseSpotFadeUp(5f);
    }

    void HouseSpotFadeUp(float waitTime)
    // Calls the Light Controller Animator to run the "houseSpotLight_FadeUp" Animation
    {
        coRout.DelayAnimation(waitTime, LightContAnimator, "houseSpotLight_FadeUp");
    }
    #endregion
}
