using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ActiveSpawnZone : MonoBehaviour
{

    public GameObject enemyPrefab;
    public GameObject[] enemyPrefabs;
    public List<GameObject> myGameObjects = new List<GameObject>();
    public Transform spawnSlimePoint;
    Vector3 position;
    public float cooldownRespawn = 100f;
    public float currentRespawnTime;

    [SerializeField] private int numberOfEnemy = 3;

    public float activeSpawnDistance = 30f;

    public Transform player;

    private void Awake()
    {
        player = GameObject.FindGameObjectWithTag("Player").GetComponent<Transform>();
        currentRespawnTime = cooldownRespawn;
        spawnSlimePoint = GetComponent<Transform>();

    }

    private void Update()
    {
        if (currentRespawnTime >= cooldownRespawn)
        {
            currentRespawnTime = cooldownRespawn;
        }
        else
        {
            currentRespawnTime += Time.deltaTime;
        }

        if (currentRespawnTime >= cooldownRespawn)
        {
            ActiveDistance();
            if (myGameObjects.Count == 0)
            {
                //currentRespawnTime = 0f;
            }
        }

        for (int i = 0; i < numberOfEnemy; i++)
        {
            if (enemyPrefabs[i] == null)
            {
                // Remove the GameObject from the list
                myGameObjects.Remove(enemyPrefabs[i]);

                
            }            
        }
    }

    private void ActiveDistance()
    {
        Vector3 activeDistance = player.transform.position - this.transform.position;

        if (activeDistance.magnitude <= activeSpawnDistance)
        {
            if (myGameObjects.Count >= 1)
            {
                return;
            }
            else
            {
                for (int i = 0; i < numberOfEnemy; i++)
                {
                    Debug.Log("Spawn");
                    RandomizePosition();
                    enemyPrefabs[i] = Instantiate(enemyPrefab, position, spawnSlimePoint.rotation);
                    myGameObjects.Add(enemyPrefabs[i]);
                }

                currentRespawnTime = 0;
            }
        }
        else
        {
            for (int i = 0; i < numberOfEnemy; i++)
            {
                Destroy(enemyPrefabs[i], cooldownRespawn * 3);
            }
        }

    }   

    void RandomizePosition()
    {
        float x = Random.Range(spawnSlimePoint.transform.position.x - 2, spawnSlimePoint.transform.position.x + 2);
        float y = Random.Range(spawnSlimePoint.transform.position.y - 2, spawnSlimePoint.transform.position.y + 2);
        float z = Random.Range(0f, 0f);

        position = new Vector3(x, y, z);
    }
}
