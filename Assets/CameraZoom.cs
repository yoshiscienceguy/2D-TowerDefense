using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraZoom : MonoBehaviour
{
    Camera cam;
    public float maxHeight = 6;
    public float minHeight = -23;
    public float maxWidth = 26;
    public float minWidth = 6;

    public float maxSize = 14;
    public float minSize = 4.5f;
    public float scale = .2f;
    float currentSize;
    public float moveSpeed = 5;
    // Start is called before the first frame update
    void Start()
    {
        cam = GetComponent<Camera>();
        currentSize = cam.orthographicSize;
    }

    // Update is called once per frame
    void Update()
    {
        currentSize -= Input.mouseScrollDelta.y * scale;
        currentSize = Mathf.Clamp(currentSize, minSize, maxSize);
        cam.orthographicSize = currentSize;

        float h = Input.GetAxis("Horizontal");
        float v = Input.GetAxis("Vertical");
        Vector3 direction = new Vector3(h, v, 0);

        if (Input.GetKey(KeyCode.LeftShift))
        {
            direction *= moveSpeed * 2;
        }
        else {
            direction *= moveSpeed;
        }
        

        transform.Translate(direction * Time.deltaTime);


        Vector3 location = transform.position;
        if (location.y + currentSize > maxHeight) {
            location.y = maxHeight - currentSize;
        }
        else if (location.y - currentSize < minHeight)
        {
            location.y = minHeight + currentSize;
        }

        float screenRatio = (float)Screen.width / (float)Screen.height;
        float widthOrtho = Camera.main.orthographicSize * screenRatio;
        Debug.Log(location.x);
        if (location.x + widthOrtho > maxWidth)
        {
            location.x = maxWidth - widthOrtho;
        }
        else if (location.x - widthOrtho < minWidth)
        {
            location.x = minWidth + widthOrtho;
        }

        transform.position = location;
        

        
    }
}
