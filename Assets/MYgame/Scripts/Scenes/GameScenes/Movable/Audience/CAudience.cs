using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CAudience : CActor
{
    public override EMovableType MyMovableType() { return EMovableType.eAudience; }

    protected override void AddInitState()
    {


    }

    protected override void CreateMemoryShare()
    {
        m_MyMemoryShare = new CActorMemoryShare();

        base.CreateMemoryShare();

        m_AllObj = m_MyActorMemoryShare.m_AllObj = this.gameObject;
        //SetBaseMemoryShare();

        //m_MaxMoveDirSize = Screen.width > Screen.height ? (float)Screen.width : (float)Screen.height;
        //m_MaxMoveDirSize = m_MaxMoveDirSize / 5.0f;
    }
}
