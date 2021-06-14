using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SawCollision : MonoBehaviour, IEnemyBlockable
{
    public Collider col_Owner;

    private void OnEnable()
    {
        col_Owner.enabled = true;
    }

    public void BlockEnemy()
    {
        col_Owner.enabled = false;
    }
}
