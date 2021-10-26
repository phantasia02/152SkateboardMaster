using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CFinishStatePlayer : CPlayerStateBase
{
    public override StaticGlobalDel.EMovableState StateType() { return StaticGlobalDel.EMovableState.eFinish; }
    protected float m_TotleRange = 0.1f;
    public const float m_MaxPercent = 1.0f;

    public CFinishStatePlayer(CMovableBase pamMovableBase) : base(pamMovableBase)
    {
        m_TotleRange = m_MaxPercent - (float)m_MyGameManager.EndLastSplineFollower.startPosition;
        Debug.Log($" m_TotleRange = { m_TotleRange}");
    }

    protected override void InState()
    {
        m_MyPlayerMemoryShare.m_PlayerFollwer.motion.applyPositionY = true;
        m_MyPlayerMemoryShare.m_PlayerFollwer.motion.applyRotationY = true;

        m_MyPlayerMemoryShare.m_MyRigidbody.useGravity = false;
        Time.timeScale = 0.3f;
        m_MyPlayerMemoryShare.m_UpdateUI = false;

        
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
        m_MyPlayerMemoryShare.m_MyMovable.ChangState = StaticGlobalDel.EMovableState.eWait;

        float lTemp = 1.0f - (float)m_MyPlayerMemoryShare.m_PlayerFollwer.result.percent;
        Debug.Log($" lTemp  = { lTemp }");
        //if (lTemp < 0.0f)
        //{
        //    Debug.Log("game over");
        //    return;
        //}

        Debug.Log($" 1 - lTemp / m_TotleRange = {1 - lTemp / m_TotleRange}");
       
    }
}
