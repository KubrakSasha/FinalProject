using System;

public class LevelSystem
{
    public event Action OnExpirienceChanged;
    public event Action OnLevelChanged;

    private int _level;
    private int _expirience;
    private int _expirienceToNextLevel;

    public LevelSystem()
    {
        _level = 0;
        _expirience = 0;
        _expirienceToNextLevel = 100;
    }

    public void AddExpirience(int amount) 
    {
        _expirience += amount;
        OnExpirienceChanged?.Invoke();
        if (_expirience >= _expirienceToNextLevel)
        {
            _level++;
            OnLevelChanged?.Invoke();
            _expirience -= _expirienceToNextLevel;
        }
    }
    public int GetLevel() 
    {
        return _level;
    }
    public int GetExpirience()
    {
        return _expirience;
    }
    public float GetExpiriencePercent()
    {
        return (float)_expirience / _expirienceToNextLevel;
    }

}