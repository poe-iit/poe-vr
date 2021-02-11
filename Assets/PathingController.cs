using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PathingController : MonoBehaviour
{
    private GameObject[] pathGameObjects = null;
    private PathObject[] pathObjects = null; 
    private GameObject[] pathObjectLights = null;
    private int pathLightSequenceStart = 0;
    private int pathLightSequenceIter = 0;
    private int frameCounter = 0;

    public Material lightBulbColor = null;
    
    public Color normalColor = new Color(1.0f, 1.0f, 1.0f, 1.0f);
    public float normalIntensity = 4.0f;
    public Color nonpathColor = new Color(1.0f, 1.0f, 1.0f, 1.0f);
    public float nonpathIntensity = 1.0f;
    public Color inactivePathColor = new Color(0.0f, 1.0f, 0.0f, 1.0f);
    public float inactivePathIntensity = 4.0f;
    public Color activePathColor = new Color(1.0f, 0.5f, 0.0f, 1.0f);
    public float activePathIntensity = 6.0f;
    public int framesPerPathSequenceState = 30;
    public float pathLightRange = 2.0f;
    public float nonpathLightRange = 6.0f;

    // Start is called before the first frame update
    void Start()
    {
        pathGameObjects = GameObject.FindGameObjectsWithTag("path_object");
        pathObjects = new PathObject[pathGameObjects.Length];
        for (int i = 0; i < pathObjects.Length; i++)
        {
            pathObjects[i] = pathGameObjects[i].GetComponent<PathObject>();
        }
        pathObjectLights = new GameObject[pathGameObjects.Length];
        for (int i=0; i<pathObjects.Length; i++)
        {
            List<Transform> children = new List<Transform>();
            Helper.GetAllChildren(pathGameObjects[i].transform, ref children);

            foreach (Transform child in children)
            {
                if (child.tag == "path_object_light")
                {
                    pathObjectLights[i] = child.gameObject;
                }
            }
        }

        foreach (PathObject p in pathObjects)
        {
            if (p.PathLightingSequenceIndex > pathLightSequenceStart)
                pathLightSequenceStart = p.PathLightingSequenceIndex;
        }

        pathLightSequenceIter = pathLightSequenceStart + 1;
        Debug.Log(pathObjects.Length);
        Debug.Log(pathGameObjects.Length);
        Debug.Log(pathObjectLights.Length);
        Debug.Log(pathLightSequenceIter);
        Debug.Log(pathLightSequenceStart);
    }

    // Update is called once per frame
    void Update()
    {
        frameCounter++;
        if (frameCounter >= framesPerPathSequenceState)
        {

            pathLightSequenceIter--;
            frameCounter = 0;

            for (int i = 0; i < pathObjects.Length; i++)
            {
                Light light = pathObjectLights[i].GetComponent<Light>();
                //Material bulb = GetComponent<Renderer>().material;
                //mymat.SetColor("_EmissionColor", Color.red);
                if (pathObjects[i].PathLightingSequenceIndex == pathLightSequenceIter)
                {
                    light.color = activePathColor;
                    light.intensity = activePathIntensity;
                    light.range = pathLightRange;

                } else if (pathObjects[i].PathLightingSequenceIndex < 0)
                {
                    light.color = nonpathColor;
                    light.intensity = nonpathIntensity;
                    light.range = nonpathLightRange;
                } else
                {
                    light.color = inactivePathColor;
                    light.intensity = inactivePathIntensity;
                    light.range = pathLightRange;
                }
            }

            if (pathLightSequenceIter <= 0)
            {
                pathLightSequenceIter = pathLightSequenceStart + 1;
            }
        }


    }


}
