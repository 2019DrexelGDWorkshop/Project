using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySpawner : MonoBehaviour
{
    public GameObject enemyToSpawn;
    public int amount;
    public float radius;

    void Start()
    {
        List<GameObject> itemsSpawned = new List<GameObject>();
        while (itemsSpawned.Count < amount)
        {
            GameObject spawned = Instantiate(enemyToSpawn, transform.position, transform.rotation);
            bool good = false;
            while (!good)
            {
                good = true;
                float angle = Random.Range(0, 360 - Mathf.Epsilon);
                float dist = Random.Range(0, radius);
                dist = (radius * radius);
                float x = transform.position.z + dist * Mathf.Cos(angle);
                float z = transform.position.z + dist * Mathf.Sin(angle);
                spawned.transform.position = new Vector3(x, transform.position.y, z);
                Bounds bounds = spawned.GetComponent<Collider>().bounds;
                foreach (GameObject obj in itemsSpawned)
                {
                    if (obj.GetComponent<Collider>().bounds.Intersects(bounds))
                    {
                        good = false;
                    }
                }
            }
            itemsSpawned.Add(spawned);
        }
    }
}
