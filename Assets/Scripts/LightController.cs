using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LightController : MonoBehaviour
{

    private GameObject[] ceilingLights = null;
    private int lightFlashIndex = 0;
    private int lightFramesCounter = 0;
    public int framesPerLight = 5;

    // Start is called before the first frame update
    void Start()
    {
        ceilingLights = GameObject.FindGameObjectsWithTag("celing_light_light");
        Array.Sort(ceilingLights, new GameObjectPositionComparer());
        Debug.Log(ceilingLights.Length);
    }

    // Update is called once per frame
    void Update()
    {
        lightFramesCounter += 1;
        if (lightFramesCounter >= framesPerLight)
        {
            ceilingLights[lightFlashIndex].GetComponent<Light>().color = new Color(0.0f, 0.0f, 0.0f, 1.0f);
            lightFlashIndex += 1;
            if (lightFlashIndex >= ceilingLights.Length)
                lightFlashIndex = 0;
            ceilingLights[lightFlashIndex].GetComponent<Light>().color = new Color(1.0f, 0.0f, 0.0f, 1.0f);
            ceilingLights[lightFlashIndex].GetComponent<Light>().intensity = 8.0f;
            lightFramesCounter = 0;
        }
    }

    
}


public class GameObjectPositionComparer : IComparer<GameObject>
{
    public int Compare(GameObject a, GameObject b)
    {
        float delta = a.transform.position.x - b.transform.position.x;
        if (delta != 0.0f)
        {
            if (delta < 0.0f)
                return -1;
            if (delta > 0.0f)
                return 1;
        }

        delta = a.transform.position.z - b.transform.position.z;
        if (delta != 0.0f)
        {
            if (delta < 0.0f)
                return -1;
            if (delta > 0.0f)
                return 1;
        }

        delta = a.transform.position.y - b.transform.position.y;
        if (delta != 0.0f)
        {
            if (delta < 0.0f)
                return -1;
            if (delta > 0.0f)
                return 1;
        }

        return 0;

    }
}