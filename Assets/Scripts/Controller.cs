using System;
using UnityEngine;
using UnityEngine.InputSystem;

public class Controller : MonoBehaviour
{
    public Camera _mainCamera;
    public Transform _cameraPosition;
    CharacterController _controller;
    Weapon _weapon;

    [Header("Control Settings")]
    public float playerSpeed = 5.0f;
    public float runningSpeed = 7.5f;
    public float mouseSensitivity = 10.0f;

    float m_VerticalAngle, m_HorizontalAngle;

    void Start()
    {
        m_VerticalAngle = 0.0f;
        m_HorizontalAngle = transform.localEulerAngles.y;

        _mainCamera.transform.SetParent(_cameraPosition, false);
        _mainCamera.transform.localPosition = Vector3.zero + new Vector3(0.0f, 0.6f, 0.0f);
        _mainCamera.transform.localRotation = Quaternion.identity;

        _controller = GetComponent<CharacterController>();
        _weapon = GetComponent<Weapon>();
    }

    void Update()
    {
        // Move around with ZQSD
        Vector3 move = new Vector3(Input.GetAxis("Horizontal"), 0, Input.GetAxisRaw("Vertical"));
        if (move.sqrMagnitude > 1.0f)
            move.Normalize();

        // float usedSpeed = m_Grounded ? actualSpeed : m_SpeedAtJump;
        bool running = Input.GetButton("Run");
        if (running) Debug.Log("Character is running");
        float speed = running ? runningSpeed : playerSpeed;
        move = transform.TransformDirection(move);
        _controller.Move(move * Time.deltaTime * speed);

        // Turn player
        float turnPlayer = Input.GetAxis("Mouse X") * 10.0f;
        m_HorizontalAngle += turnPlayer;

        if (m_HorizontalAngle > 360) m_HorizontalAngle -= 360.0f;
        if (m_HorizontalAngle < 0) m_HorizontalAngle += 360.0f;

        Vector3 currentAngles = transform.localEulerAngles;
        currentAngles.y = m_HorizontalAngle;
        transform.localEulerAngles = currentAngles;

        // Camera look up/down
        var turnCam = -Input.GetAxis("Mouse Y");
        turnCam *= mouseSensitivity;
        m_VerticalAngle = Mathf.Clamp(turnCam + m_VerticalAngle, -89.0f, 89.0f);
        currentAngles = _cameraPosition.transform.localEulerAngles;
        currentAngles.x = m_VerticalAngle;
        _cameraPosition.transform.localEulerAngles = currentAngles;

        // Aiming
        Quaternion quat = Quaternion.identity;
        quat.eulerAngles = new Vector3(_cameraPosition.transform.localEulerAngles.x, transform.localEulerAngles.y, transform.localEulerAngles.z);
        Vector3 aimDirection = (quat * Vector3.forward).normalized;

        // Recenter cursor position
        Vector2 screenCenter = new(Screen.width / 2f, Screen.height / 2f);
        Vector2 screenCenterFloored = new((float)System.Math.Floor(screenCenter.x), (float)Math.Floor(Screen.height / 2f));

        if (new Vector2(Input.mousePosition.x, Input.mousePosition.y) != screenCenterFloored)
        {
            Mouse.current.WarpCursorPosition(screenCenter);
        }

        // Fire
        if (Input.GetButtonDown("Fire"))
        {
           _weapon.Fire(aimDirection, _cameraPosition.position);
        }
    }
}
