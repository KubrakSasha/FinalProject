
public class PlayerMain : Singleton<PlayerMain>
{
    private PlayerStats _stats;
    private PlayerShootingHandler _shootingHandler;
    private PlayerMovementHandler _movementHandler;   

    public PlayerStats Stats => _stats;
    public PlayerShootingHandler ShootingHandler => _shootingHandler;
    private void Awake()
    {
        _stats = GetComponent<PlayerStats>();
        _shootingHandler = GetComponent<PlayerShootingHandler>();
        _movementHandler = GetComponent<PlayerMovementHandler>();
    }
}
