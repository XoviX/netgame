using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterAnimation : MonoBehaviour
{
    private const string GROUNDED = "Grounded";
    private const string SPEED = "Speed";

    [SerializeField] private Animator animator;
    [SerializeField] private CheckFly checkFly;
    [SerializeField] private Character character;

    // Update is called once per frame
    void Update()
    {
        // получение локальной скорости
        Vector3 localVelocity = character.transform.InverseTransformVector(character.Velocity);
        float speed = localVelocity.magnitude / character.Speed;

        animator.SetFloat(SPEED, speed * Mathf.Sign(localVelocity.z));
        animator.SetBool(GROUNDED, !checkFly.isFly);
    }
}
