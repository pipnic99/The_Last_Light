using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RaycastDetection : MonoBehaviour
{
    public float raycastDistance = 10f;
    public float raycastHeight = 3f;
    private Vector3 startPosition;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        startPosition = this.transform.position;
        startPosition.y += raycastHeight;
        RaycastHit hit;
        if(Physics.Raycast(startPosition, transform.forward, out hit, raycastDistance))
        {
            if(hit.collider.CompareTag("Player"))
            {
                Debug.Log("Player hit!");
            }
        }
        Debug.DrawRay(startPosition, transform.forward * raycastDistance, Color.green);
    }
}
