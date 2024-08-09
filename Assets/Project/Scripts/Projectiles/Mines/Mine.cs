using System;
using System.Collections;
using System.Collections.Generic;
using Build.Game.Scripts.ECS.EntityActors;
using UnityEngine;

public class Mine : MonoBehaviour
{
    [field: SerializeField] public float Damage { get; private set; }

    [field: SerializeField] public float LifeTime { get; private set; } = 4f;
    
    [field: SerializeField] public ParticleSystem ExplosionEffect { get; private set; }

    private void OnEnable()
    {
        StartCoroutine(LifeRoutine());
    }
    
    private void OnTriggerEnter(Collider collision)
    {
        if(collision.gameObject.TryGetComponent(out EnemyActor enemy))
        {
            enemy.Health.TakeDamage(Damage);
            ExplosionEffect.Play();
            gameObject.SetActive(false);
        }
    }

    private void OnDisable()
    {
        StopCoroutine(LifeRoutine());
    }

    private IEnumerator LifeRoutine()
    {
        yield return new WaitForSeconds(LifeTime);
        
        gameObject.SetActive(false);
    }
}
