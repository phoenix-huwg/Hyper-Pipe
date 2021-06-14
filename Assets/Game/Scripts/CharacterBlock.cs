using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterBlock : MonoBehaviour
{
    private void OnTriggerEnter(Collider other)
    {
        if (other.tag.Equals("Cutter"))
        {
            IEnemyBlockable iEB = other.gameObject.GetComponent<IEnemyBlockable>();

            if (iEB != null)
            {
                iEB.BlockEnemy();
                // Helper.DebugLog("Character Block");
            }
        }
    }
}
