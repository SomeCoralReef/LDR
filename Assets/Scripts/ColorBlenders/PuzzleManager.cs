using System.Collections;
using UnityEngine;
using System.Collections.Generic;
public class PuzzleManager : MonoBehaviour
{
    public GameObject[] phase1Objects; // Assign in inspector
    public GameObject[] phase2Objects; // Assign in inspector
    
    private void Start()
    {
        StartCoroutine(PhaseTransition());
    }

    public void Update()
    {
        if (Input.GetKeyDown(KeyCode.R))
        {
            ResetPhase();
        }
    }

    IEnumerator PhaseTransition()
    {
        // Check phase 1 completion
        yield return new WaitUntil(() => CheckAllCorrect());

        // Start scaling down all phase 1 objects simultaneously
        List<Coroutine> scaleDownCoroutines = new List<Coroutine>();
        foreach (var obj in phase1Objects)
        {
            Coroutine coroutine = StartCoroutine(ScaleObjectDown(obj));
            scaleDownCoroutines.Add(coroutine);
        }

        // Wait for all scale down coroutines to complete
        foreach (var coroutine in scaleDownCoroutines)
        {
            yield return coroutine;
        }

        // Optionally, destroy or deactivate phase 1 objects
        foreach (var obj in phase1Objects)
        {
            Destroy(obj); // or obj.SetActive(false);
        }

        // Instantiate or activate phase 2 objects with initial scale of 0
        foreach (var obj in phase2Objects)
        {
            obj.transform.localScale = Vector3.zero;
            obj.SetActive(true);
        }

        // Scale up phase 2 objects
        foreach (var obj in phase2Objects)
        {
            obj.SetActive(true); // Ensure the object is active
            obj.transform.localScale = Vector3.zero; // Set scale to 0 to start the scale up animation from nothing
            yield return ScaleObjectUp(obj);
        }
    }

    IEnumerator ScaleObjectDown(GameObject obj)
    {
        // 1 second delay before starting to scale down
        yield return new WaitForSeconds(1);
        // Adjust this value to scale down faster or slower
        float scaleDownSpeed = 1f; // Example: Increase this value to scale down faster

        while (obj.transform.localScale.x > 0)
        {
            obj.transform.localScale -= new Vector3(scaleDownSpeed, scaleDownSpeed, scaleDownSpeed) * Time.deltaTime;
            yield return null;
        }
    }

    IEnumerator ScaleObjectUp(GameObject obj)
    {
        while (obj.transform.localScale.x < 1) // Assuming final scale is 1,1,1
        {
            obj.transform.localScale += new Vector3(3.5f, 3.5f, 3.5f) * Time.deltaTime;
            yield return null;
        }
    }

    // Placeholder for a method to check if all containers have the correct box
    public bool CheckAllCorrect()
    {
    foreach (var containerObj in phase1Objects) // If your containers are part of phase1Objects
    {
        Container container = containerObj.GetComponent<Container>();
        if (container != null && !container.IsCorrectColor())
        {
            //Debug.Log("Found a container without the correct box. Stopping Phase 1.");
            return false; // Found at least one container without the correct box
        }
    }
    Debug.Log("All containers have the correct box. Proceeding to Phase 2.");
    return true; // All containers have the correct box
    }

    public void ResetPhaseColors()
    {
        foreach (var containerObj in phase1Objects)
        {
            Container container = containerObj.GetComponent<Container>();
            if (container != null)
            {
                container.ClearColor();
            }
        }
    }

    private void ResetPhase()
    {
        ResetPhaseColors();
        StopAllCoroutines();
        foreach (var obj in phase1Objects)
        {
        // Check if the GameObject has a Draggable component
            Draggable draggableComponent = obj.GetComponent<Draggable>();

        // If it has a Draggable component, then reset its position
            if (draggableComponent != null && obj.activeSelf)
            {
                obj.transform.position = draggableComponent.originalPosition;
            }
        }   
    }
}