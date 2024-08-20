using System;
using Project.Scripts.Score;
using UnityEngine;
using Zenject;

namespace Build.Game.Scripts.ECS.EntityActors
{
    public class StoneActor : ScoreActor
    {
        [field: SerializeField] public Health Health{ get; private set; }

        [field: SerializeField] public Animator Animator { get; private set; }

        private Score _score;

        public void Construct(Score score)
        {
            _score = score;
        }

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
            _score.OnKill(this);
            gameObject.SetActive(false);
        }

        public override void Accept(IActorVisitor visitor)
        {
            visitor.Visit(this);
        }
    }
}