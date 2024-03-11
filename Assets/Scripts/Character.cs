using UnityEngine;

public abstract class Character : MonoBehaviour
{
    [Tooltip("�������� ����������� (max)")]
    [field:SerializeField] public float Speed { get; protected set; } = 2f;
    public Vector3 Velocity { get; protected set; }
}
