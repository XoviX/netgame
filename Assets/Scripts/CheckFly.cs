using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// �������� �����
/// </summary>
public class CheckFly : MonoBehaviour
{
    public bool isFly { get; private set; }

    [Header("��������� ����������")]
    [SerializeField] private LayerMask layerMask;
    [SerializeField] private float radius = 1f;
    [SerializeField] private float coyoteTime = 0.15f;
    private float flyTimer = 0;

    private void Update()
    {
        {
            if (Physics.CheckSphere(transform.position, radius, layerMask))
            {
                isFly = false;
                flyTimer = 0;
            }
            else
            {
                flyTimer += Time.deltaTime;
                if (flyTimer > coyoteTime)
                    isFly = true;
            }
        }
    }

#if UNITY_EDITOR
    private void OnDrawGizmos()
    {
        Gizmos.DrawWireSphere(transform.position, radius);
    }
#endif
}
