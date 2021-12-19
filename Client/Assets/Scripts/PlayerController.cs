using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

public class PlayerController : EventBehaviour
{
    [SerializeField] float _moveSpeed;
    [SerializeField] float _jumpPower;

    private Animator _animator;
    private Rigidbody2D _rigidbody;
    private SpriteRenderer _renderer;

    public float MoveSpeed { get => _moveSpeed; set => _moveSpeed = value; }
    public float JumpPower { get => _jumpPower; set => _jumpPower = value; }

    protected override void Awake()
    {
        base.Awake();

        _animator = GetComponent<Animator>();
        _renderer = GetComponent<SpriteRenderer>();
        _rigidbody = GetComponent<Rigidbody2D>();

        gameObject.SetActive(false);
    }

    private void Start()
    {
        this.MoveSpeed = DataManager.Get().Data.DefaultPlayerSpeed;
        this.JumpPower = DataManager.Get().Data.DefaultPlayerJumpPower;
    }

    private void Update()
    {
        if (Input.GetButtonDown("Jump") && _animator.GetBool("isJump") == false)
        {
            _rigidbody.AddForce(Vector2.up * JumpPower, ForceMode2D.Impulse);
            _animator.SetBool("isJump", true);
        }

        if (Input.GetButton("Horizontal"))
        {
            _renderer.flipX = Input.GetAxisRaw("Horizontal") == -1;
        }

        if (Mathf.Abs(_rigidbody.velocity.x) < 0.3f)
            _animator.SetBool("isRunning", false);
        else
            _animator.SetBool("isRunning", true);
    }

    private void FixedUpdate()
    {
        var h = Input.GetAxisRaw("Horizontal");
        _rigidbody.AddForce(new Vector2(MoveSpeed * h, 0));
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.layer == LayerMask.NameToLayer("Ground"))
        {
            _animator.SetBool("isJump", false);
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.layer == LayerMask.NameToLayer("Poop"))
        {
            var poop = collision.GetComponent<Poop>();
            if (poop != null)
            {
                OnAttacked(poop.Damage);
            }
        }
    }

    private void OnAttacked(int damage)
    {
        GameManager.Get().Hp -= damage;
        EventManager.Get().Dispatch(GameEventType.PLAYER_ATTACKED);
    }

    public override void OnEvent(GameEventType eventType, object token)
    {
        if (eventType == GameEventType.GAME_START)
        {
            gameObject.SetActive(true);
        }
        else  if (eventType == GameEventType.GAME_STOP)
        {
            gameObject.SetActive(false);
        }
    }
}
