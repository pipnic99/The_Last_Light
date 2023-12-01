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
    // Start is called before the first frame update
    void Start()
    {
        moveScript = GetComponent<MovimientoJugador>();
    }

    // Update is called once per frame
    void Update()
    {
        if(Input.GetKeyDown(KeyCode.LeftShift) && !dashCD)
        {
            StartCoroutine(Dash());
            dashCD = true;
        }
    }
    IEnumerator Dash()
    {
        float startTime = Time.time;

        while(Time.time < startTime + dashTime)
        {
            moveScript.characterController.Move(moveScript.movimiento * dashSpeed * Time.deltaTime);

            yield return null;
        }
        yield return new WaitForSeconds(timedashCD);
        dashCD = false;
    }
}
