using System;
using UnityEngine;

public abstract class Gun : MonoBehaviour
{
    [SerializeField] protected Bullet bulletPrefab;
    public Action shoot;
}
