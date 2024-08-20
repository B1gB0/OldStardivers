using UnityEngine;

namespace Project.Game.Scripts
{
    public class Mines : Weapon
    {
        [field: SerializeField] public int CountMines { get; private set; }
        
        [SerializeField] private Mine _prefab;
        [SerializeField] private float _delay;
        [SerializeField] private bool _isAutoExpandPool = false;
        [SerializeField] private Transform _installPoint;
        
        private ObjectPool<Mine> _pool;

        private float _lastShotTime;
        private float _minValue = 0f;

        private void Awake()
        {
            _pool = new ObjectPool<Mine>(_prefab, CountMines, new GameObject("PoolMines").transform);
            _pool.AutoExpand = _isAutoExpandPool;
        }
        
        public override void Shoot()
        {
            if (_lastShotTime <= _minValue)
            {
                Mine mine = _pool.GetFreeElement();
            
                mine.transform.position = _installPoint.position;

                _lastShotTime = _delay;
            }

            _lastShotTime -= Time.deltaTime;
        }
    }
}