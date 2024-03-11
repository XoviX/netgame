using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Controller : MonoBehaviour
{
    [SerializeField] private PlayerCharacter player;
    [SerializeField] private PlayerGun gun;
    [SerializeField] private float mouseSensetivity = 1f;

    private MultiplayerManager multiplayerManager;

    private void Start()
    {
        multiplayerManager = MultiplayerManager.Instance;
    }


    void Update()
    {
        float h = Input.GetAxisRaw("Horizontal");
        float v = Input.GetAxisRaw("Vertical");

        float mouseX = Input.GetAxis("Mouse X");
        float mouseY = Input.GetAxis("Mouse Y");

        bool isShoot = Input.GetMouseButton(0);

        bool space = Input.GetKeyDown(KeyCode.Space);

        player.SetInput(h, v, mouseX);
        player.RotateX(-mouseY * mouseSensetivity);
        if (space)
        {
            player.Jump();
        }

        if (isShoot)
        {
            if (gun)
            {
                if (gun.TryShoot(out ShootInfo shootInfo))
                {
                    SendShoot(ref shootInfo);
                }
            }
        }

        SendMove();
    }

    private void SendMove()
    {
        player.GetMoveInfo(out Vector3 position, out Vector3 velocity, out Vector3 rotate);
        Dictionary<string, object> data = new Dictionary<string, object>()
        {
            {"pX", position.x},
            {"pY", position.y},
            {"pZ", position.z},
            {"vX", velocity.x},
            {"vY", velocity.y},
            {"vZ", velocity.z},
            {"rX", rotate.x},
            {"rY", rotate.y},
            {"rZ", rotate.z},
        };
        multiplayerManager.SendMessage("move", data);
    }

    private void SendShoot(ref ShootInfo shootInfo)
    {
        shootInfo.key = multiplayerManager.GetSessionId();
        string json = JsonUtility.ToJson(shootInfo);
        multiplayerManager.SendMessage("shoot", json);
    }
}

[System.Serializable]
public struct ShootInfo
{
    public string key;
    public float pX;
    public float pY;
    public float pZ;
    public float dX;
    public float dY;
    public float dZ;
}