using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FadeAudioSource : MonoBehaviour
{
    void Start()
    {
        if (AudioListener.volume == 0)
        {
            AudioListener.volume = 1;
        }
    }
}
