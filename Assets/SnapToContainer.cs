using UnityEngine;
using System.Collections;

public class SnapToContainer : MonoBehaviour
{
    public Transform container; // Assign the Container's Transform in the Inspector
    private bool isDragging = false;

    void Update()
    {
        if (!isDragging)
        {
            float distance = Vector2.Distance(transform.position, container.position);
            if (distance < .5f)
            {
                // Start Lerp to Container's position
                StartCoroutine(LerpPosition(container.position, 0.5f)); // Lerping over half a second
                isDragging = false;
            }
        }
    }

    IEnumerator LerpPosition(Vector2 targetPosition, float duration)
    {
        float time = 0;
        Vector2 startPosition = transform.position;

        while (time < duration)
        {
            transform.position = Vector2.Lerp(startPosition, targetPosition, time / duration);
            time += Time.deltaTime;
            yield return null;
        }

        transform.position = targetPosition;
    }

    void OnMouseDown()
    {
        isDragging = true;
    }

    void OnMouseUp()
    {
        isDragging = false;
    }
}