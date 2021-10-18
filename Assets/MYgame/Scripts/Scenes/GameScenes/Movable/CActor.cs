﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CActorMemoryShare : CMemoryShareBase
{
    public CActor       m_Target           = null;
    public CActor       m_MyActor          = null;
    public int          m_Hp               = 10;
    public Vector3      m_DeathImpactDir   = Vector3.forward;

};

public abstract class CActor : CMovableBase
{
    //[SerializeField] protected Transform m_MyFloorStartPoint = null;
    //public Transform MyFloorStartPoint { get { return m_MyFloorStartPoint; } }
    //  abstract public EMovableType MyMovableType();
    protected CActorMemoryShare m_MyActorMemoryShare = null;

    public virtual int TargetMask() { return 0; }
    public virtual int TargetIndex() { return 0; }

    public int ActorTypeDataHp
    {
        set { m_MyActorMemoryShare.m_Hp = value; }
        get { return m_MyActorMemoryShare.m_Hp; }
    }

    public Vector3 DeathImpactDir
    {
        set
        {
            m_MyActorMemoryShare.m_DeathImpactDir = value;
            m_MyActorMemoryShare.m_DeathImpactDir.y = 0.0f;
        }
        get { return m_MyActorMemoryShare.m_DeathImpactDir; }
    }


    protected override void Start()
    {
        base.Start();
    }

    public bool SetTarget(CActor target)
    {
        m_MyActorMemoryShare.m_Target = target;

        return true;
    }

    public void ShowMyCollision(bool show)
    {
        for (int i = 0; i < m_MyActorMemoryShare.m_AllChildCollider.Length; i++)
            m_MyActorMemoryShare.m_AllChildCollider[i].gameObject.SetActive(show);

        m_MyActorMemoryShare.m_MyRigidbody.useGravity = !show;
    }

    public virtual void RemoveGroup()
    {
        //m_MyActorMemoryShare.m_MyRigidbody.useGravity = true;
        //m_MyActorMemoryShare.m_MyRigidbody.constraints = RigidbodyConstraints.FreezeRotation;
    }

    public void SetReadyDeath(Vector3 TragetDir)
    {
        RemoveGroup();
        LockChangState = StaticGlobalDel.EMovableState.eDeath;
        ChangState = StaticGlobalDel.EMovableState.eDeath;
        DeathImpactDir = TragetDir;

        for (int i = 0; i < m_MyActorMemoryShare.m_AllChildCollider.Length; i++)
            m_MyActorMemoryShare.m_AllChildCollider[i].gameObject.layer = 0;
    }

    


}
