using System;
using System.Collections;
using System.Collections.Generic;
using Build.Game.Scripts.ECS.EntityActors;
using UnityEngine;

public class ClosestEnemyDetector : MonoBehaviour
{
    public readonly List<EnemyActor> Enemies = new ();

    private float _currentDistanceOfClosestEnemy;
    private float _minValue = 0f;
    
    public EnemyActor СlosestEnemy { get; private set; }

    private void Update()
    {
        SetClosestEnemy();
    }
    
    public void AddEnemy(EnemyActor enemy)
    {
        Enemies.Add(enemy);
    }

    private void SetClosestEnemy()
    {
        float distance = Mathf.Infinity;

        Vector3 position = transform.localPosition;

        foreach (EnemyActor enemy in Enemies)
        {
            if (enemy.Health.Value > _minValue)
            {
                _currentDistanceOfClosestEnemy = Vector3.Distance(position, enemy.transform.localPosition);

                if (_currentDistanceOfClosestEnemy < distance)
                {
                    СlosestEnemy = enemy;
                    distance = _currentDistanceOfClosestEnemy;
                }
            }
        }
    }
}
