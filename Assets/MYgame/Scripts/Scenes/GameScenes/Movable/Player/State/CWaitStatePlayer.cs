using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CWaitStatePlayer : CPlayerStateBase
{
    public override StaticGlobalDel.EMovableState StateType() { return StaticGlobalDel.EMovableState.eWait; }

    public CWaitStatePlayer(CMovableBase pamMovableBase) : base(pamMovableBase)
    {

    }

    protected override void InState()
    {
        SetAnimationState(CAnimatorStateCtl.EState.eIdle);
        m_MyPlayerMemoryShare.m_MyMovable.SetMoveBuff(CMovableBase.ESpeedBuff.eHit, 0.0f, true);
        ShowMoveFx(false);
    }

    protected override void updataState()
    {
    }

    protected override void OutState()
    {
        m_MyPlayerMemoryShare.m_UpdateUI = true;
        ShowMoveFx(true);
    }

    public override void MouseDown()
    {
        m_MyPlayerMemoryShare.m_MyMovable.ChangState = StaticGlobalDel.EMovableState.eMove;
    }
}
