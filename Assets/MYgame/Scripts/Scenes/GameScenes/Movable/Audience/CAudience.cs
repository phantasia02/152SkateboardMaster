using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CAudienceMemoryShare : CActorMemoryShare
{
    public CAudience m_MyAudience = null;
   
};

public class CAudience : CActor
{
    public override EMovableType MyMovableType() { return EMovableType.eAudience; }

    public override float DefSpeed { get { return 4.0f; } }

    protected CAudienceMemoryShare m_MyAudienceMemoryShare = null;

    protected override void AddInitState()
    {

        m_AllState[(int)StaticGlobalDel.EMovableState.eMove].AllThisState.Add(new CMoveStateAudience(this));
        m_AllState[(int)StaticGlobalDel.EMovableState.eWin].AllThisState.Add(new CWinStateAudience(this));
    }

    protected override void CreateMemoryShare()
    {
        m_MyAudienceMemoryShare = new CAudienceMemoryShare();
        m_MyMemoryShare = m_MyAudienceMemoryShare;

        base.CreateMemoryShare();

        m_AllObj = m_MyActorMemoryShare.m_AllObj = this.gameObject;
        //SetBaseMemoryShare();

        //m_MaxMoveDirSize = Screen.width > Screen.height ? (float)Screen.width : (float)Screen.height;
        //m_MaxMoveDirSize = m_MaxMoveDirSize / 5.0f;
    }

    protected override void Start()
    {
        base.Start();
        SetCurState(StaticGlobalDel.EMovableState.eMove);
    }
}
