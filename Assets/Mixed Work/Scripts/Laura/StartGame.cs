using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class StartGame : MonoBehaviour {

    Image fade;
    public float fadeSpeed = 20;
    public bool allowLoad;
    bool startedLoad = true;

    private void Start()
    {
        fade = GameObject.Find("Fade").GetComponent<Image>();
        StartCoroutine(Fade(true));
    }

    private void Update()
    {
        if (!startedLoad)
        {
            StartCoroutine(LoadInBackground());
            startedLoad = true;
        }
    }

    public void LoadByName (string sceneName)
	{
        StartCoroutine(Fade(false));
	}

    IEnumerator LoadInBackground()
    {
        AsyncOperation asyncOperation = SceneManager.LoadSceneAsync(0);
        asyncOperation.allowSceneActivation = false;
        while (!asyncOperation.isDone)
        {
            if (asyncOperation.progress >= 0.9f)
            {
                if (allowLoad)
                {
                    asyncOperation.allowSceneActivation = true;
                }
            }
            yield return null;
        }
    }

    IEnumerator Fade(bool fadeIn)
    {
        if (fadeIn)
        {
            for (float f = 1; f >= 0; f = f - 1/fadeSpeed)
            {
                fade.color = new Color(fade.color.r, fade.color.b, fade.color.g, f);
                yield return new WaitForSeconds(1/fadeSpeed);
            }
            fade.color = new Color(fade.color.r, fade.color.b, fade.color.g, 0);
            startedLoad = false;
        }
        else
        {
            for (float f = 0; f <= 1; f = f + 1/fadeSpeed)
            {
                fade.color = new Color(fade.color.r, fade.color.b, fade.color.g, f);
                yield return new WaitForSeconds(1/fadeSpeed);
            }
            fade.color = new Color(fade.color.r, fade.color.b, fade.color.g, 1);
            allowLoad = true;
        }
    }
}
