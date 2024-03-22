using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.InputSystem;


public class PlayerControler : MonoBehaviour
{
    [SerializeField] float MaxStamina;
    [SerializeField] float CurrentStamina;
    [SerializeField] float StaminaMultiplierRester;
    [SerializeField] float velocity;
    [SerializeField] bool Isruning;
    [SerializeField] bool CanRun;
    Vector3 speed;

    // Start is called before the first frame update
    void Start()
    {
        MaxStamina = 100;
        CurrentStamina = MaxStamina;
        StaminaMultiplierRester = 15;
        CanRun = true;
    }
    private void Update()
    {
        if(CurrentStamina < 0)
        {
            CanRun = false;
        }
        else
        {
            CanRun = true;
        }
        if (Isruning == true)
        {
            CurrentStamina -= Time.deltaTime * StaminaMultiplierRester;
        }
        else{
            StartCoroutine(MoreStaminaLapse());
        }
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        transform.position = transform.position + speed * velocity;

    }
    public void movement(InputAction.CallbackContext context)
    {
        speed = context.ReadValue<Vector3>();
    }
    public void run(InputAction.CallbackContext context)
    {
        if (context.performed && CanRun == true)
        {
            if(CurrentStamina >= 0)
            {
                Isruning = true;
                velocity *= 2;
            }
        }
        else if (context.canceled)
        {
            if(CanRun == false) 
            {
                velocity /= 2;
            }
            Isruning = false;
        }
        
    }
    public IEnumerator MoreStaminaLapse()
    {
        yield return new WaitForSeconds(10);
        Debug.Log("wasaaaaaaaaaaaa");
    }

}
