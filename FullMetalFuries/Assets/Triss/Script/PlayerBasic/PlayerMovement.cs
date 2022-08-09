using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using AnimationAsset;

public class PlayerMovement : MonoBehaviour
{
    [SerializeField]
    private float Speed;

    private PlayerInput _input;
    private Rigidbody2D _rigidbody;
    private Animator _animator;
    private SpriteRenderer _spriteRenderer;

    void Start()
    {
        _input = GetComponent<PlayerInput>();
        _rigidbody = GetComponent<Rigidbody2D>();
        _animator = GetComponent<Animator>();
        _spriteRenderer = GetComponent<SpriteRenderer>();
    }

    void Update()
    {
        if(_input.X != 0 || _input.Y != 0)
        {
            _animator.SetBool(PlayerAnimation.Move, true);

            _spriteRenderer.flipX = _input.X < 0;

            float moveX = _input.X * Speed * Time.deltaTime;
            float moveY = _input.Y * Speed * Time.deltaTime;

            _rigidbody.MovePosition(new Vector2(transform.position.x + moveX, transform.position.y + moveY));
        }
        else
        {
            _animator.SetBool(PlayerAnimation.Move, false);
        }

        transform.position = new Vector3(transform.position.x, transform.position.y, transform.position.y);
    }
}
