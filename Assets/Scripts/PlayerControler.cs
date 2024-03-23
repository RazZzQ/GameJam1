using System;
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
    [SerializeField] bool Isrunning;
    [SerializeField] float StaminaMultiplierAdded;
    [SerializeField] bool CanRun;
    [SerializeField] float Timer;
    public event Action StaminaZero;
    Vector3 speed;

    private void OnEnable()
    {
        StaminaZero += activateMoreStaminaLapse;
    }
    private void OnDisable()
    {
        StaminaZero -= activateMoreStaminaLapse;
    }
    // Start is called before the first frame update
    void Start()
    {
        MaxStamina = 100;
        CurrentStamina = MaxStamina;
        StaminaMultiplierRester = 15;
        StaminaMultiplierAdded = 15;
        Isrunning = false;
        CanRun = true;
    }
    private void Update()
    {
        transform.position = transform.position + speed * velocity;

        if (Isrunning == true && speed != Vector3.zero)
        {
            CurrentStamina -= Time.deltaTime * StaminaMultiplierRester;

            if (CurrentStamina <= 0)
            {
                Isrunning = false;
                CanRun = false;
                velocity /= 2; // Reduce la velocidad a la normal.
                StaminaZero?.Invoke();
            }
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
                CanRun = true;
                Isrunning = true;
                velocity *= 2;
            }
            else if (context.canceled)
            {
                velocity /= 2;
                Isrunning = false;
                CanRun=false;
            }
        }
    }
    void activateMoreStaminaLapse()
    {
        if (CurrentStamina <= 0)
        {
            StartCoroutine(MoreStaminaLapse());
        }
    }
    public IEnumerator MoreStaminaLapse()
    {
        yield return new WaitForSeconds(3);
        CurrentStamina = CurrentStamina + 0.01f;   
    }
}