using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyAI : MonoBehaviour
{
    public Transform Path;
    public float speed = 5;
    private Vector3 destination;
    private int currentWaypoint;
    // Start is called before the first frame update
    void Start()
    {
        destination = Path.GetChild(currentWaypoint).position;
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
}
