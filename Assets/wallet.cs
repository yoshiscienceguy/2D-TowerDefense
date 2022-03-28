using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Tilemaps;
public class wallet : MonoBehaviour
{
    public float munny = 100;
    public Text munnyText;
    public Tilemap grid;
    public GameObject Selector;
    public GameObject turrentToPlace;
    public LayerMask TurretLayer;
    // Start is called before the first frame update
    void Start()
    {
        munnyText.text = "$" + munny.ToString();
    }

    public bool buy(float price)
    {
        if (munny - price < 0)
        {
            return false;
        }
        munny -= price;
        munnyText.text = "$" + munny.ToString();
        return true;

    }

    public void sell(float price)
    {
        munny += price;
        munnyText.text = "$" + munny.ToString();

    }
    public void earn(float price)
    {
        munny += price;
        munnyText.text = "$" + munny.ToString();

    }
    // Update is called once per frame
    void Update()
    {
        Vector3 mouseWorldPos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        Vector3Int coordinate = grid.WorldToCell(mouseWorldPos);
        if (grid.HasTile(coordinate))
        {
            Vector3 location = grid.GetCellCenterWorld(coordinate);
            Selector.transform.position = location;
            RaycastHit2D hit = Physics2D.Raycast(location, Vector3.forward,10, TurretLayer);
            if (hit.collider == null)
            {
                if (!Selector.activeSelf)
                {
                    Selector.SetActive(true);
                }
            }
            else
            {
                if (Selector.activeSelf)
                {
                    Selector.SetActive(false);
                }
                Debug.Log(hit.collider.name);
            }
            if (Input.GetMouseButtonDown(0))
            {
                Instantiate(turrentToPlace, location, Quaternion.identity);
            }
        }
        else
        {
            Selector.SetActive(false);
        }
    }
}
