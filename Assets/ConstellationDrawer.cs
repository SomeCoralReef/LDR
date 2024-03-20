using System.Collections.Generic;
using UnityEngine;

public class ConstellationDrawer : MonoBehaviour
{
    public GameObject starPrefab; // Assign in inspector
    public LineRenderer lineRenderer; // Assign in inspector
    private List<Vector3> starPositions = new List<Vector3>();

    void Start()
    {
        // Assuming lineRenderer has been assigned either in the Inspector or via GetComponent
        lineRenderer.startWidth = 0.05f; // Set the starting width of the line
        lineRenderer.endWidth = 0.05f;   // Set the ending width of the line
    }
    
    void Update()
    {
        if (Input.GetMouseButtonDown(0)) // Or touch input
        {
            Vector3 pos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            pos.z = 0; // Ensure star isn't placed at camera's z position
            GameObject newStar = Instantiate(starPrefab, pos, Quaternion.identity);
            starPositions.Add(newStar.transform.position);

            UpdateLineRenderer();
        }
    }

    void UpdateLineRenderer()
    {
        lineRenderer.positionCount = starPositions.Count;
        lineRenderer.SetPositions(starPositions.ToArray());
    }

    // Methods for rendering to texture and saving image would go here
}
