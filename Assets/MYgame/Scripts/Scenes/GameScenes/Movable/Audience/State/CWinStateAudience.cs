using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CWinStateAudience : CAudienceStateBase
{
    public override StaticGlobalDel.EMovableState StateType() { return StaticGlobalDel.EMovableState.eWin; }

    public CWinStateAudience(CMovableBase pamMovableBase) : base(pamMovableBase)
    {

    }

    protected override void InState()
    {
        SetAnimationState(CAnimatorStateCtl.EState.eWin);
        m_MyAudienceMemoryShare.m_MyMovable.AnimatorStateCtl.transform.NewFxAddParentShow(CGGameSceneData.EAllFXType.eAttributeGood, new Vector3(0.0f, 2.0f, 0.0f));
    }

    protected override void updataState()
    {
    }

    protected override void OutState()
    {

    }
}
