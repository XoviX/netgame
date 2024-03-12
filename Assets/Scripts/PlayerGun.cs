using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerGun : Gun
{
    [SerializeField] private Transform bulletPoint;

    [Header("��������� ������")]
    [Tooltip("�������� ����")]
    public float bulletSpeed = 100f;
    [Tooltip("����������������")]
    public float rateOfFire = 5f;
    [Tooltip("���� ����")]
    [SerializeField] private int damage = 10;

    private float lastShootTime = 0;

    public bool TryShoot(out ShootInfo info)
    {
        info = new ShootInfo();

        if (Time.time - lastShootTime < 1 / rateOfFire)
            return false;

        Vector3 position = bulletPoint.position;
        Vector3 velocity = bulletPoint.forward * bulletSpeed;

        lastShootTime = Time.time;
        Instantiate(bulletPrefab, position, bulletPoint.rotation).Init(velocity, damage);
        shoot?.Invoke();

        info.pX = position.x;
        info.pY = position.y;
        info.pZ = position.z;
        info.dX = velocity.x;
        info.dY = velocity.y;
        info.dZ = velocity.z;

        return true;
    }

}
