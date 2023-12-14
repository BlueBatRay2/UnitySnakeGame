using UnityEngine;

using System;
using TMPro;

public class VisualMap : MonoBehaviour
{
    public TMP_InputField widthInputField;
    public TMP_InputField heightInputField;
    public Material material; 

    private GameObject currentPlaneOrQuad;

    
    public void CreateVisualMap()
    {
        if (!int.TryParse(widthInputField.text, out int width) ||
            !int.TryParse(heightInputField.text, out int height))
        {

            Console.Write("Error, input fields are invalid");
            return;
        }

        if (currentPlaneOrQuad != null)
        {
            currentPlaneOrQuad.transform.localScale = new Vector3(width, height, 1f);
        }
        else
        {
            currentPlaneOrQuad = CreateNewPlaneOrQuad(width, height);
        }
    }
    
    private GameObject CreateNewPlaneOrQuad(float width, float height)
    {
        GameObject planeOrQuad = GameObject.CreatePrimitive(PrimitiveType.Quad); 
        planeOrQuad.transform.localScale = new Vector3(width, height, 1f);

        if (material != null)
        {
            Renderer renderer = planeOrQuad.GetComponent<Renderer>();
            renderer.material = material;
        }

        planeOrQuad.transform.position = new Vector3(0f, 0f, 0f);
        return planeOrQuad;
    }
    
}
