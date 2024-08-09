using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

public abstract class Bullet : MonoBehaviour
{
    [field: SerializeField] public float MovementSpeed { get; private set; }

    [field: SerializeField] public float Damage { get; private set; }

    [field: SerializeField] public float LifeTime { get; private set; } = 4f;

    private Transform _enemyPosition;

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
        transform.position = Vector3.MoveTowards(transform.position, _enemyPosition.position, MovementSpeed * Time.deltaTime);
    }

    public void SetDirection(Transform enemyPosition)
    {
        _enemyPosition = enemyPosition;

        transform.LookAt(_enemyPosition);
    }

    private IEnumerator LifeRoutine()
    {
        yield return new WaitForSeconds(LifeTime);
        
        gameObject.SetActive(false);
    }
}
