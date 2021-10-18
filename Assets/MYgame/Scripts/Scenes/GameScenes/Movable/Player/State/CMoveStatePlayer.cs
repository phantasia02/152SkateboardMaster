using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CMoveStatePlayer : CPlayerStateBase
{
    public override StaticGlobalDel.EMovableState StateType() { return StaticGlobalDel.EMovableState.eMove; }

    public CMoveStatePlayer(CMovableBase pamMovableBase) : base(pamMovableBase)
    {

    }

    protected override void InState()
    {
        m_MyPlayerMemoryShare.m_PlayerFollwer.follow = true;
        //SetAnimationState(CAnimatorStateCtl.EState.eRun);
        m_MyPlayerMemoryShare.m_MyMovable.ResetMoveBuff();
    }

    protected override void updataState()
    {
        UpdateSpeed();
    }

    protected override void OutState()
    {
    }


}
