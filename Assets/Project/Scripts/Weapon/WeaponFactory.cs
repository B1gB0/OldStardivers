using UnityEngine;
using UnityEngine.Serialization;
using UnityEngine.UI;

namespace Project.Game.Scripts
{
    public class WeaponFactory : MonoBehaviour
    {
        [SerializeField] private Gun _gunTemplate;
        [SerializeField] private Mines _minesTemplate;
        
        private ClosestEnemyDetector _enemyDetector;
        private WeaponHolder _weaponHolder;
        private Button _minesButton;
        private Transform _player;
        
        public void CreateGun()
        {
            Gun gun = Instantiate(_gunTemplate, _player);
            gun.Construct(_enemyDetector);
            _weaponHolder.AddWeapon(gun);
        }

        public void CreateMines()
        {
            Vector3 position = new Vector3(_player.position.x, 0f, _player.position.z);
            Mines mines = Instantiate(_minesTemplate, _player);
            mines.transform.position = position;
            mines.Construct(_minesButton);
            _weaponHolder.AddWeapon(mines);
        }

        public void GetData(ClosestEnemyDetector enemyDetector, Transform player, WeaponHolder weaponHolder)
        {
            _enemyDetector = enemyDetector;
            _weaponHolder = weaponHolder;
            _player = player;
        }

        public void GetMinesButton(Button button)
        {
            _minesButton = button;
        }
    }
}