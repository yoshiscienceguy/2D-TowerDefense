using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class spawnManager : MonoBehaviour
{
    public Transform[] paths;

    public GameObject enemy;
    public int wave;
    public float waveCoolDown = 5;
    private bool readyToSpawn;
    private float currentTime;
    private int enemyAmount;
    public int enemiesDestroyed;

    public float min_spawnRate = .1f;
    public float max_spawnRate = .4f;
    private float spawnRate;
    private float spawnTime;
    private int spawnedEnemies;

    private float randomHealth = 3;

    public GameObject WaveText;
    public GameObject CountdownText;
    // Start is called before the first frame update
    void Start()
    {
        enemyAmount = 5;
        enemiesDestroyed = 5;
        CountdownText.SetActive(false);
        WaveText.GetComponentInChildren<Text>().text = "Wave\n" + wave.ToString();
        WaveText.GetComponent<Animator>().SetTrigger("NewWave");

    }

    // Update is called once per frame
    void Update()
    {
        if (enemiesDestroyed == enemyAmount)
        {
            if (currentTime < waveCoolDown)
            {
                CountdownText.SetActive(true);
                currentTime += Time.deltaTime;
                CountdownText.GetComponent<Text>().text = "Round Starts in:\n"+ Mathf.Round(waveCoolDown - currentTime).ToString();
            }
            else
            {
                CountdownText.SetActive(false);
                readyToSpawn = true;
                currentTime = 0;
                enemiesDestroyed = 0;
                wave++;
                enemyAmount = Random.Range(enemyAmount, enemyAmount + 2) ;
                spawnedEnemies = 0;
                randomHealth = Random.Range(randomHealth, randomHealth + .5f);

                WaveText.GetComponent<Animator>().SetTrigger("NewWave");
                WaveText.GetComponentInChildren<Text>().text = "Wave\n" + wave.ToString();
            }

        }
        else
        {

            if (readyToSpawn)
            {
                if (spawnTime >= spawnRate)
                {
                    Transform path = paths[Random.Range(0, paths.Length)];
                    spawnTime = 0;
                    GameObject clone = Instantiate(enemy, transform.position, Quaternion.identity);
                    clone.GetComponent<EnemyAI>().Path = path;

                    
                    clone.GetComponent<EnemyAI>().health = randomHealth;

                    spawnedEnemies++;
                    spawnRate = Random.Range(min_spawnRate, max_spawnRate);
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
