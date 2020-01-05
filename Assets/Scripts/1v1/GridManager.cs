using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GridManager2 : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }


    // Update is called once per frame
    void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            Vector3 clickPosition = -Vector3.one;
            Plane plane = new Plane(Vector3.up, 0f);
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            float distanceToPlane;

            if(plane.Raycast (ray, out distanceToPlane))
            {
                clickPosition = ray.GetPoint(distanceToPlane);
            }
            Debug.Log(clickPosition);
        }
    }
}
