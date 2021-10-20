using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class EnemyAI : MonoBehaviour
{
    public Transform Path;
    public float speed = 5;
    private Vector3 destination;
    private int currentWaypoint;
    public float rotSpeed = 360;

    public float health = 3;
    private float cHealth;
    public Image healthbar;


    // Start is called before the first frame update
    void Start()
    {
        cHealth = health;
        destination = Path.GetChild(currentWaypoint).position;
        healthbar.fillAmount = cHealth / health;
    }

    // Update is called once per frame
    void Update()
    {
        float distance = Vector3.Distance(transform.position, destination);
        if (distance <= .01f)
        {
            currentWaypoint++;
            if (currentWaypoint >= Path.childCount)
            {
                Destroy(gameObject);
            }
            else
            {
                destination = Path.GetChild(currentWaypoint).position;
            }
        }
        else {
            transform.position = Vector3.MoveTowards(transform.position, destination, speed * Time.deltaTime);
        }


        Vector3 dir = destination - transform.position;
        dir.Normalize();

        float zAngle = Mathf.Atan2(dir.y, dir.x) * Mathf.Rad2Deg ;

        Quaternion desiredRot = Quaternion.Euler(0, 0, zAngle);

        transform.rotation = Quaternion.RotateTowards(transform.rotation, desiredRot, rotSpeed * Time.deltaTime);

    }

    public void takeDamage(float damage) {
        cHealth -= damage;
        healthbar.fillAmount = cHealth / health;
        if (cHealth <= 0) {
            GameObject.Find("Spawn").GetComponent<spawnManager>().enemiesDestroyed++;
            Destroy(gameObject);
        }
    }


}
