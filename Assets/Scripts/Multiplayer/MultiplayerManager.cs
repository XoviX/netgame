using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Colyseus;
using System;
using static UnityEngine.EventSystems.EventTrigger;

public class MultiplayerManager : ColyseusManager<MultiplayerManager>
{
    #region Variables
    [SerializeField] private PlayerCharacter playerPrefab;
    [SerializeField] private EnemyController enemyPrefab;

    private ColyseusRoom<State> room;

    private Dictionary<string, EnemyController> enemys = new Dictionary<string, EnemyController>();
    #endregion

    protected override void Awake()
    {
        base.Awake();

        Instance.InitializeClient();
        Connect();
    }

    private async void Connect()
    {
        Dictionary<string, object> data = new Dictionary<string, object>()
        {
            { "hp", playerPrefab.MaxHealth },
            { "speed", playerPrefab.Speed }
        };

        room = await Instance.client.JoinOrCreate<State>("netgame_room", data);

        room.OnStateChange += OnChange;
        room.OnMessage<string>("Shoot", ApplyShoot);
    }

    private void ApplyShoot(string json)
    {
        ShootInfo shootInfo = JsonUtility.FromJson<ShootInfo>(json);

        if (enemys.ContainsKey(shootInfo.key)) 
        {
            enemys[shootInfo.key].Shoot(shootInfo);
        } 
        else
        {
            Debug.LogError("Выстрел незарегистрированного врага");
        }
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
        var position = new Vector3(player.pX, player.pY, player.pZ);

        Instantiate(playerPrefab, position, Quaternion.identity);
    }

    private void CreateEnemy(string key, Player player)
    {
        var position = new Vector3(player.pX, player.pY, player.pZ);

        var enemy = Instantiate(enemyPrefab, position, Quaternion.identity);
        enemy.Init(player);

        enemys.Add(key, enemy);
    }

    private void RemoveEnemy(string key, Player player)
    {
        if (!enemys.ContainsKey(key))
            return;

        enemys[key].Destroy();

        enemys.Remove(key);
    }


    protected override void OnDestroy()
    {
        room.Leave();
    }

    /// <summary>
    /// Сообщение на сервер
    /// </summary>
    /// <param name="key">ключ</param>
    /// <param name="data">словарь</param>
    public void SendMessage(string key, Dictionary<string, object> data)
    {
        room.Send(key, data);
    }

    /// <summary>
    /// Сообщение на сервер
    /// </summary>
    /// <param name="key">ключ</param>
    /// <param name="data">строка</param>
    public void SendMessage(string key, string data)
    {
        room.Send(key, data);
    }

    public string GetSessionId() => room.SessionId;
}
