using Colyseus.Schema;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

public class PlayerCharacter : Character
{
    [SerializeField] private Health health;

    [SerializeField] private Rigidbody rb;
    [Tooltip("Башка зайца")]
    [SerializeField] private Transform head;
    [Tooltip("Точка привязки камеры")]
    [SerializeField] private Transform cameraPoint;

    [Space(5)]
    [Tooltip("Минимальный угол наклона")]
    [SerializeField] private float minHeadAngle = -90f;
    [Tooltip("Максимальный угол наклона")]
    [SerializeField] private float maxHeadAngle = 90f;
    private float currentRotateX = 0f;

    private float inputH = 1f;
    private float inputV = 1f;

    [Space(5)]
    [Tooltip("Сила прыжка")]
    [SerializeField] private float jumpForce = 10f;
    [Tooltip("Время последнего прыжка")]
    [SerializeField] private float jumpTime = 0f;
    [Tooltip("Задержка прыжка")]
    [SerializeField] private float jumpDelay = 0.15f;
    [Tooltip("Признак прыжка")]
    [SerializeField] private CheckFly checkFly;

    private float inputRotateY = 0f;

    public void Jump()
    {
        if (checkFly.isFly)
            return;
        if (Time.time - jumpTime < jumpDelay)
            return;

        jumpTime = Time.time;
        rb.AddForce(0, jumpForce, 0, ForceMode.VelocityChange);
    }

    private void Start()
    {
        Transform camera = Camera.main.transform;
        camera.parent = cameraPoint;
        camera.localPosition = Vector3.zero;
        camera.localRotation = Quaternion.identity;

        health.SetMax(MaxHealth);
        health.SetCurrent(MaxHealth);
    }

    /// <summary>
    /// Поворот головы
    /// </summary>
    /// <param name="value"></param>
    public void RotateX(float value)
    {
        //head.Rotate(value, 0, 0);

        currentRotateX = Mathf.Clamp(currentRotateX + value, minHeadAngle, maxHeadAngle);
        head.localEulerAngles = new Vector3(currentRotateX, 0, 0);
    }

    void FixedUpdate()
    {
        Move();
        RotateY();
    }

    private void Move()
    {
        // for Update
        //Vector3 direction = new Vector3(inputH, 0, inputV).normalized;
        //transform.position += direction * speed * Time.deltaTime;

        Vector3 velocity = (transform.forward * inputV + transform.right * inputH).normalized * Speed;
        velocity.y = rb.velocity.y;
        Velocity = velocity;
        rb.velocity = Velocity;
    }

    public void SetInput(float h, float v, float rotateY)
    {
        inputH = h;
        inputV = v;
        inputRotateY += rotateY;
    }

    private void RotateY()
    {
        rb.angularVelocity = new Vector3(0, inputRotateY, 0);
        inputRotateY = 0;
    }

    public void GetMoveInfo(out Vector3 position, out Vector3 velocity, out Vector3 rotate)
    {
        position = transform.position;
        velocity = rb.velocity;
                                // вверх-вниз               вправо-влево
        rotate = new Vector3(head.localEulerAngles.x, transform.eulerAngles.y, 0);
    }

    internal void OnChange(List<DataChange> changes)
    {
        foreach (var dataChange in changes)
        {
            switch (dataChange.Field)
            {
                case "currentHP":
                    health.SetCurrent((sbyte)dataChange.Value);
                    break;
                default:
                    Debug.Log("Не обрабатывается изменение поля: " + dataChange.Field);
                    break;
            }
        }
    }
}
