using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour
{
    [SerializeField] private float delay = 1f;

    void Awake()
    {
        DontDestroyOnLoad(gameObject);
    }

    public void PlayGame ()
    {
        StartCoroutine(ChangeScene());
    }

    public void QuitGame ()
    {
        Application.Quit();
    }

    IEnumerator ChangeScene()
    {
        float elapsedTime = 0;
        float currentVolume = AudioListener.volume;

        while (elapsedTime < delay)
        {
            elapsedTime += Time.deltaTime;
            AudioListener.volume = Mathf.Lerp(currentVolume, 0, elapsedTime / delay);
            yield return null;
        }
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
    }
}