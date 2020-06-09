using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ColorShader : MonoBehaviour
{
    public Material material;
    public float red = 1;
    public float green = 1;
    public float blue = 1;
    public float redValue = 1;
    public float greenValue = 1;
    public float blueValue = 0;
    private float sent = 10;
    private float maxLight = 30;
    private bool effect = false;

    private void Start()
    {
        material.SetFloat("_Red", red);
        material.SetFloat("_Green", green);
        material.SetFloat("_Blue", blue);
    }

    private void OnRenderImage(RenderTexture source, RenderTexture destination)
    {
        Graphics.Blit(source, destination, material);
    }
    // Update is called once per frame
    void Update()
    {
        if (effect)
        {
            red += Time.deltaTime * sent;
            green += Time.deltaTime * sent;
            blue += Time.deltaTime * sent;
            red = Mathf.Clamp(red, redValue, maxLight);
            green = Mathf.Clamp(green, greenValue, maxLight);
            blue = Mathf.Clamp(blue, blueValue, maxLight);
            material.SetFloat("_Red", red);
            material.SetFloat("_Green", green);
            material.SetFloat("_Blue", blue);
            if (red >= maxLight || green >= maxLight || blue >= maxLight) sent *= -1;
            if (red <= redValue && green <= greenValue && blue <= blueValue)
            {
                sent *= -1;
                effect = false;
            }
        }


    }

    

    public void deleteRed()
    {
        redValue = 0;
        effect = true;
    }
    public void deleteBlue()
    {
        blueValue = 0;
        effect = true;
    }
    public void deleteGreen()
    {
        greenValue = 0;
        effect = true;
    }
    
}
