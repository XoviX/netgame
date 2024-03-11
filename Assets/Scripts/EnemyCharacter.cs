using UnityEngine;

public class EnemyCharacter : Character
{
    [SerializeField] private Transform head;
    // предсказанная позиция
    public Vector3 targetPosition { get; private set; } = Vector3.zero;
    // длинна 
    private float velocityMagnitude;

    private void Start()
    {
        targetPosition = transform.position;
    }

    public void SetSpeed(float value) => Speed = value;

    public void SetRotateX(float value)
    {
        head.localEulerAngles = new Vector3(value, 0, 0);
    }

    public void SetRotateY(float value)
    {
        transform.localEulerAngles = new Vector3(0, value, 0);
    }

    private void Update()
    {
        if (velocityMagnitude > 0.1f)
        {
            transform.position = Vector3.MoveTowards(transform.position, targetPosition, velocityMagnitude * Time.deltaTime);
        }
        else
        {
            transform.position = targetPosition;
        }
    }

    /// <summary>
    /// 
    /// </summary>
    /// <param name="position"></param>
    /// <param name="velocity"></param>
    /// <param name="averageInterval"></param>
    public void SetMovement(in Vector3 position, in Vector3 velocity, in float averageInterval)
    {
        targetPosition = position + velocity * averageInterval;
        velocityMagnitude = velocity.magnitude;

        Velocity = velocity;
    }
}
