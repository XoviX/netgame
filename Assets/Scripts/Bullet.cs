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
    private int damage = 0;

    public void Init(Vector3 velocity, int damage = 0)
    {
        this.damage = damage;
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
       if (collision.collider.TryGetComponent<EnemyCharacter>(out EnemyCharacter enemy))
        {
            enemy.ApplyDamage(damage);
        }

        Destroy();
    }
}
