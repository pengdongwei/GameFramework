using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public partial class Attribute {
    private string m_Effect_EmitName;
    private string m_Effect_HitName;

    public string Effect_EmitName
    {
        get
        {
            return m_Effect_EmitName;
        }

        set
        {
            m_Effect_EmitName = value;
        }
    }

    public string Effect_HitName
    {
        get
        {
            return m_Effect_HitName;
        }

        set
        {
            m_Effect_HitName = value;
        }
    }
}
