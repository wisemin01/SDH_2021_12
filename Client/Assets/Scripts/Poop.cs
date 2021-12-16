using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

public class Poop : MonoBehaviour
{
    [SerializeField] float _speed;
    [SerializeField] int _damage;

    private ObjectPool _pool;

    public int Damage { get => _damage; set => _damage = value; }
    public float Speed { set => _speed = value; }

    public void SetObjectPool(ObjectPool pool)
    {
        _pool = pool;
    }

    private void FixedUpdate()
    {
        this.transform.Translate(new Vector2(0, -_speed * Time.fixedDeltaTime));

        if (transform.position.y < -10)
        {
            _pool.Return(this.gameObject);
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.layer == LayerMask.NameToLayer("Player"))
        {
            this._pool.Return(gameObject);
        }
    }
}
