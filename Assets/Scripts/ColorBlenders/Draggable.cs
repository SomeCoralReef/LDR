using UnityEngine;

public class Draggable : MonoBehaviour
{
    private Vector3 offset;
    public Vector3 originalPosition; // Store the original position for potential future use
    public bool isDragging = false;
    private bool isLerping = false;
    private Vector3 lerpTargetPosition;
    private float lerpSpeed = 5f;

    void Awake()
    {
        originalPosition = transform.position; // Set original position
    }

    void OnMouseDown()
    {
        offset = gameObject.transform.position - GetMouseWorldPos();
        isDragging = true;
        isLerping = false;
    }

    void Update()
    {
        if (isDragging)
        {
            transform.position = GetMouseWorldPos() + offset;
        }
        else if (isLerping)
        {
            transform.position = Vector3.Lerp(transform.position, lerpTargetPosition, Time.deltaTime * lerpSpeed);
            if (Vector3.Distance(transform.position, lerpTargetPosition) < 0.01f)
            {
                isLerping = false;
                transform.position = lerpTargetPosition;
            }
        }
    }

    void OnMouseUp()
    {
        isDragging = false;
        CheckAndSnapToContainer(); // Attempt to snap, but don't move if not close enough
    }

    Vector3 GetMouseWorldPos()
    {
        Vector3 mousePoint = Input.mousePosition;
        mousePoint.z = Camera.main.nearClipPlane;
        return Camera.main.ScreenToWorldPoint(mousePoint);
    }

    bool CheckAndSnapToContainer()
    {
        float nearestDistance = Mathf.Infinity;
        GameObject nearestContainer = null;

        foreach (GameObject container in GameObject.FindGameObjectsWithTag("Container"))
        {
            float distance = Vector3.Distance(transform.position, container.transform.position);
            if (distance < nearestDistance)
            {
                nearestContainer = container;
                nearestDistance = distance;
            }
        }

        if (nearestContainer != null && nearestDistance <= 1.0f) // Assuming 1.0f is your snappable distance
        {
            lerpTargetPosition = nearestContainer.transform.position;
            isLerping = true;
            return true; // Snapped to a container
        }
        // No action needed here for Option 2, the box simply stays where it is
        return false; // Not close enough to any container
    }

    public Color GetColor()
    {
        return GetComponent<SpriteRenderer>().color;
    }

}
