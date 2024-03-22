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
        Isruning = false;
        CanRun = true;
    }
    private void Update()
    {
        transform.position = transform.position + speed * velocity;

        if (Isruning == true && speed != Vector3.zero)
        {
            CurrentStamina -= Time.deltaTime * StaminaMultiplierRester;
        }
        else
        {
            //ACA SE RECUPERA LA ESTAMINA
        }
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        
    }
    public void movement(InputAction.CallbackContext context)
    {
        speed = context.ReadValue<Vector3>();
    }
    public void run(InputAction.CallbackContext context)
    {
        if(CurrentStamina >= 0)
        {
            if (context.performed)
            {
                Isruning = true;
                velocity *= 2;
            }
            else if (context.canceled)
            {
                velocity /= 2;
                Isruning = false;
            }
        }
    }
    public IEnumerator MoreStaminaLapse()
    {
        yield return new WaitForSeconds(10);
        Debug.Log("wasaaaaaaaaaaaa");
    }
}