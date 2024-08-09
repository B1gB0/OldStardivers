using Build.Game.Scripts.ECS.Components;
using Leopotam.Ecs;

namespace Build.Game.Scripts.ECS.System
{
    public class PlayerAttackSystem : IEcsRunSystem
    {
        private readonly EcsFilter<PlayerComponent, MovableComponent, AttackComponent> _attackFilter;
        
        public void Run()
        {
            
        }
    }
}