using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class CJumpGroupPostStatePlayer : CPlayerStateBase
{
    public override StaticGlobalDel.EMovableState StateType() { return StaticGlobalDel.EMovableState.eJump; }

    public CJumpGroupPostStatePlayer(CMovableBase pamMovableBase) : base(pamMovableBase)
    {

    }

    protected override void InState()
    {
        m_MyPlayerMemoryShare.m_MyMovable.SetStateIndex(StaticGlobalDel.EMovableState.eHit, 1);
        OpenUseGravity(false);
        //m_MyPlayerMemoryShare.m_PlayerFollwer.motion.applyPositionY = false;
        //m_MyPlayerMemoryShare.m_MyMovable.transform.d

        SameStateUpdataPlay();

        base.InState();
    }

    protected override void updataState()
    {
    }

    protected override void OutState()
    {
        OpenUseGravity(true);
        //m_MyPlayerMemoryShare.m_PlayerFollwer.motion.applyPositionY = true;
    }

    public override void MouseDrag()
    {
        m_MyPlayerMemoryShare.m_MyPlayer.MouseDrag();
    }

    public override void OnTriggerEnter(Collider other)
    {
        if (other.tag == StaticGlobalDel.TagDoorPost)
        {

            TriggerEnterDoor(other);
            // m_OneTouch = true;
        }
    }

    protected override void SameStateUpdataPlay()
    {
        m_MyPlayerMemoryShare.m_MyMovable.transform.DOPause();
        m_MyPlayerMemoryShare.m_MyMovable.transform.DOMoveY(m_MyPlayerMemoryShare.m_NextDoorPost.transform.position.y, 0.1f).SetEase(Ease.Linear);
    }
}
