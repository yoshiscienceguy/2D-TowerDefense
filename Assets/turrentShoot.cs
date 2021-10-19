using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class turrentShoot : MonoBehaviour
{
    LookAtEnemy lookscript;
    public float shootingfrequency = .2f;
    float shootingrange;
    public float turretDamage;
    public LayerMask layers;
    // Start is called before the first frame update
    void Start()
    {
        shootingrange = GetComponent<CircleCollider2D>().radius;
        lookscript = GetComponent<LookAtEnemy>();
    }

    // Update is called once per frame
    void LateUpdate()
    {
        if (lookscript.target != null) {
            RaycastHit2D hit = Physics2D.Raycast(transform.position, transform.forward, shootingrange,layers);
            if (hit.collider != null) {
                Debug.Log(hit.collider.gameObject.name);
                hit.collider.gameObject.GetComponent<EnemyAI>().takeDamage(turretDamage);
            }
                
            
        }
    }
}
