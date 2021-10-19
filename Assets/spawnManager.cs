using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class spawnManager : MonoBehaviour
{
    public Transform path;

    public GameObject enemy;
    public int wave;
    public float waveCoolDown = 5;
    private bool readyToSpawn;
    private float currentTime;
    private int enemyAmount;
    public int enemiesDestroyed;

    public float spawnRate = .2f;
    private float spawnTime;
    public int spawnedEnemies;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (enemiesDestroyed == enemyAmount)
        {
            if (currentTime < waveCoolDown)
            {
                currentTime += Time.deltaTime;
            }
            else
            {
                readyToSpawn = true;
                currentTime = 0;
                wave++;
                enemyAmount += 5;

            }

        }
        else
        {

            if (readyToSpawn)
            {
                if (spawnTime >= spawnRate)
                {
                    spawnTime = 0;
                    GameObject clone = Instantiate(enemy, transform.position, Quaternion.identity);
                    clone.GetComponent<EnemyAI>().Path = path;
                    spawnedEnemies++;
                }
                else
                {
                    spawnTime += Time.deltaTime;
                }

                if (spawnedEnemies == enemyAmount)
                {
                    readyToSpawn = false;
                }
            }
        }
    }
}
