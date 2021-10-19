using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LookAtEnemy : MonoBehaviour
{
	public float rotSpeed = 90f;

	public Transform target;

	List<Transform> enemies;
    private void Start()
    {
		enemies = new List<Transform>();
    }
    // Update is called once per frame
    void Update()
	{
		if (enemies.Count > 0) {
			float closest = Mathf.Infinity;
			foreach (Transform enemy in enemies) {
				float distance = Vector3.Distance(transform.position, enemy.position);
				if (distance < closest) {
					target = enemy;
					closest = distance;
				}
			}
		}
		// At this point, we've either found the player,
		// or he/she doesn't exist right now.

		if (target == null)
			return; // Try again next frame!

		// HERE -- we know for sure we have a player. Turn to face it!

		Vector3 dir = target.position - transform.position;
		dir.Normalize();

		float zAngle = Mathf.Atan2(dir.y, dir.x) * Mathf.Rad2Deg - 90;

		Quaternion desiredRot = Quaternion.Euler(0, 0, zAngle);

		transform.rotation = Quaternion.RotateTowards(transform.rotation, desiredRot, rotSpeed * Time.deltaTime);

	}

    private void OnTriggerEnter2D(Collider2D collision)
    {
		if (collision.CompareTag("Enemy")) {
			if (!enemies.Contains(collision.transform)) {
				enemies.Add(collision.transform);
			}
		}
    }

	private void OnTriggerExit2D(Collider2D collision)
	{
		if (collision.CompareTag("Enemy"))
		{
			if (enemies.Contains(collision.transform))
			{
				enemies.Remove(collision.transform);
				if (collision.transform == target) {
					target = null;
				}
			}
		}
	}
}
