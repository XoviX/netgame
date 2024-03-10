using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterAnimation : MonoBehaviour
{
    private const string GROUNDED = "Grounded";
    private const string SPEED = "Speed";

    [SerializeField] private Animator animator;
    [SerializeField] private CheckFly checkFly;
    [SerializeField] private Rigidbody rb;
    [SerializeField] private float maxSpeed = 5f;

    // Update is called once per frame
    void Update()
    {
        // получение локальной скорости
        Vector3 localVelocity = rb.transform.InverseTransformVector(rb.velocity);
        float speed = localVelocity.magnitude / maxSpeed;

        animator.SetFloat(SPEED, speed * Mathf.Sign(localVelocity.z));
        animator.SetBool(GROUNDED, !checkFly.isFly);
    }
}
