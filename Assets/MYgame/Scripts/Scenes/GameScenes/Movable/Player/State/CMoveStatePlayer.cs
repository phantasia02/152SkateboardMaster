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
        m_MyPlayerMemoryShare.m_MyMovable.SetStateIndex(StaticGlobalDel.EMovableState.eHit, 0);
        SetAnimationState(CAnimatorStateCtl.EState.eRun);
        m_MyPlayerMemoryShare.m_AddSpeedSecond = 20.0f;
        m_MyPlayerMemoryShare.m_PlayerFollwer.follow = true;
        m_MyPlayerMemoryShare.m_MyMovable.ResetMoveBuff();

        base.InState();
    }

    protected override void updataState()
    {
        //float lTempAddVal = Time.deltaTime * 0.5f;
        //m_MyPlayerMemoryShare.m_MyPlayer.AnimationVal += lTempDown ? lTempAddVal : -lTempAddVal;

        UpdateSpeed();
    }

    protected override void OutState()
    {
    }

    public override void MouseDrag()
    {
        m_MyPlayerMemoryShare.m_MyPlayer.MouseDrag();
    }

    public override void OnTriggerEnter(Collider other)
    {
        if (other.tag == StaticGlobalDel.TagDoorPost && !m_OneTouch)
        {
            TriggerEnterDoor(other);
            m_OneTouch = true;
        }
    }
}
