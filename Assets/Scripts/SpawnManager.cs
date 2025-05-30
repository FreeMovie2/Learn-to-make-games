using UnityEngine;

public class SpawnManager : MonoBehaviour
{
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    public GameObject enemy;
    public float spawnTime = 0f;
    public float delay = 5f;
    public GameObject []spawnPoint;

    // Update is called once per frame
    void Update()
    {
        spawnTime += Time.deltaTime;
        if (spawnTime >= delay && Player.isAlive)
        {
            int index = Random.Range(0, spawnPoint.Length);
            Instantiate(enemy, spawnPoint[index].transform.position, spawnPoint[index].transform.rotation);
            spawnTime -= delay;
        }
    }
}
