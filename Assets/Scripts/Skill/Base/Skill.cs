using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using SkillTask = Task.Task;

public class Skill : ISkill
{
    private Attribute m_Attribute;
    private Entity m_Target;
    private Entity m_Caster;

    public Attribute Attribute
    {
        get
        {
            return m_Attribute;
        }

        set
        {
            m_Attribute = value;
        }
    }

    public Entity Target
    {
        get
        {
            return m_Target;
        }

        set
        {
            m_Target = value;
        }
    }

    public Entity Caster
    {
        get
        {
            return m_Caster;
        }

        set
        {
            m_Caster = value;
        }
    }

    public Skill(Attribute attribute)
    {
        this.Attribute = attribute;
    }
  
    public void End()
    {
    }

    public IEnumerator Cast()
    {
        //释放
        ICast emit = new EffectEmit().Handle(this);
        SkillTask emitTask = new SkillTask("释放", new Task.TimeCondition(1f));
        Task.TaskManager.Instance().AddTask(emitTask);

        //命中
        ICast hit = new EffectHit().Handle(this);
        SkillTask hitTask = new SkillTask("命中", new Task.TimeCondition(1f));
        Task.TaskManager.Instance().AddTask(hitTask);

        //伤害结算和飘字
        SkillTask task = Task.TaskManager.Instance().Next();
        while (task != null)
        {
            yield return task;
            task = Task.TaskManager.Instance().Next();
        }
    }

    public void Init<T, U>(T caster, U target, Attribute attribute = null) where T : Entity where U : Entity
    {
        this.Target = target;
        this.Caster = caster;
        this.Attribute = Attribute ?? attribute;
    }
    public bool IsValid(IVerify verify)
    {
        if (verify != null)
        {
            return verify.Verify(this.Caster, this.Target, this.Attribute);
        }
        return true;
    }

    public bool IsValid(List<IVerify> verifyList)
    {
        if(verifyList!=null && verifyList.Count > 0)
        {
            if (verifyList.Count == 1) return IsValid(verifyList[0]);
            //多余一个的验证条件需要先排序
            verifyList.Sort(delegate (IVerify x, IVerify y) {
                return x.Priority().CompareTo(y.Priority());
            });
            foreach(var verify in verifyList)
            {
                if (!IsValid(verify))
                {
                    return false;
                }
            }
        }
        return true;

    }

    public IEnumerator Sing(Task.Task task)
    {
        yield return task;
    }

    public void Interrupt(IInterrupt interrupt)
    {
        if (interrupt != null)
        {
            interrupt.Handle(this.Caster);
        }
    }
    /// <summary>
    /// 处理驱散
    /// </summary>
    /// <param name="damage"></param>
    /// <returns>正数表示加血, 负数表示扣血</returns>
    public int Caculate(IDamage damage)
    { 
        //驱散处理
        this.m_Caster.Disperse(this.m_Attribute.Mute);
        //处理计算后的返回值
        return damage.Handle(this);
    }

    /// <summary>
    /// 技能结束
    /// </summary>
    /// <param name="complate"></param>
    public void End(SkillComplate complate = null)
    {
        if (complate != null)
        {
            complate(this);
        }
    }
}
