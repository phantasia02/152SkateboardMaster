using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CMoveStatePlayer : CPlayerStateBase
{
    public override StaticGlobalDel.EMovableState StateType() { return StaticGlobalDel.EMovableState.eMove; }

    bool lTempDown = false;

    public CMoveStatePlayer(CMovableBase pamMovableBase) : base(pamMovableBase)
    {

    }

    protected override void InState()
    {
        SetAnimationState(CAnimatorStateCtl.EState.eRun);
        m_MyPlayerMemoryShare.m_PlayerFollwer.follow = true;
        m_MyPlayerMemoryShare.m_MyMovable.ResetMoveBuff();
    }

    protected override void updataState()
    {
        float lTempAddVal = Time.deltaTime * 0.5f;
        m_MyPlayerMemoryShare.m_MyPlayer.AnimationVal += lTempDown ? lTempAddVal : -lTempAddVal;

        UpdateSpeed();
    }

    protected override void OutState()
    {
    }

    public override void MouseDown()
    {
        lTempDown = true;
    }

    public override void MouseUp()
    {
        lTempDown = false;
    }
}
