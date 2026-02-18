using UnityEngine;

public abstract class Usable : MonoBehaviour
{
    //replace with outline shader
    public Material originalMaterial;
    public Renderer objectRenderer;
    private void Awake()
    {
        objectRenderer = GetComponent<Renderer>();
        originalMaterial = objectRenderer.material;
    }
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }    
}
