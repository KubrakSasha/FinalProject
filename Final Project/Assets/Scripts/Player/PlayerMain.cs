
public class PlayerMain : Singleton<PlayerMain>
{
    private PlayerStats _stats;
    public PlayerShootingHandler _shootingHandler;
    private PlayerMovementHandler _movementHandler;
    

    public PlayerStats Stats => _stats;
    private void Awake()
    {
        _stats = GetComponent<PlayerStats>();
        _shootingHandler = GetComponent<PlayerShootingHandler>();
        _movementHandler = GetComponent<PlayerMovementHandler>();
    }
}
