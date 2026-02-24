using UnityEngine;

public abstract class Usable : MonoBehaviour
{
    //replace with outline shader
    public Material originalMaterial;
    public Renderer objectRenderer;
    public UsableTemplate template;
    
    private void Awake()
    {
        objectRenderer = GetComponent<Renderer>();
        if (objectRenderer == null)
        {
            objectRenderer = GetComponentInChildren<Renderer>();
        }
            originalMaterial = objectRenderer.material;
    }

    // Update is called once per frame
    void Update()
    {
        
    }    
}
