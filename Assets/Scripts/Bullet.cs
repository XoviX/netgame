using System.Collections;
using UnityEngine;

/// <summary>
/// Пуля
/// </summary>
public class Bullet : MonoBehaviour
{
    [SerializeField] private Rigidbody rb;

    [Header("Параметры пули")]
    [Tooltip("Время жизни пули")]
    [SerializeField] private float lifeTime = 3f;

    public void Init(Vector3 velocity)
    {
        rb.velocity = velocity;
        StartCoroutine(DelayDestroy());
    }

    private IEnumerator DelayDestroy()
    {
        yield return new WaitForSecondsRealtime(lifeTime);
        Destroy();
    }

    private void Destroy()
    {
        Destroy(gameObject);
    }

    private void OnCollisionEnter(Collision collision)
    {
        Destroy();
    }
}
