using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MpVerify : IVerify
{
    public int Priority()
    {
        return 1;
    }

    public bool Verify(Entity caster, Entity target, Attribute attribute)
    {
        if (attribute.CostMp <= caster.Mp)
        {
            return true;
        }
        else
        {
            throw new MpException(caster, attribute);
        }
    }
}
