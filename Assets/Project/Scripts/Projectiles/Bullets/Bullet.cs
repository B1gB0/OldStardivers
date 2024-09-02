using System.Collections;
using UnityEngine;

public abstract class Bullet : MonoBehaviour
{
    [field: SerializeField] public float LifeTime { get; private set; } = 4f;
    
    private float _bulletSpeed;
    private Transform _enemyPosition;

    protected float Damage { get; private set; }

    private void OnEnable()
    {
        StartCoroutine(LifeRoutine());
    }

    private void OnDisable()
    {
        StopCoroutine(LifeRoutine());
    }

    private void Update()
    {
        transform.position = Vector3.MoveTowards(transform.position, _enemyPosition.position, _bulletSpeed * Time.deltaTime);
    }

    public void SetDirection(Transform enemyPosition)
    {
        _enemyPosition = enemyPosition;
        transform.LookAt(_enemyPosition);
    }

    public void SetCharacteristics(float damage, float bulletSpeed)
    {
        Damage = damage;
        _bulletSpeed = bulletSpeed;
    }

    private IEnumerator LifeRoutine()
    {
        yield return new WaitForSeconds(LifeTime);
        
        gameObject.SetActive(false);
    }
}
