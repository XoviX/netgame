# netgame

Всё как в уроке, только в списке MultiplayerManager одни GameObject'ы оставил...

[SerializeField] private GameObject playerPrefab;
[SerializeField] private GameObject enemyPrefab;
private EnemyController enemy;

в Awake() дополнительно...
 enemy = enemyPrefab.GetComponent<EnemyController>();