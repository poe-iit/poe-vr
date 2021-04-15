//using System.Collections;
//using System.Collections.Generic;
//using System;
//using UnityEngine;
//using UnityEngine.XR.Management;

//namespace TheoryOfProgramming
//{
//    class AdjacencyList
//    {
//        LinkedList<Tuple<int, int>>[] adjacencyList;

//        // Constructor - creates an empty Adjacency List
//        public AdjacencyList(int vertices)
//        {
//            adjacencyList = new LinkedList<Tuple<int, int>>[vertices];

//            for (int i = 0; i < adjacencyList.Length; ++i)
//            {
//                adjacencyList[i] = new LinkedList<Tuple<int, int>>();
//            }
//        }

//        // Appends a new Edge to the linked list
//        public void addEdgeAtEnd(int startVertex, int endVertex, int weight)
//        {
//            adjacencyList[startVertex].AddLast(new Tuple<int, int>(endVertex, weight));
//        }

//        // Adds a new Edge to the linked list from the front
//        public void addEdgeAtBegin(int startVertex, int endVertex, int weight)
//        {
//            adjacencyList[startVertex].AddFirst(new Tuple<int, int>(endVertex, weight));
//        }

//        // Returns number of vertices
//        // Does not change for an object
//        public int getNumberOfVertices()
//        {
//            return adjacencyList.Length;
//        }

//        // Returns a copy of the Linked List of outward edges from a vertex
//        public LinkedList<Tuple<int, int>> this[int index]
//        {
//            get
//            {
//                LinkedList<Tuple<int, int>> edgeList
//                               = new LinkedList<Tuple<int, int>>(adjacencyList[index]);

//                return edgeList;
//            }
//        }

//        // Prints the Adjacency List
//        public void printAdjacencyList()
//        {
//            int i = 0;

//            foreach (LinkedList<Tuple<int, int>> list in adjacencyList)
//            {
//                Console.Write("adjacencyList[" + i + "] -> ");

//                foreach (Tuple<int, int> edge in list)
//                {
//                    Console.Write(edge.Item1 + "(" + edge.Item2 + ")");
//                }

//                ++i;
//                Console.WriteLine();
//            }
//        }

//        // Removes the first occurence of an edge and returns true
//        // if there was any change in the collection, else false
//        public bool removeEdge(int startVertex, int endVertex, int weight)
//        {
//            Tuple<int, int> edge = new Tuple<int, int>(endVertex, weight);

//            return adjacencyList[startVertex].Remove(edge);
//        }
//    }

//    class TestGraph
//    {
//        public static void Main()
//        {
//            Console.WriteLine("Enter the number of vertices -");
//            int vertices = Int32.Parse(Console.ReadLine());

//            AdjacencyList adjacencyList = new AdjacencyList(vertices + 1);

//            Console.WriteLine("Enter the number of edges -");
//            int edges = Int32.Parse(Console.ReadLine());

//            Console.WriteLine("Enter the edges with weights -");
//            int startVertex, endVertex, weight;

//            for (int i = 0; i < edges; ++i)
//            {
//                startVertex = Int32.Parse(Console.ReadLine());
//                endVertex = Int32.Parse(Console.ReadLine());
//                weight = Int32.Parse(Console.ReadLine());

//                adjacencyList.addEdgeAtEnd(startVertex, endVertex, weight);
//            }

//            adjacencyList.printAdjacencyList();
//            adjacencyList.removeEdge(1, 2, 1);
//            adjacencyList.printAdjacencyList();
//        }
//    }
//}

//public class PathingController : MonoBehaviour
//{
//    private GameObject[] pathGameObjects = null;
//    private PathObject[] pathObjects = null;
//    private GameObject[] pathObjectLights = null;
//    private int pathLightSequenceStart = 0;
//    private int pathLightSequenceIter = 0;
//    private int frameCounter = 0;

//    public Material lightBulbColor = null;

//    public Color normalColor = new Color(1.0f, 1.0f, 1.0f, 1.0f);
//    public float normalIntensity = 4.0f;
//    public Color nonpathColor = new Color(1.0f, 1.0f, 1.0f, 1.0f);
//    public float nonpathIntensity = 1.0f;
//    public Color inactivePathColor = new Color(0.0f, 1.0f, 0.0f, 1.0f);
//    public float inactivePathIntensity = 4.0f;
//    public Color activePathColor = new Color(1.0f, 0.5f, 0.0f, 1.0f);
//    public float activePathIntensity = 6.0f;
//    public int framesPerPathSequenceState = 30;
//    public float pathLightRange = 2.0f;
//    public float nonpathLightRange = 6.0f;
//    public bool VRMode = false;


//    // Start is called before the first frame update
//    void Start()
//    {
//        child_position = new Vector3(0, 0, 0);
//        child2_position = new Vector3(0, 0, 0);
//        List<Vector3> neighbors = new List<Vector3>[pathGameObjects.Length];
//        dist = new float(0);
//        dist2 = new float(0);

//        if (VRMode is false)
//        {
//            XRGeneralSettings.Instance.Manager.DeinitializeLoader();
//        }
//        else
//        {
//            XRGeneralSettings.Instance.Manager.InitializeLoader();
//        }

//        pathGameObjects = GameObject.FindGameObjectsWithTag("path_object");
//        pathObjects = new PathObject[pathGameObjects.Length];
//        for (int i = 0; i < pathObjects.Length; i++)
//        {
//            pathObjects[i] = pathGameObjects[i].GetComponent<PathObject>();
//        }
//        pathList = new LinkedList<> { };
//        pathObjectLights = new GameObject[pathGameObjects.Length];
//        for (int i = 0; i < pathObjects.Length; i++)
//        {
//            List<Transform> children = new List<Transform>();
//            Helper.GetAllChildren(pathGameObjects[i].transform, ref children);

//            //child to find neighbor nodes from
//            int count_child = 0;
//            foreach (Transform child in children)
//            {
//                if (child.tag == "path_object_light")
//                {
//                    child_position = child.position;
//                    int count = 0;
//                    //pathObjectLights[i] = child.gameObject;
//                }
//                //find neighbor nodes
//                foreach (Transform child2 in children)
//                {
//                    distance_list = new float[3] { 1000, 1000, 1000 };
//                    idx = new int[3] { -1, -1, -1 };

//                    if (child2.tag == "path_object_light")
//                    {

//                        //pathObjectLights[i] = child2.gameObject;
//                        child2_position = child2.position;
//                        float distance = Vector3.Distance(child.position, child2.position);
//                        //if the lights are roughly aligned along the x or z axes
//                        if (Math.Abs(child_position[2] - child2_position[2]) < 0.01) or(Math.Abs(child_position[0] - child2_position[0]) < 0.01);
//                        {
//                            int maxValue = distance_list.Max();

//                            if (distance < maxValue)
//                            {
//                                int maxIndex = distance_list.ToList().IndexOf(maxValue);
//                                distance_list[maxIndex] = distance;
//                                idx[maxIndex] = count;
//                            }
//                        }
//                        count++;
//                    }
//                }
//                //create linked list
//                int minValue = distance_list.Min();
//                for (int i = 0; i <= 3; i++)
//                {
//                    if (distance_list[i] < minValue * 2)
//                    {
//                        pathList.addEdgeAtEnd(children[count_child], children[idx[i]], distance_list[i]);
//                    }
//                }
//                count_child++;
//            }
//        }

//        // A star algorithm
//        // 
//        foreach (LinkedList<Tuple<int, int>> list in pathList)
//        {
//            pathObjectLights[i] = list;
//        }

//        foreach (PathObject p in pathObjects)
//        {
//            if (p.PathLightingSequenceIndex > pathLightSequenceStart)
//                pathLightSequenceStart = p.PathLightingSequenceIndex;
//            // reassign PathLightingSequenceIndex to A star path
//        }

//        pathLightSequenceIter = pathLightSequenceStart + 1;
//        Debug.Log(pathObjects.Length);
//        Debug.Log(pathGameObjects.Length);
//        Debug.Log(pathObjectLights.Length);
//        Debug.Log(pathLightSequenceIter);
//        Debug.Log(pathLightSequenceStart);
//    }

//    // Update is called once per frame
//    void Update()
//    {
//        frameCounter++;
//        if (frameCounter >= framesPerPathSequenceState)
//        {

//            pathLightSequenceIter--;
//            frameCounter = 0;

//            for (int i = 0; i < pathObjects.Length; i++)
//            {
//                Light light = pathObjectLights[i].GetComponent<Light>();
//                //Material bulb = GetComponent<Renderer>().material;
//                //mymat.SetColor("_EmissionColor", Color.red);
//                if (pathObjects[i].PathLightingSequenceIndex == pathLightSequenceIter)
//                {
//                    light.color = activePathColor;
//                    light.intensity = activePathIntensity;
//                    light.range = pathLightRange;

//                }
//                else if (pathObjects[i].PathLightingSequenceIndex < 0)
//                {
//                    light.color = nonpathColor;
//                    light.intensity = nonpathIntensity;
//                    light.range = nonpathLightRange;
//                }
//                else
//                {
//                    light.color = inactivePathColor;
//                    light.intensity = inactivePathIntensity;
//                    light.range = pathLightRange;
//                }
//            }

//            if (pathLightSequenceIter <= 0)
//            {
//                pathLightSequenceIter = pathLightSequenceStart + 1;
//            }
//        }


//    }


//}
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR.Management;


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
    public bool VRMode = false;


    // Start is called before the first frame update
    void Start()
    {
        if (VRMode is false)
        {
            XRGeneralSettings.Instance.Manager.DeinitializeLoader();
        }
        else
        {
            XRGeneralSettings.Instance.Manager.InitializeLoader();
        }
        pathGameObjects = GameObject.FindGameObjectsWithTag("path_object");
        pathObjects = new PathObject[pathGameObjects.Length];
        for (int i = 0; i < pathObjects.Length; i++)
        {
            pathObjects[i] = pathGameObjects[i].GetComponent<PathObject>();
        }
        pathObjectLights = new GameObject[pathGameObjects.Length];
        for (int i = 0; i < pathObjects.Length; i++)
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

                }
                else if (pathObjects[i].PathLightingSequenceIndex < 0)
                {
                    light.color = nonpathColor;
                    light.intensity = nonpathIntensity;
                    light.range = nonpathLightRange;
                }
                else
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
