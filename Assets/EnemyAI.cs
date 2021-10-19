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
            if (currentWaypoint >= Path.childCount-1)
            {
                Destroy(gameObject);
            }
            destination = Path.GetChild(currentWaypoint).position;
        }
        else {
            transform.position = Vector3.MoveTowards(transform.position, destination, speed * Time.deltaTime);
        }
    }

    public void takeDamage(float damage) {
        cHealth -= damage;
        healthbar.fillAmount = cHealth / health;
        if (cHealth <= 0) {
            Destroy(gameObject);
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("turret projectile")) {
            takeDamage(1);
        }
    }
}
