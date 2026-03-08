using UnityEngine;

public class LaunchSpawner : MonoBehaviour
{
    public Transform spawnPoint;
    public GameObject prefabToSpawn;
    public float spawnRate;
    public Vector3 spawnForce;

    private void Start()
    {
        launch();
    }
    
    void launch()
    {
        var newSpawn =  Instantiate(prefabToSpawn, spawnPoint.position, spawnPoint.rotation);
        newSpawn.GetComponent<Rigidbody>().AddForce(spawnForce, ForceMode.Impulse);
        LeanTween.delayedCall(spawnRate, launch);
    }
}
