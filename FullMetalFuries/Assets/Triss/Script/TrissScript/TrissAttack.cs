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


    public bool IsAttacking { get; private set; }
    private int _attack1Type;
    private int _attackStack;

    [SerializeField] private int _shieldMaxHealth = 75;
    [SerializeField]
    private int _shieldHealth = 75;
    private bool isShieldOn = false;
    private bool isShieldBroken = false;
    private bool isShieldRestoring = false;

    [SerializeField] private float _shieldRestoreDuringTime;
    [SerializeField] private float _shieldRestoreOffsetTime;
    public UnityEvent OnShieldBroke = new UnityEvent();
    private float lastShieldDamagedTime = 0f;

    void Start()
    {
        _input = GetComponent<PlayerInput>();
        _movement = GetComponent<PlayerMovement>();
        _animator = GetComponent<Animator>();

        lastShieldDamagedTime = Time.time;
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
        _movement.ShieldOn(isShieldOn);
        _animator.SetBool(PlayerAnimation.Shield, isShieldOn);

        if(!isShieldRestoring && Time.time - lastShieldDamagedTime > 1f)
        {
            StartCoroutine(RestoreShield());
        }

    }
    public void OnAttack1()
    {
        _attack1Type = (_attack1Type + 1) % 2;
        _animator.SetTrigger(PlayerAnimation.Attack1_1[_attack1Type]);
    }

    public void OnAttack2()
    {
        isShieldOn = _input.Attack2 && !isShieldBroken;
    }

    public void OnDodge()
    {
        throw new System.NotImplementedException();
    }

    public void OnSpecialAttack()
    {
        throw new System.NotImplementedException();
    }


    public void OnCollider(int attackType)
    {
        Colliders[attackType].SetActive(true);
    }

    public void OffCollider(int attackType)
    {
        Colliders[attackType].SetActive(false);
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

        _shieldHealth -= damaged;
        lastShieldDamagedTime = Time.time;
        if (_shieldHealth <= 0)
        {
            outDamage -= _shieldHealth;
            _shieldHealth = 0;
            isShieldBroken = true;
            OnShieldBroke.Invoke();
            StartCoroutine(RestoreShield());
            return true;
        }

        return false;
    }

    private IEnumerator RestoreShield()
    {
        isShieldRestoring = true;

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

        isShieldBroken = false;
        lastShieldDamagedTime = Time.time;
        isShieldRestoring = false;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        Debug.Log("Triggered");
        int outDamaged;
        OnDamaged(50, out outDamaged);
        collision.gameObject.SetActive(false);
    }
}
