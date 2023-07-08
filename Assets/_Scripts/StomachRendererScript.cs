using System;
using System.Collections.Generic;
using UnityEngine;

public class StomachRendererScript : MonoBehaviour
{
    private List<GameObject> stomachParts;
    private LineRenderer lineRenderer;

    void Start()
    {
        lineRenderer = GetComponent<LineRenderer>();

        // Automatically assign GameObjects with the "StomachPart" tag to the stomachParts list
        GameObject[] partsArray = GameObject.FindGameObjectsWithTag("Stomach");

        // Sort the array based on the suffix of their names
        Array.Sort(partsArray, (x, y) => string.Compare(x.name, y.name));

        // Convert sorted array to a List
        stomachParts = new List<GameObject>(partsArray);

        // Set the number of points to the number of GameObjects
        lineRenderer.positionCount = stomachParts.Count + 1;
    }

    void Update()
    {
        // Update the LineRenderer's points to the current position of the GameObjects
        for (int i = 0; i < stomachParts.Count; i++)
        {
            lineRenderer.SetPosition(i, stomachParts[i].transform.position);
        }

        // Close the loop by setting the last point to the position of the first GameObject
        lineRenderer.SetPosition(stomachParts.Count, stomachParts[0].transform.position);
    }
}
