using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Container : MonoBehaviour
{
    public Color targetColor;
    private Color currentBoxColor;

    private void OnTriggerStay2D(Collider2D enteredBox)
    {
        Draggable draggable = enteredBox.GetComponent<Draggable>();
        if(enteredBox.CompareTag("Boxes") && !draggable.isDragging)
        {
            SetBoxColor(draggable.GetColor());
            Debug.Log("Box color: " + currentBoxColor);
        }
    }
    public void SetBoxColor(Color boxColor)
    {
        currentBoxColor = boxColor;
        
    }

    public void ClearColor()
    {
        currentBoxColor = Color.clear; // Or any default color that indicates 'empty'
    }
    public bool IsCorrectColor()
    {
            float tolerance = 0.01f; // Adjust this value as needed
    return Mathf.Abs(currentBoxColor.r - targetColor.r) < tolerance &&
           Mathf.Abs(currentBoxColor.g - targetColor.g) < tolerance &&
           Mathf.Abs(currentBoxColor.b - targetColor.b) < tolerance &&
           Mathf.Abs(currentBoxColor.a - targetColor.a) < tolerance;
    }
}
