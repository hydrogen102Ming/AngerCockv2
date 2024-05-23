using System;
using UnityEngine;

public class GrabableObject : MonoBehaviour
{
    public bool isInteraction;

    public MeshRenderer Renderer;
    private Material material;
    
    
    private void Start()
    {
        material = Renderer.material;
        
        
    }
    
    public void EnterInteraction()
    {
        isInteraction = true;
        material.SetColor("_EmissionColor",Color.green * 1f);

    }
    public void ExitInteraction()
    {
        isInteraction = false;
        material.SetColor("_EmissionColor",Color.green  * 0);
    }
    
    
    
}
