using UnityEngine;

namespace Project.Game.Scripts
{
    public class WeaponFactory : MonoBehaviour
    {
        [SerializeField] private Gun gunTemplate;
        
        private ClosestEnemyDetector _enemyDetector;
        private WeaponHolder _weaponHolder;
        private Transform _player;
        
        public Gun CreateGun()
        {
            Gun gun = Instantiate(gunTemplate, _player);
            gun.Construct(_enemyDetector);
            _weaponHolder.AddWeapon(gun);
            
            return gun;
        }

        public void GetData(ClosestEnemyDetector enemyDetector, Transform player, WeaponHolder weaponHolder)
        {
            _enemyDetector = enemyDetector;
            _weaponHolder = weaponHolder;
            _player = player;
        }
    }
}