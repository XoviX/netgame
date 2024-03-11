using Colyseus.Schema;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static UnityEngine.EventSystems.EventTrigger;

/// <summary>
///  онтроллер врагов
/// </summary>
public class EnemyController : Character
{
    [SerializeField] private EnemyCharacter enemyCharacter;
    [SerializeField] protected EnemyGun gun;

    private List<float> recieveTimeInterval = new List<float> { 0, 0, 0, 0, 0};
    private float averageRecieveInterval
    {
        get
        {
            int count = recieveTimeInterval.Count;
            float sum = 0;
            for (int i = 0; i < count; i++)
            {
                sum += recieveTimeInterval[i];
            }
            return sum/count;
        }
    }
    private float lastRecieveTime = 0f;

    private Player player;

    public void Init(Player player)
    {
        this.player = player;
        enemyCharacter.SetSpeed(player.speed);
        player.OnChange += OnChange;
    }

    public void Shoot(in ShootInfo info)
    {
        Vector3 position = new Vector3(info.pX, info.pY, info.pZ);
        Vector3 velocity = new Vector3(info.dX, info.dY, info.dZ);

        gun.Shoot(position, velocity);
    }

    private void SaveRecieveTime()
    {
        float interval = Time.time - lastRecieveTime;
        lastRecieveTime = Time.time;

        recieveTimeInterval.Add(interval);
        recieveTimeInterval.RemoveAt(0);
    }

    internal void OnChange(List<DataChange> changes)
    {
        SaveRecieveTime();

        Vector3 position = transform.position;      //enemyCharacter.targetPosition;
        Vector3 velocity = enemyCharacter.Velocity; //Vector3.zero

        foreach (var dataChange in changes)
        {
            switch (dataChange.Field)
            {
                case "pX":
                    position.x = (float)dataChange.Value;
                    break;
                case "pY":
                    position.y = (float)dataChange.Value;
                    break;
                case "pZ":
                    position.z = (float)dataChange.Value;
                    break;
                case "vX":
                    velocity.x = (float)dataChange.Value;
                    break;
                case "vY":
                    velocity.y = (float)dataChange.Value;
                    break;
                case "vZ":
                    velocity.z = (float)dataChange.Value;
                    break;
                case "rX":
                    enemyCharacter.SetRotateX((float)dataChange.Value);
                    break;
                case "rY":
                    enemyCharacter.SetRotateY((float)dataChange.Value);
                    break;
                case "rZ":
                    //0
                    break;
                default:
                    Debug.Log("Ќе обрабатываетс€ изменение пол€: " + dataChange.Field);
                    break;
            }
        }

        //было просто перемещение transform.position = position;
        enemyCharacter.SetMovement(position, velocity, averageRecieveInterval);
    }

    public void Destroy() 
    {
        player.OnChange -= OnChange;
        Destroy(gameObject);
    }
}
