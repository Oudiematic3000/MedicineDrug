using UnityEngine;

public class LaunchSpawner : MonoBehaviour
{
    public Transform spawnPoint;
    public GameObject prefabToSpawn;
    public float startSpawnTimer=45, maxSpawnTimer=15, spawnTimer, initalTimer=5f;
    public Vector3 spawnForce;
    int count = 0;

    private void Start()
    {
        spawnTimer = startSpawnTimer;
        LeanTween.delayedCall(initalTimer, launch);
    }
    
    void launch()
    {
        if (GameManager.instance.gamePaused) return;
        var newSpawn =  Instantiate(prefabToSpawn, spawnPoint.position, spawnPoint.rotation);
        newSpawn.GetComponent<Rigidbody>().AddForce(spawnForce, ForceMode.Impulse);
        RateIncrease();
        LeanTween.delayedCall(spawnTimer, launch);
    }
    public void RateIncrease()  
    {
        if (spawnTimer <= maxSpawnTimer) return;
        count++;
        if (count >= 2) { spawnTimer -= 3; count = 0; }
    }
}
