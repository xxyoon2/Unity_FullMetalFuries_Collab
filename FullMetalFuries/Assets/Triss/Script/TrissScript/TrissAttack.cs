using AnimationAsset;
using System.Collections;
using UnityEngine;
using UnityEngine.Events;

public class TrissAttack : MonoBehaviour, IPlayerAttackable
{
    public GameObject[] Colliders;

    private PlayerInput _input;
    private PlayerMovement _movement;
    private Animator _animator;
    private Rigidbody2D _rigidbody;


    private bool _isAttacking;
    private int _attack1Type;
    private int _attackStack;
    private float _attackForce = 5f;
    private const int _maxAttackStack = 3;
    private float _lastAttack1Time;
    private float _attackStackOffsetTime = 1f;
    public int attackDamage { get; set; }

    // 공격2: 쉴드 관련
    [SerializeField] private int _shieldMaxHealth = 75;
    [SerializeField]
    private int _shieldHealth = 75;
    private bool _isShieldOn = false;
    private bool _isShieldBroken = false;
    private bool _isShieldRestoring = false;

    [SerializeField] private float _shieldRestoreDuringTime;
    [SerializeField] private float _shieldRestoreOffsetTime;
    public UnityEvent OnShieldBroke = new UnityEvent();
    private float _lastShieldDamagedTime = 0f;

    void Start()
    {
        _input = GetComponent<PlayerInput>();
        _movement = GetComponent<PlayerMovement>();
        _animator = GetComponent<Animator>();
        _rigidbody = GetComponent<Rigidbody2D>();

        _lastShieldDamagedTime = Time.time;
        _lastAttack1Time = Time.time;
    }

    void Update()
    {

        if (_input.Attack1)
        {
            OnAttack1();
        }
        else
        {
            OnAttack2();

            if (_input.SpecialAttack)
            {
                OnSpecialAttack();
            }
            else if (_input.Dodge)
            {
                OnDodge();
            }
        }

        // 쉴드 관련 후처리
        _movement.ShieldOn(_isShieldOn);
        _animator.SetBool(PlayerAnimation.Shield, _isShieldOn);

        if(!_isShieldRestoring && Time.time - _lastShieldDamagedTime > _shieldRestoreOffsetTime)
        {
            StartCoroutine(RestoreShield());
        }

    }
    public void OnAttack1()
    {
        if(_isShieldOn || _isAttacking)
        {
            return;
        }

        _isAttacking = true;

        // 충돌 주기
        Vector2 direction = transform.localScale.x > 0 ? Vector2.right : Vector2.left;
        _rigidbody.AddForce(direction * _attackForce, ForceMode2D.Impulse);
        
        Colliders[(int) IPlayerAttackable.AttackType.Attack1].SetActive(true);

        switch(_attackStack)
        {
            case 0:
                _attack1Type = (_attack1Type + 1) % 2;
                _animator.SetTrigger(PlayerAnimation.Attack1_1[_attack1Type]);
                break;
            case 1:
                _animator.SetTrigger(PlayerAnimation.Attack1_2);
                break;
            case 2:
                _animator.SetTrigger(PlayerAnimation.Attack1_3);
                break;
        }
        
    }

    public void OnAttack2()
    {
        _isShieldOn = _input.Attack2 && !_isShieldBroken;
    }

    public void OnDodge()
    {
        throw new System.NotImplementedException();
    }

    public void OnSpecialAttack()
    {
        _isAttacking = true;
        throw new System.NotImplementedException();
    }

    public bool IsAttacking()
    {
        return _isAttacking;
    }

    public void OnAttackDone(int attackType)
    {
        Debug.Log("gkgk");
        _isAttacking = false;

        Colliders[(int)IPlayerAttackable.AttackType.Attack1].SetActive(false);
    }

    /// <summary>
    /// 플레이어가 공격을 받았을 경우 나타나는 효과를 적용.
    /// </summary>
    /// <param name="damaged"> 받은 데미지 </param>
    /// <returns> 공격을 받았으면 true 반환, 공격을 받지 않았다면(막았다면) false 반환</returns>
    public bool OnDamaged(int damaged, out int outDamage)
    {
        Debug.Log("Damaged");

        outDamage = 0;
        if (!_input.Attack2)
        {
            outDamage = damaged;
            return true;
        }

        if (_shieldHealth <= 0)
        {
            outDamage = damaged;
            return true;
        }

        
        // 쉴드 공격 방어 처리
        _shieldHealth -= damaged;
        _lastShieldDamagedTime = Time.time;

        if (_shieldHealth <= 0)
        {
            outDamage -= _shieldHealth;
            _shieldHealth = 0;
            _isShieldBroken = true;
            OnShieldBroke.Invoke();
            StartCoroutine(RestoreShield());
            return true;
        }

        return false;
    }

    private IEnumerator RestoreShield()
    {
        _isShieldRestoring = true;

        int startShieldHealth = _shieldHealth;
        int endShieldHealth = _shieldMaxHealth;
        float currentShieldHealth = startShieldHealth;
        
        yield return new WaitForSeconds(_shieldRestoreOffsetTime);

        while (true)
        {
            currentShieldHealth = Mathf.Clamp(currentShieldHealth + Time.deltaTime * _shieldRestoreDuringTime, 0, endShieldHealth);
            _shieldHealth = (int) currentShieldHealth;

            if(currentShieldHealth >= endShieldHealth)
            {
                break;
            }

            yield return null;
        }

        _isShieldBroken = false;
        _lastShieldDamagedTime = Time.time;
        _isShieldRestoring = false;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(_isShieldOn)
        {
            int outDamaged;
            OnDamaged(50, out outDamaged);
            collision.gameObject.SetActive(false);
        }
        else
        {

            if (Time.time - _lastAttack1Time < _attackStackOffsetTime)
            {
                _attackStack = (_attackStack + 1) % 3;
            }
            else
            {
                _attackStack = 0;
                _lastAttack1Time = Time.time;
            }
        }
    }
}
