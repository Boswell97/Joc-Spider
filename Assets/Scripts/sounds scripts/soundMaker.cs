using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class soundMaker : MonoBehaviour
{
    public List<AudioClip> audioClips;
    [SerializeField] private string parameterName;
    public float interval = 1f;

    private AudioSource audioSource;
    private float timer = 0f;

    void Start()
    {
        audioSource = gameObject.AddComponent<AudioSource>();
    }

    void Update()
    {
        bool parameterValue = GetParameterValue(parameterName);

        if (parameterValue && audioClips.Count > 0)
        {
            timer += Time.deltaTime;
            if (timer >= interval)
            {
                PlayRandomSound();
                timer = 0f;
            }
        }
    }

    void PlayRandomSound()
    {
        int randomIndex = Random.Range(0, audioClips.Count);
        audioSource.clip = audioClips[randomIndex];
        audioSource.Play();
    }

    bool GetParameterValue(string paramName)
    {
        return Animator.StringToHash(paramName) != 0 && GetComponent<Animator>().GetBool(paramName);
    }
}
