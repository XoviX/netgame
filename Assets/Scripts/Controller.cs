using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Controller : MonoBehaviour
{
    [SerializeField] private PlayerCharacter player;

    // Update is called once per frame
    void Update()
    {
        float h = Input.GetAxisRaw("Horizontal");
        float v = Input.GetAxisRaw("Vertical");

        player.SetInput(h, v);

        SendMove();
    }

    private void SendMove()
    {
        player.GetMoveInfo(out Vector3 position);
        Dictionary<string, object> data = new Dictionary<string, object>()
        {
            {"x", position.x},
            {"y", position.z}
        };
        MultiplayerManager.Instance.SendMessage("move", data); 
    }
}
