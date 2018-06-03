using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// 施法脚本
/// </summary>
public abstract class ICast : MonoBehaviour {
    public abstract IEnumerator Cast(object userdata);
}
