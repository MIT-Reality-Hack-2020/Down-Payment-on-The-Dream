using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class TriggerManager : MonoBehaviour
{
    #region Variables
    // Various Audio Sources:
    // [0] narrator - main audio source / on the camera
    // [1] music - various music stuff / on the camera
    // [2] house - door opening, newspaper, phone, etc.
    // [3] windows - crashing of windows
    // [4] picket - protestors outside of house
    // [5] cross - creaks as it rises
    // [6] cars - car driving off after breaking windows / police siren

    // Main Level
    public GameObject Level;

    // Audio Sources
    public AudioSource mainAudioSource;
    public AudioSource musicAudioSource;

    // Audio Clips
    private const int numClips = 5;
    public AudioClip[] audioFiles = new AudioClip[numClips];
    public bool[] startedPlaying = new bool[numClips];
    public bool[] alreadyPlayed = new bool[numClips];

    // Triggers / Tabs
    private const int numPullTabs = 5;
    private pullTab_manager pullTab00, pullTab01, pullTab02, pullTab03, pullTab04;
    public GameObject[] pullTabs = new GameObject[numPullTabs];

    // Animators
    private const int numAnimators = 11;
    public Animator[] allAnimators = new Animator[numAnimators];
    #endregion


    #region Monobehaviour
    void Start()
    {
        for (int i = 0; i < numClips; i++)
        {
            startedPlaying[i] = false;
            alreadyPlayed[i] = false;
        }
    }

    void Update()
    {
        // If the Level hasn't been filled
        if (Level == null)
        {
            Level = GameObject.FindGameObjectWithTag("level");
            pullTabs[0] = Level.transform.GetChild(0).transform.GetChild(0).gameObject;
            pullTabs[1] = Level.transform.GetChild(1).transform.GetChild(0).gameObject;
            pullTabs[2] = Level.transform.GetChild(2).transform.GetChild(0).gameObject;
            pullTabs[3] = Level.transform.GetChild(3).transform.GetChild(0).gameObject;
            pullTabs[4] = Level.transform.GetChild(4).transform.GetChild(0).gameObject;

            pullTab00 = pullTabs[0].GetComponent<pullTab_manager>();
            pullTab01 = pullTabs[1].GetComponent<pullTab_manager>();
            pullTab02 = pullTabs[2].GetComponent<pullTab_manager>();
            pullTab03 = pullTabs[3].GetComponent<pullTab_manager>();
            pullTab04 = pullTabs[4].GetComponent<pullTab_manager>();

            // Assigning Animators
            allAnimators[0] = Level.transform.GetChild(0).GetComponent<Animator>(); // house trigger
            allAnimators[10] = Level.transform.GetChild(1).transform.GetChild(1).GetComponent<Animator>();   // mailbox
            allAnimators[1] = Level.transform.GetChild(1).transform.GetChild(2).GetComponent<Animator>();   // letter

            allAnimators[2] = Level.transform.GetChild(3).GetComponent<Animator>(); // house up
            allAnimators[3] = Level.transform.GetChild(2).GetComponent<Animator>(); // newspaper
            allAnimators[9] = Level.transform.GetChild(2).transform.GetChild(3).GetComponent<Animator>();  // protestors

            allAnimators[4] = Level.transform.GetChild(4).GetComponent<Animator>(); // bush trigger
            allAnimators[5] = Level.transform.GetChild(0).transform.GetChild(2).GetComponent<Animator>();   // biker
            allAnimators[6] = Level.transform.GetChild(4).transform.GetChild(1).GetComponent<Animator>();   // cross
            allAnimators[7] = Level.transform.GetChild(6).GetComponent<Animator>(); // ghost
            allAnimators[8] = Level.transform.GetChild(7).GetComponent<Animator>(); // bricks
        }

        // First Audio Plays + First Tab Becomes Available (Introduction)
        if (pullTab00.isOpen && !startedPlaying[0] && !alreadyPlayed[0])
        {
            PlayAudio("Narrative_Intro_00", mainAudioSource, 0, 1.0f, pullTabs[0], pullTabs[1]);
            DelayAnimation(1.15f, allAnimators[2], "house_up");
            DelayAnimation(2f, allAnimators[0], "palm_tree_up");
            DelayAnimation(3f, allAnimators[4], "bush_up");
            DelayAnimation(5f, allAnimators[5], "paperboy");
            DelayAnimation(8f, allAnimators[3], "newspaper_up");
            DelayAnimation(7f, allAnimators[10], "mailbox_up");
        }
   
        // Second Audio Plays + Second Tab Becomes Available (Letter)
        else if (pullTab01.isOpen && !startedPlaying[1] && (alreadyPlayed[0] && !alreadyPlayed[1]))
        {
            PlayAudio("Narrative_Letter_01", mainAudioSource, 1, 1.0f, pullTabs[1], pullTabs[2]);
            DelayAnimation(19f, allAnimators[1], "mailbox");
        }

        // Third Audio Plays + Third Tab Becomes Available (Picketing)
        else if (pullTab02.isOpen && !startedPlaying[2] && (alreadyPlayed[1] && !alreadyPlayed[2]))
        {
            PlayAudio("Narrative_Picketing_02", mainAudioSource, 2, 1.0f, pullTabs[2], pullTabs[3]);
            DelayAnimation(6f, allAnimators[9], "protest");
            DelayAnimation(8f, allAnimators[7], "ghost_up");
        }

        // Fourth Audio Plays + Fourth Tab Becomes Available (Window / Cross)
        else if (pullTab03.isOpen && !startedPlaying[3] && (alreadyPlayed[2] && !alreadyPlayed[3]))
        {
            PlayAudio("Narrative_Cross_03", mainAudioSource, 3, 1.0f, pullTabs[3], pullTabs[4]);
            DelayAnimation(6f, allAnimators[7], "bricks");
            DelayAnimation(9f, allAnimators[6], "cross");
        }

        // Fifth Audio Plays + Fifth Tab Becomes Available (End)
        else if (pullTab04.isOpen && !startedPlaying[4] && (alreadyPlayed[3] && !alreadyPlayed[4]))
        {
            PlayAudio("Narrative_End_04", mainAudioSource, 4, 1.0f, pullTabs[4]);
            DelayAnimation(1f, allAnimators[6], "cross_down");
        }

        else if (alreadyPlayed[4])
        {
            SceneManager.LoadScene("ending");
        }
    }

    // PLAYS AUDIO FILES AND TURNS OFF AFTER PLAYING //
    void PlayAudio(string clipName, AudioSource audioSource, int audioFileNum,
                   float delayT, GameObject currTrigger, GameObject nextTrigger = null)
    {
        // Display Clip Name
        Debug.Log(clipName);

        // Set Temp Variables
        AudioClip currAudioClip = audioFiles[audioFileNum];
        float currAudioLength = currAudioClip.length;

        // Play Audio Clips
        audioSource.clip = currAudioClip;
        audioSource.PlayDelayed(delayT);
        startedPlaying[audioFileNum] = true;

        // Wait to Cancel
        AlreadyPlayedConfirm(currAudioLength, audioFileNum, currTrigger, nextTrigger);
    }
    #endregion

    #region Coroutine
    // DELAY ANIMATION //
    void DelayAnimation(float delayT, Animator anim, string animClip)
    {
        StartCoroutine(DelayAnimationCoroutine(delayT, anim, animClip));
    }
    IEnumerator DelayAnimationCoroutine(float delayT, Animator anim, string animClip)
    {
        yield return new WaitForSeconds(delayT);
        anim.Play(animClip);
    }

    // WAIT FOR AUDIO TO END //
    void AlreadyPlayedConfirm(float currClipLength, int currBool, GameObject currTrigger, GameObject nextTrigger = null)
    {
        StartCoroutine(WaitForAudioToEndCoroutine(currClipLength, currBool, currTrigger, nextTrigger));
    }
    IEnumerator WaitForAudioToEndCoroutine(float currClipLength, int currBool, GameObject currTrigger, GameObject nextTrigger)
    {
        Debug.Log(currClipLength);
        yield return new WaitForSeconds(currClipLength);
        alreadyPlayed[currBool] = true;
        currTrigger.SetActive(false);

        if (nextTrigger != null)
        {
            nextTrigger.SetActive(true);
        }
    }
    #endregion
}
