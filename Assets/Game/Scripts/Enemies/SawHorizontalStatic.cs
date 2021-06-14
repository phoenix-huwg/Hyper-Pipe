using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SawHorizontalStatic : Enemy
{
    public override void Update()
    {
        tf_SawModel.Rotate(Vector3.up * (450f * Time.deltaTime), 1f);
    }
}
