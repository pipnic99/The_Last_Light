using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PayerDash : MonoBehaviour
{
    MovimientoJugador moveScript;
    public float dashSpeed;
    public float dashTime;
    public float timedashCD;
    private bool dashCD = false;
    private Animator animator;
    // Start is called before the first frame update
    void Start()
    {
        moveScript = GetComponent<MovimientoJugador>();
        animator = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        if(Input.GetKeyDown(KeyCode.LeftShift) && !dashCD && moveScript.characterController.isGrounded)
        {
            if(!moveScript.haciendoAccion)
            {
                StartCoroutine(Dash());
                dashCD = true;
            }
            
        }
    }
    IEnumerator Dash()
    {
        float startTime = Time.time;
        animator.SetInteger("Dash_Phase", 1);
        while (Time.time < startTime + dashTime)
        {
            moveScript.haciendoAccion = true;
            Vector3 movimiento = new Vector3(moveScript.movimiento.x * animator.GetFloat("DashSpeed"), moveScript.movimiento.y, moveScript.movimiento.z);
            moveScript.characterController.Move(movimiento * Time.deltaTime);

            yield return null;
        }
        animator.SetInteger("Dash_Phase", 0);
        moveScript.haciendoAccion = false;
        yield return new WaitForSeconds(timedashCD);
        dashCD = false;
    }
}
