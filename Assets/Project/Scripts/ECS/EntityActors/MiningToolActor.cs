using System;
using Build.Game.Scripts.ECS.Components;
using Project.Game.Scripts;
using UnityEngine;

namespace Build.Game.Scripts.ECS.EntityActors
{
    public class MiningToolActor : MonoBehaviour
    {
        private const string ObjectPoolSoundsOfMiningStoneName = "PoolMiningSoundsOfStone";
        
        [SerializeField] private float _miningRange;
        [SerializeField] private float _delay;
        [SerializeField] private float _damage;
        
        [SerializeField] private Transform _detectionPoint;
        [SerializeField] private Transform _hitEffectPoint;

        [SerializeField] private MiningStoneSound _miningStoneSoundPrefab;
        [SerializeField] private ParticleSystem _hitEffect;

        private ParticleSystem _hitEffectRef;

        private StoneActor _stoneRef;

        private int _countSounds = 4;

        private float _lastHitTime;
        private float _minValue = 0f;

        private ObjectPool<MiningStoneSound> _poolMiningSoundsOfStone;
        
        public bool IsMining { get; private set; }

        private void Awake()
        {
            _poolMiningSoundsOfStone = new ObjectPool<MiningStoneSound>(_miningStoneSoundPrefab, _countSounds, 
                new GameObject(ObjectPoolSoundsOfMiningStoneName).transform);
            
            _hitEffectRef = Instantiate(_hitEffect, _hitEffectPoint);
            _hitEffectRef.Stop();
        }

        private void Update()
        {
            RaycastHit hit;

            if (Physics.Raycast(_detectionPoint.position, _detectionPoint.forward, out hit,
                _miningRange))
            {
                if(_stoneRef != null)
                    _stoneRef.Health.SetHit(false);
                
                if (!hit.collider.TryGetComponent(out StoneActor resource)) return;
                
                IsMining = true;

                if (_lastHitTime <= _minValue)
                {
                    _stoneRef = resource;

                    MiningStoneSound sound = _poolMiningSoundsOfStone.GetFreeElement();
                    
                    sound.AudioSource.PlayOneShot(sound.AudioSource.clip);
            
                    StartCoroutine(sound.OffSound());
                    
                    _stoneRef.Health.TakeDamage(_damage);
                    _stoneRef.Health.SetHit(true);

                    _hitEffectRef.Play();

                    _lastHitTime = _delay;
                }

                _lastHitTime -= Time.deltaTime;
            }
            else
            {
                IsMining = false;
            }
        }

        private void OnDrawGizmos()
        {
            Gizmos.color = Color.blue;
        
            Gizmos.DrawLine(_detectionPoint.position, _detectionPoint.forward);
        }
    }
}