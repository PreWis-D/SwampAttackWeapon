using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShockWave : MonoBehaviour
{
    [SerializeField] private int _damage;
    [SerializeField] private float _speed;

    private void Update()
    {
        transform.Translate(Vector2.left * _speed * Time.deltaTime, Space.World);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        bool isReached = false;

        if (collision.gameObject.TryGetComponent(out Enemy enemy))
        {
            if (!isReached)
                enemy.TakeDamage(_damage);
            else
                return;
        }

        if (collision.gameObject.TryGetComponent(out ZoneDestroy zone))
            Destroy(gameObject);
    }
}
