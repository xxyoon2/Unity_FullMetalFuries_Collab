using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using AnimationAsset;

public class PlayerMovement : MonoBehaviour
{
    [SerializeField]
    private float Speed;
    [SerializeField]
    private float ShieldSpeed;
    private float currentSpeed;

    private const float Idel2PlayTime = 12f;

    private PlayerInput _input;
    private IPlayerAttackable _attack;
    private Rigidbody2D _rigidbody;
    private Animator _animator;
    private SpriteRenderer _spriteRenderer;

    private float idelElapsedTime = 0f;

    void Start()
    {
        _input = GetComponent<PlayerInput>();
        _attack = GetComponent<IPlayerAttackable>();
        _rigidbody = GetComponent<Rigidbody2D>();
        _animator = GetComponent<Animator>();
        _spriteRenderer = GetComponent<SpriteRenderer>();

        currentSpeed = Speed;
    }

    void FixedUpdate()
    {
        if (!_attack.IsAttacking() && (_input.X != 0 || _input.Y != 0))
        {
            idelElapsedTime = 0f;
            _animator.SetBool(PlayerAnimation.Move, true);

            // 뒤집기
            //_spriteRenderer.flipX = _input.X < 0;
            if(_input.X != 0)
            {
                transform.localScale = new Vector3(_input.X, transform.localScale.y, transform.localScale.z);

            }

            float moveX = _input.X * currentSpeed * Time.fixedDeltaTime;
            float moveY = _input.Y * currentSpeed * Time.fixedDeltaTime;

            _rigidbody.MovePosition(new Vector2(transform.position.x + moveX, transform.position.y + moveY));
        }
        else
        {
            idelElapsedTime += Time.deltaTime;
            if (idelElapsedTime >= Idel2PlayTime)
            {
                idelElapsedTime = 0f;
                _animator.SetTrigger(PlayerAnimation.Idel2);
            }
            else
            {
                _animator.SetBool(PlayerAnimation.Move, false);
            }
        }

        transform.position = new Vector3(transform.position.x, transform.position.y, transform.position.y);
    }

    // 이건 트리스 전용, 알랙스도 사용 가능(자유룝게 수정하세요)
    public virtual void ShieldOn(bool isShieldOn)
    {
        currentSpeed = isShieldOn ? ShieldSpeed : Speed;
    }
}
