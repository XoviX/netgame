using UnityEngine;

public class GunAnimation : MonoBehaviour
{
    private const string SHOOT = "Shoot";

    [SerializeField] private Gun gun;
    [SerializeField] private Animator animator;

    // Start is called before the first frame update
    void Start()
    {
        gun.shoot += OnShoot;
    }

    private void OnShoot()
    {
        animator.SetTrigger(SHOOT);
    }

    void OnDestroy()
    {
        gun.shoot -= OnShoot;
    }
}
