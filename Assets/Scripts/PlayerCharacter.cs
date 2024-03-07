using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerCharacter : MonoBehaviour
{
    [SerializeField] private Rigidbody rb;

    [SerializeField] private float speed = 1f;
    [SerializeField] private float inputH = 1f;
    [SerializeField] private float inputV = 1f;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    void FixedUpdate()
    {
        Move();
    }

    private void Move()
    {
        // for Update
        //Vector3 direction = new Vector3(inputH, 0, inputV).normalized;
        //transform.position += direction * speed * Time.deltaTime;

        Vector3 velocity = (transform.forward * inputV + transform.right * inputH).normalized * speed;
        rb.velocity = velocity;
    }

    public void SetInput(float h, float v)
    {
        inputH = h;
        inputV = v;
    }

    public void GetMoveInfo(out Vector3 position, out Vector3 velocity)
    {
        position = transform.position;
        velocity = rb.velocity;
    }
}
