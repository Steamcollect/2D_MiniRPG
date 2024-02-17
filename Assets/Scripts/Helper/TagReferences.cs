using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TagReferences : MonoBehaviour
{
    [Header("PlayerTag")]
    public string playerMain;

    [Header("Props")]
    public string itemParent;

    public static TagReferences instance;

    private void Awake()
    {
        instance = this;
    }
}
