using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyDetecter : MonoBehaviour
{
    private Transform target;
    private float Distance;

    private void OnTriggerStay2D(Collider2D other)
    {
        
    }

    public Transform GetNearestTarget()
    {
        return transform;
    }
}
