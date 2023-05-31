using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Movement : MonoBehaviour
{
    [SerializeField] float fSense;
    [SerializeField] float fSpeed;
    [SerializeField] float fSprintMod;
    [SerializeField] float fMaxAngle;
    [SerializeField] Light flashlight;
    [SerializeField] GameObject camHolder;

    private AudioSource audi;
    private float fCurrentSpeed;
    private float fRotationX, fRotationY;
    private Rigidbody rig;
    private bool bFlashlight = true;

    void Start()
    {
        rig = GetComponent<Rigidbody>();
        audi = GetComponent<AudioSource>();
    }

    // Update is called once per frame
    void Update()
    {
        CalculateSpeed();
        Rotation();
        Move();
        SetFlashlight();

        Cursor.visible = false;
        Cursor.lockState = CursorLockMode.Locked;
    }

    private void Rotation()
    {
        float fMouseX = Input.GetAxisRaw("Mouse X") * fSense * Time.deltaTime;
        float fMouseY = Input.GetAxisRaw("Mouse Y") * fSense * Time.deltaTime;

        fRotationY += fMouseX;

        fRotationX -= fMouseY;
        fRotationX = Mathf.Clamp(fRotationX, -fMaxAngle, fMaxAngle);

        camHolder.transform.rotation = Quaternion.Euler(new Vector3(fRotationX, fRotationY, 0));
        transform.rotation = Quaternion.Euler(new Vector3(0, fRotationY, 0));
    }

    private void Move()
    {
        float fHorizontalInput = Input.GetAxisRaw("Horizontal");
        float fVerticalInput = Input.GetAxisRaw("Vertical");

        Vector3 move = transform.forward * fVerticalInput + transform.right * fHorizontalInput;

        rig.AddForce(move.normalized * fCurrentSpeed * Time.deltaTime * 10f, ForceMode.Force);
    }

    private void CalculateSpeed()
    {
        if (Input.GetKey(KeyCode.LeftShift))
        {
            fCurrentSpeed = fSpeed * fSprintMod;
        }
        else
        {
            fCurrentSpeed = fSpeed;
        }
    }

    private void SetFlashlight()
    {
        if (Input.GetKeyDown(KeyCode.F))
        {
            bFlashlight = !bFlashlight;

            flashlight.enabled = bFlashlight;

            audi.Play();
        }
    }
}
