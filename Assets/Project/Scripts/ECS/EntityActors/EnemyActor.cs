using Project.Scripts.Score;
using Project.Scripts.UI;
using UnityEngine;

namespace Build.Game.Scripts.ECS.EntityActors
{
    public class EnemyActor : ScoreActor
    {
        [field: SerializeField] public Health Health{ get; private set; }
        
        [field: SerializeField] public Animator Animator { get; private set; }

        [field: SerializeField] public Rigidbody Rigidbody { get; private set; }

        private Score _score;
        private FloatingDamageTextPresenter _damageTextPresenter;

        public void Construct(Score score, FloatingDamageTextPresenter damageTextPresenter)
        {
            _score = score;
            _damageTextPresenter = damageTextPresenter;
            Health.IsDamaged += _damageTextPresenter.OnChangedDamageText;
        }

        private void OnEnable()
        {
            Health.Die += Die;
        }

        private void OnDisable()
        {
            Health.Die -= Die;
        }
        
        public override void Accept(IActorVisitor visitor)
        {
            visitor.Visit(this);
        }

        private void Die()
        {
            Health.IsDamaged -= _damageTextPresenter.OnChangedDamageText;
            _score.OnKill(this);
            gameObject.SetActive(false);
        }
    }
}