using Build.Game.Scripts.ECS.EntityActors;
using UnityEngine;

public class AssaultRifleBullet : Bullet
{
    private void OnTriggerEnter(Collider collision)
    {
        if(collision.gameObject.TryGetComponent(out EnemyActor enemy))
        {
            enemy.Health.TakeDamage(Damage);
            gameObject.SetActive(false);
        }
    }
}
