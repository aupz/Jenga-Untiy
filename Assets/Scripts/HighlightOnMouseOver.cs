using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
public class HighlightOnMouseOver  : MonoBehaviour
{
    public Material highlightMaterial;  // Material to apply when the object is highlighted
    private Material originalMaterial;  // Original material of the object

    private Renderer objectRenderer;  // Renderer component of the object
    private bool isHighlighted;
    private void Start()
    {
        objectRenderer = GetComponent<Renderer>();
        originalMaterial = objectRenderer.material;
    }

    private void OnMouseEnter()
    {
        objectRenderer.material = highlightMaterial;
        isHighlighted = true;
    }

    private void OnMouseExit()
    {
        objectRenderer.material = originalMaterial;
        isHighlighted = false;
    }
    private void Update()
        {
            // Check for a click with the right mouse button
            if (isHighlighted && Input.GetMouseButtonUp(1))
            {
                // Object is clicked while highlighted with the right mouse button, perform desired action
                ActivateObject();
            }
        }

    private void ActivateObject()
    {
        GradeInformation gradeInformation = gameObject.GetComponent<GradeInformation>();
        GameObject panel = GameObject.FindGameObjectWithTag("panel");       
        Image imageComponent = panel.GetComponent<Image>();
        print(imageComponent);
        if (imageComponent != null)
        {
            // Activate the Image component by setting the gameObject active
            imageComponent.enabled = true;
            Transform childTransform = panel.transform.Find("Text");
            print(childTransform.gameObject);
            TextMeshProUGUI textComponent = childTransform.gameObject.GetComponent<TextMeshProUGUI>();
            print(textComponent);
            textComponent.text = gradeInformation.getStudentGrade().grade+":"+gradeInformation.getStudentGrade().domain+"\n\n"+gradeInformation.getStudentGrade().cluster+"\n\n"+
            gradeInformation.getStudentGrade().standardid+":"+gradeInformation.getStudentGrade().standarddescription;
        }
        else
        {
            Debug.LogWarning("Image component not found on the targetObject!");
        }

    }
}
