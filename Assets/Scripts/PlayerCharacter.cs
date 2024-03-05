using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerCharacter : MonoBehaviour
{
    [SerializeField] private float speed = 1f;
    [SerializeField] private float inputH = 1f;
    [SerializeField] private float inputV = 1f;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
    

        Move();
    }

    private void Move()
    {
        Vector3 direction = new Vector3(inputH, 0, inputV).normalized;
        transform.position += direction * speed * Time.deltaTime;
    }

    public void SetInput(float h, float v)
    {
        inputH = h;
        inputV = v;
    }
}
