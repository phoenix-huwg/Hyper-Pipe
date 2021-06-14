using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PipeCollect : InGameObject
{
    public Collider col_Owner;

    private void Update()
    {
        transform.Rotate(Vector3.up * (450f * Time.deltaTime));
    }

    public override void OnEnable()
    {
        col_Owner.enabled = true;
        base.OnEnable();
    }

    public void SetupCollected()
    {
        col_Owner.enabled = false;
        Destroy(gameObject);
    }
}
