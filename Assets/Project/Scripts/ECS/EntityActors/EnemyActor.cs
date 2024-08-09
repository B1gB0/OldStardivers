using System;
using UnityEngine;

namespace Build.Game.Scripts.ECS.EntityActors
{
    public class EnemyActor : MonoBehaviour
    {
        [field: SerializeField] public Health Health{ get; private set; }
        
        [field: SerializeField] public Animator Animator { get; private set; }

        [field: SerializeField] public Rigidbody Rigidbody { get; private set; }
        
        private void OnEnable()
        {
            Health.Die += Die;
        }

        private void OnDisable()
        {
            Health.Die -= Die;
        }

        private void Die()
        {
            gameObject.SetActive(false);
        }
    }
}