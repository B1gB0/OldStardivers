using UnityEngine;
using UnityEngine.Serialization;
using UnityEngine.UI;

namespace Project.Game.Scripts
{
    public class WeaponFactory : MonoBehaviour
    {
        private const string Gun = nameof(Gun);
        private const string Mines = nameof(Mines);
        private const string FragGrenades = nameof(FragGrenades);
        
        [SerializeField] private Gun _gunTemplate;
        [SerializeField] private Mines _minesTemplate;
        [FormerlySerializedAs("_grenadesTemplate")] [SerializeField] private FragGrenades fragGrenadesTemplate;
        
        private ClosestEnemyDetector _enemyDetector;
        private WeaponHolder _weaponHolder;
        private Button _minesButton;
        private Transform _player;
        
        private void CreateGun()
        {
            Gun gun = Instantiate(_gunTemplate, _player);
            gun.Construct(_enemyDetector);
            _weaponHolder.AddWeapon(gun);
        }

        private void CreateMines()
        {
            Vector3 position = new Vector3(_player.position.x, 0f, _player.position.z);
            Mines mines = Instantiate(_minesTemplate, _player);
            mines.transform.position = position;
            mines.Construct(_minesButton);
            _weaponHolder.AddWeapon(mines);
        }

        private void CreateFragGrenades()
        {
            FragGrenades fragGrenades = Instantiate(fragGrenadesTemplate, _player);
            fragGrenades.Construct(_enemyDetector);
            _weaponHolder.AddWeapon(fragGrenades);
        }

        public void CreateWeapon(string weaponType)
        {
            switch (weaponType)
            {
                case Gun :
                    CreateGun();
                    break;
                case Mines :
                    CreateMines();
                    break;
                case FragGrenades : 
                    CreateFragGrenades();
                    break;
            }
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