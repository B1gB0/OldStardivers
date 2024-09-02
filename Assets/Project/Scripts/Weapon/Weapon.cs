using System;
using System.Collections;
using System.Collections.Generic;
using Build.Game.Scripts.ECS.EntityActors;
using Project.Game.Scripts;
using UnityEngine;

public abstract class Weapon : MonoBehaviour
{
    public abstract void Shoot();
}