using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AwakeDisabler : MonoBehaviour
{
    private void Start()
    {
        gameObject.SetActive(false);
    }
}
