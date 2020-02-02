using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class cameraAutoFollow : MonoBehaviour
{
    public float distance = -15.0f;
    public GameObject character;

    // Start is called before the first frame update
    void Start()
    {
    }

    // Update is called once per frame
    void Update()
    {
        transform.position = new Vector3(character.transform.position.x, character.transform.position.y, distance);
    }
}
