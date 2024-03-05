using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Colyseus;

public class MultiplayerManager : ColyseusManager<MultiplayerManager>
{
    private ColyseusRoom<State> room;

    protected override void Awake()
    {
        base.Awake();

        Instance.InitializeClient();
        Connect();
    }

    private async void Connect()
    {
        room = await Instance.client.JoinOrCreate<State>("state_handler");
    }

    protected override void OnDestroy()
    {
        room.Leave();
    }


}
