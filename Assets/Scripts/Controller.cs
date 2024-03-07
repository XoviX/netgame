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
        player.GetMoveInfo(out Vector3 position, out Vector3 velocity);
        Dictionary<string, object> data = new Dictionary<string, object>()
        {
            {"pX", position.x},
            {"pY", position.y},
            {"pZ", position.z},
            {"vX", velocity.x},
            {"vY", velocity.y},
            {"vZ", velocity.z}
        };
        MultiplayerManager.Instance.SendMessage("move", data); 
    }
}
