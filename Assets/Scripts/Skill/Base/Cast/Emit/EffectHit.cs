using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EffectHit : IEmit
{
    public ICast Handle(Skill skill)
    {
        string effectName = skill.Attribute.Effect_HitName;
        if (string.IsNullOrEmpty(effectName)) return null;

        GameObject prefab = Resources.Load<GameObject>(effectName);
        GameObject clone = GameObject.Instantiate<GameObject>(prefab);
        //TO-DO 根据不同处理不同的发射情况
        return clone.AddComponent<Floating>();
    }
}
