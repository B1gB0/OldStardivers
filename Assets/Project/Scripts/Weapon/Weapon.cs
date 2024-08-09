using System;
using System.Collections;
using System.Collections.Generic;
using Build.Game.Scripts.ECS.EntityActors;
using UnityEngine;

public abstract class Weapon : MonoBehaviour
{
    [field: SerializeField] public ClosestEnemyDetector Detector { get; private set; }
    
    public abstract void Shoot();
}