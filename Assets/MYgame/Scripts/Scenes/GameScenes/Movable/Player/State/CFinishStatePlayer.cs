using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CFinishStatePlayer : CPlayerStateBase
{
    public override StaticGlobalDel.EMovableState StateType() { return StaticGlobalDel.EMovableState.eFinish; }

    public CFinishStatePlayer(CMovableBase pamMovableBase) : base(pamMovableBase)
    {

    }

    protected override void InState()
    {
        m_MyPlayerMemoryShare.m_PlayerFollwer.motion.applyPositionY = true;
        m_MyPlayerMemoryShare.m_PlayerFollwer.motion.applyRotationY = true;

        m_MyPlayerMemoryShare.m_MyRigidbody.useGravity = false;
        Time.timeScale = 0.3f;
    }

    protected override void updataState()
    {
    }

    protected override void OutState()
    {
        m_MyPlayerMemoryShare.m_PlayerFollwer.enabled = false;
        m_MyPlayerMemoryShare.m_MyRigidbody.useGravity = true;
        Time.timeScale = 1.0f;
    }

    public override void MouseDown()
    {
    }
}
