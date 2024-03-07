using UnityEngine;

public class EnemyCharacter : MonoBehaviour
{
    // предсказанная позиция
    public Vector3 targetPosition { get; private set; } = Vector3.zero;
    // длинна 
    private float velocityMagnitude;

    private void Start()
    {
        targetPosition = transform.position;
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
    }
}
