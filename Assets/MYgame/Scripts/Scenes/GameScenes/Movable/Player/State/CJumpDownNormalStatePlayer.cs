using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CJumpDownNormalStatePlayer : CPlayerStateBase
{
    public override StaticGlobalDel.EMovableState StateType() { return StaticGlobalDel.EMovableState.eJumpDown; }

    public CJumpDownNormalStatePlayer(CMovableBase pamMovableBase) : base(pamMovableBase)
    {

    }

    protected override void InState()
    {
        Time.timeScale = 1.0f;
    }

    protected override void updataState()
    {
    }

    protected override void OutState()
    {
    }
}
