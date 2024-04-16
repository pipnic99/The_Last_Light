using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RaycastDetection : MonoBehaviour
{
    public float raycastDistance = 10f;
    public float raycastHeight = 3f;
    private Vector3 startPosition;
    public GameManager gameManager;
    public Animator animator;
    public GameObject muzleFlash;
    // Start is called before the first frame update
    void Start()
    {
        animator = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        startPosition = this.transform.position;
        startPosition.y += raycastHeight;
        RaycastHit hit;
        if(Physics.Raycast(startPosition, transform.forward, out hit, raycastDistance))
        {
            if (hit.collider.CompareTag("Player"))
            {
                gameManager.IsAlive = false;
                animator.SetBool("Shoot", true);
                if (animator.GetFloat("ShotTiming") > 0.3)
                {
                    gameManager.blood = true;
                    muzleFlash.SetActive(true);
                }
            }
        }
        Debug.DrawRay(startPosition, transform.forward * raycastDistance, Color.green);
        
    }
}
