using UnityEngine;

public class PlayerInput : MonoBehaviour
{
    public float X { get; private set; }
    public float Y { get; private set; }

    public bool Attack1 { get; private set; }
    public bool Attack2 { get; private set; }
    public bool SpecialAttack { get; private set; }
    public bool Dodge { get; private set; }

    public bool Reload { get; private set; }

    void Update()
    {
        X = Y = 0f;
        Attack1 = Attack2 = SpecialAttack = Dodge = false;

        if(Input.GetKey(KeyCode.W))
        {
            Y = 1;
        }
        else if(Input.GetKey(KeyCode.S))
        {
            Y = -1;
        }

        if (Input.GetKey(KeyCode.D))
        {
            X = 1;
        }
        else if (Input.GetKey(KeyCode.A))
        {
            X = -1;
        }

        Attack1 = Input.GetMouseButtonDown(0);
        Attack2 = Input.GetMouseButtonDown(1);

        SpecialAttack = Input.GetKeyDown(KeyCode.F);
        Dodge = Input.GetKeyDown(KeyCode.Space);

        Reload = Input.GetKeyDown(KeyCode.R);
    }
}
