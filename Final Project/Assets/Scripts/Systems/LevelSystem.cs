using System;
public class LevelSystem
{
    public event Action OnExpirienceChanged;
    public event Action OnLevelChanged;

    private int _level;
    private float _expirience;
    private float _expirienceToNextLevel;
    private int _maxLevel = 11;

    public int Level => _level;
    public float Expirience => _expirience;

    public LevelSystem()
    {
        _level = 0;
        _expirience = 0;
        _expirienceToNextLevel = 100;
    }

    public void AddExpirience(float amount) //
    {
        if (_level < _maxLevel)
        {
            _expirience += amount;
            OnExpirienceChanged?.Invoke();
        }        
        if (_expirience >= _expirienceToNextLevel && _level < _maxLevel)
        {
            _level++;
            OnLevelChanged?.Invoke();
            _expirience -= _expirienceToNextLevel;
        }
    }    
    public float GetExpiriencePercent()
    {
        return (float)_expirience / _expirienceToNextLevel;
    }
}