using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TriggerDoc : MonoBehaviour
{
    void OnEnable()
    {
        SpawnDoc.TriggerDoc += SelfDestruct;
    }

    void OnDisable()
    {
        SpawnDoc.TriggerDoc -= SelfDestruct;
    }

    void SelfDestruct()
    {
        Destroy(this.gameObject);
    }
}
