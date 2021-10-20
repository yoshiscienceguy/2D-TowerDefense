using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class turrentShoot : MonoBehaviour
{
    LookAtEnemy lookscript;
    public float shootingfrequency = .2f;
    private float currentFrequency;
    float shootingrange;
    public float turretDamage;
    public LayerMask layers;
    private bool canShoot;
    private GameObject gunFlare;
    // Start is called before the first frame update
    void Start()
    {
        shootingrange = GetComponent<CircleCollider2D>().radius;
        lookscript = GetComponent<LookAtEnemy>();
        gunFlare = transform.GetChild(0).gameObject;
        gunFlare.SetActive(false);
    }

    // Update is called once per frame
    private void Update()
    {
        if (!canShoot)
        {
            if (currentFrequency < shootingfrequency)
            {
                currentFrequency += Time.deltaTime;
            }
            else
            {
                canShoot = true;
            }
        }
    }
    void LateUpdate()
    {
        if (lookscript.target != null && canShoot) {
            Debug.DrawRay(transform.position, transform.up);
                
            RaycastHit2D hit = Physics2D.Raycast(transform.position, transform.up, shootingrange,layers);
            if (hit.collider != null) {
                Debug.Log(hit.collider.gameObject.name);
                hit.collider.gameObject.GetComponent<EnemyAI>().takeDamage(turretDamage);
                StartCoroutine("gunFlash");
            }

            canShoot = false;
            currentFrequency = 0;
            
        }
    }

    IEnumerator gunFlash() {
        gunFlare.SetActive(true);
        yield return new WaitForSeconds(.1f);
        gunFlare.SetActive(false);
    }
}
