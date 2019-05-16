using UnityEngine;
using System.Collections;

[RequireComponent(typeof(AudioSource))]
public class Ambience : MonoBehaviour
{

    public AudioClip[] clip;

    public float nrClips;

    AudioSource aus;

    public float randomness;
    public float waitMin;
    public float waitMax;

    void Start()
    {
        aus = GetComponent<AudioSource>();

        //aus.loop = true;


        StartCoroutine(PlayEngineSound());
    }

    private void FixedUpdate()
    {
        randomness = Random.Range(-1, nrClips);
    }


    IEnumerator PlayEngineSound()
    {

        while (this != null)
        {

            Debug.Log("playing clip");

            if (randomness <= 1f)
                aus.clip = clip[1];

            if (randomness <= 2f && randomness > 1f)
                aus.clip = clip[2];

            if (randomness <= 3f && randomness > 2f)
                aus.clip = clip[3];

            if (randomness <= 4f && randomness > 3f)
                aus.clip = clip[4];

            if (randomness <= 5f && randomness > 4f)
                aus.clip = clip[5];

            if (randomness <= 6f && randomness > 5f)
                aus.clip = clip[6];

            if (randomness <= 7f && randomness > 6f)
                aus.clip = clip[7];

            if (randomness <= 8f && randomness > 7f)
                aus.clip = clip[8];

            if (randomness <= 9f && randomness > 8f)
                aus.clip = clip[9];

            if (randomness <= 10f && randomness > 9f)
                aus.clip = clip[10];

            if (randomness <= 11f && randomness > 10f)
                aus.clip = clip[11];

            if (randomness <= 12f && randomness > 11f)
                aus.clip = clip[12];

            if (randomness <= 13f && randomness > 12f)
                aus.clip = clip[13];

            aus.Play();
            yield return new WaitForSeconds(aus.clip.length + Random.Range(waitMin,waitMax));






            aus.Play();
        }

    }
}
