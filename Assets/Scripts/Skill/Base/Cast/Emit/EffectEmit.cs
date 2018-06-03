using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EffectEmit : IEmit
{
    public ICast Handle(Skill skill)
    {
        string emitname = skill.Attribute.Effect_EmitName;
        if (string.IsNullOrEmpty(emitname)) return null;

        GameObject prefab = Resources.Load<GameObject>(emitname);
        GameObject clone = GameObject.Instantiate<GameObject>(prefab);
        //TO-DO 根据不同处理不同的发射情况
       return clone.AddComponent<Floating>();  
    }
}
