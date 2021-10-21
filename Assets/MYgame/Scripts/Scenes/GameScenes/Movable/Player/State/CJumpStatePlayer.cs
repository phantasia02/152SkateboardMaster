using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class CJumpStatePlayer : CPlayerStateBase
{
    public override StaticGlobalDel.EMovableState StateType() { return StaticGlobalDel.EMovableState.eJump; }

    public CJumpStatePlayer(CMovableBase pamMovableBase) : base(pamMovableBase)
    {

    }

    protected override void InState()
    {
        m_MyPlayerMemoryShare.m_MyMovable.SetStateIndex( StaticGlobalDel.EMovableState.eHit, 1);
        m_MyPlayerMemoryShare.m_MyRigidbody.useGravity = false;
        //m_MyPlayerMemoryShare.m_PlayerFollwer.motion.applyPositionY = false;

        m_MyPlayerMemoryShare.m_MyMovable.transform.DOLocalMoveY(8.0f, 0.5f).SetEase( Ease.OutQuad);




        Time.timeScale = 0.1f;
        base.InState();
    }

    protected override void updataState()
    {
    }

    protected override void OutState()
    {
        m_MyPlayerMemoryShare.m_MyRigidbody.useGravity = true;
        Time.timeScale = 1.0f;
        //m_MyPlayerMemoryShare.m_PlayerFollwer.motion.applyPositionY = true;
    }

    public override void MouseDrag()
    {
        m_MyPlayerMemoryShare.m_MyPlayer.MouseDrag();

       // m_MyPlayerMemoryShare.m_MyMovable.transform.DOLocalMoveY(8.0f, 0.5f).SetEase(Ease.InSine);
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
