using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Colyseus;
using System;
using static UnityEngine.EventSystems.EventTrigger;

public class MultiplayerManager : ColyseusManager<MultiplayerManager>
{
    [SerializeField] private GameObject playerPrefab;
    [SerializeField] private GameObject enemyPrefab;
    private EnemyController enemy;

    private ColyseusRoom<State> room;

    protected override void Awake()
    {
        base.Awake();

        Instance.InitializeClient();
        Connect();

        enemy = enemyPrefab.GetComponent<EnemyController>();
    }

    private async void Connect()
    {
        room = await Instance.client.JoinOrCreate<State>("state_handler");

        room.OnStateChange += OnChange;

    }

    private void OnChange(State state, bool isFirstState)
    {
        if (!isFirstState) return;

        // state - состояние нашей комнаты
        // isFirstState - подключились впервые (для сессии)

        state.players.ForEach((key, player) =>
        {
            if (key == room.SessionId)
                CreatePlayer(player);
            else
                CreateEnemy(key, player);
        });

        // к комнате подключился новый игрок
        room.State.players.OnAdd += CreateEnemy;

        // от комнады отключился игрок
        room.State.players.OnRemove += RemoveEnemy;
    }

    private void CreatePlayer(Player player)
    {
        var position = new Vector3(player.x, 0, player.y);

        Instantiate(playerPrefab, position, Quaternion.identity);
    }

    private void CreateEnemy(string key, Player player)
    {
        var position = new Vector3(player.x, 0, player.y);

        var newEnemy = Instantiate(enemy, position, Quaternion.identity);
        player.OnChange += newEnemy.OnChange;
    }

    private void RemoveEnemy(string key, Player value)
    {
        //throw new NotImplementedException();
    }


    protected override void OnDestroy()
    {
        room.Leave();
    }

    /// <summary>
    /// Сообщение на сервер
    /// </summary>
    public void SendMessage(string key, Dictionary<string, object> data)
    {
        room.Send(key, data);
    }
}
