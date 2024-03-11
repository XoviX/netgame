using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyGun : Gun
{
    // Start is called before the first frame update
    public void Shoot(Vector3 position, Vector3 velocity)
    {
        Instantiate(bulletPrefab, position, Quaternion.identity).Init(velocity);
        shoot?.Invoke();
    }
}
