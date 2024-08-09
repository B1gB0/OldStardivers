using System.Collections;
using System.Collections.Generic;
using Build.Game.Scripts.ECS.Data;
using Build.Game.Scripts.ECS.Data.SO;
using Project.Scripts.ECS.Data;
using UnityEngine;

public class DataFactory
{
    private readonly string _playerData = "SO/PlayerData";
    private readonly string _firstLevel = "SO/FirstLevel";
    private readonly string _enemyData = "SO/EnemyData";
    private readonly string _stoneData = "SO/StoneData";
    
    public PlayerInitData CreatePlayerData()
    {
        return Resources.Load<PlayerInitData>(_playerData);
    }
    
    public LevelInitData CreateLevelData()
    {
        return Resources.Load<LevelInitData>(_firstLevel);
    }
    
    public EnemyInitData CreateEnemyData()
    {
        return Resources.Load<EnemyInitData>(_enemyData);
    }
    
    public StoneInitData CreateStoneData()
    {
        return Resources.Load<StoneInitData>(_stoneData);
    }
}
