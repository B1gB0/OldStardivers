using System;
using Build.Game.Scripts;
using UnityEngine;

namespace Project.Scripts.Score
{
    public class Score
    {
        private readonly ActorVisitor _actorVisitor = new ();

        private int Value => _actorVisitor.AccumulatedScore;
        
        private int MaxValue => _actorVisitor.MaxScore;
        

        public event Action<int, int> ValueChanged;

        public void OnKill(ScoreActor scoreActor)
        {
            scoreActor.Accept(_actorVisitor);
            ValueChanged?.Invoke(Value, MaxValue);
        }
    }
}