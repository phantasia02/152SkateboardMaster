using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CHitStatePlayer : CPlayerStateBase
{
    public override StaticGlobalDel.EMovableState StateType() { return StaticGlobalDel.EMovableState.eHit; }

    public CHitStatePlayer(CMovableBase pamMovableBase) : base(pamMovableBase)
    {

    }

    protected override void InState()
    {
        m_MyPlayerMemoryShare.m_PlayerFollwer.direction = Dreamteck.Splines.Spline.Direction.Backward;
        m_MyPlayerMemoryShare.m_MyPlayer.SetMoveBuff(CMovableBase.ESpeedBuff.eHit, 0.0f);
        m_MyPlayerMemoryShare.m_AddSpeedSecond = 10.0f;


    }

    protected override void updataState()
    {
        UpdateSpeed();
        Debug.Log($" m_MyMemoryShare.m_TotleSpeed = { m_MyMemoryShare.m_TotleSpeed}");
        if (m_MyMemoryShare.m_TotleSpeed <= 1.0f)
            m_MyPlayerMemoryShare.m_MyMovable.ChangState = StaticGlobalDel.EMovableState.eMove;

    }

    protected override void OutState()
    {
        m_MyPlayerMemoryShare.m_PlayerFollwer.direction = Dreamteck.Splines.Spline.Direction.Forward;
      //  m_MyPlayerMemoryShare.m_AddSpeedSecond = 5.0f;
    }
}
