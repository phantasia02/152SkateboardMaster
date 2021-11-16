using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CHitJumpStatePlayer : CPlayerStateBase
{
    public override StaticGlobalDel.EMovableState StateType() { return StaticGlobalDel.EMovableState.eHit; }

    bool lNextMove = false;

    public CHitJumpStatePlayer(CMovableBase pamMovableBase) : base(pamMovableBase)
    {

    }

    protected override void InState()
    {
        Time.timeScale = 1.0f;
        m_MyPlayerMemoryShare.m_AddSpeedSecond = 40.0f;
        m_MyPlayerMemoryShare.m_MyPlayer.SetMoveBuff(CMovableBase.ESpeedBuff.eHit, 0.0f);

        m_MyPlayerMemoryShare.m_PlayerFollwer.motion.applyPositionY = false;
        //m_MyPlayerMemoryShare.m_MyRigidbody.useGravity = true;
        lNextMove = false;
    }

    protected override void updataState()
    {
        UpdateSpeed();

        if (lNextMove && MomentinTime(0.5f))
        {
            m_MyPlayerMemoryShare.m_MyMovable.ChangState = StaticGlobalDel.EMovableState.eMove;
        }

    }

    protected override void OutState()
    {
        m_MyPlayerMemoryShare.m_BuffDoorInstanceID = 0;
        //  m_MyPlayerMemoryShare.m_MyRigidbody.useGravity = false;
        // m_MyPlayerMemoryShare.m_MyRigidbody.constraints = RigidbodyConstraints.FreezeRotation;
    }

    public override void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.layer == (int)StaticGlobalDel.ELayerIndex.eFloor)
        {
            lNextMove = true;
            m_StateTime = 0.0f;
            
        }
    }

    //public override void MouseDrag()
    //{
    //    m_MyPlayerMemoryShare.m_MyPlayer.MouseDrag();
    //}

    
}
