using System;
using System.Collections.Generic;
using UnityEngine;

public class StomachRendererSmoothScript : MonoBehaviour
{
    private List<GameObject> stomachParts;
    private LineRenderer lineRenderer;
    [SerializeField]
    private int resolution = 10; // Change this to control the smoothness

    void Start()
    {
        lineRenderer = GetComponent<LineRenderer>();

        // Automatically assign GameObjects with the "StomachPart" tag to the stomachParts list
        GameObject[] partsArray = GameObject.FindGameObjectsWithTag("Stomach");

        // Sort the array based on the suffix of their names
        Array.Sort(partsArray, (x, y) => string.Compare(x.name, y.name));

        // Convert sorted array to a List
        stomachParts = new List<GameObject>(partsArray);

        // Set the number of points to stomachParts.Count * resolution + 1
        lineRenderer.positionCount = stomachParts.Count * resolution + 1;
    }

    void Update()
    {
        int pointIndex = 0;
    
        for (int i = 0; i < stomachParts.Count; i++)
        {
            // Get the current stomach part and the next three, wrapping around the array if necessary
            Vector3 p0 = stomachParts[i].transform.position;
            Vector3 p1 = stomachParts[(i + 1) % stomachParts.Count].transform.position;
            Vector3 p2 = stomachParts[(i + 2) % stomachParts.Count].transform.position;
            Vector3 p3 = stomachParts[(i + 3) % stomachParts.Count].transform.position;

            // Generate interpolated points between each pair of stomach parts
            for (int j = 0; j < resolution; j++)
            {
                float t = j / (float)resolution;
                Vector3 interpolatedPosition = CatmullRom(p0, p1, p2, p3, t);
                lineRenderer.SetPosition(pointIndex++, interpolatedPosition);
            }
        }

        // Ensure the loop is closed
        lineRenderer.SetPosition(pointIndex, stomachParts[0].transform.position);
    }

    Vector3 CatmullRom(Vector3 p0, Vector3 p1, Vector3 p2, Vector3 p3, float t)
    {
        Vector3 a = 2f * p1;
        Vector3 b = p2 - p0;
        Vector3 c = 2f * p0 - 5f * p1 + 4f * p2 - p3;
        Vector3 d = -p0 + 3f * p1 - 3f * p2 + p3;

        return 0.5f * (a + b * t + c * t * t + d * t * t * t);
    }
}