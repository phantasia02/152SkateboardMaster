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

        m_MyPlayerMemoryShare.m_MyMovable.transform.DOLocalMoveY(8.0f, 0.5f).SetEase( Ease.InSine);
       // m_MyPlayerMemoryShare.m_AllObj.transform.DOLocalRotate(new Vector3(0.0f, Random.Range(-120.0f, 120.0f), 0.0f), 0.5f);
    }

    protected override void updataState()
    {
    }

    protected override void OutState()
    {
        m_MyPlayerMemoryShare.m_MyRigidbody.useGravity = true;
        //m_MyPlayerMemoryShare.m_PlayerFollwer.motion.applyPositionY = true;
    }

    public override void MouseDrag()
    {
        m_MyPlayerMemoryShare.m_MyPlayer.MouseDrag();

        m_MyPlayerMemoryShare.m_MyMovable.transform.DOLocalMoveY(8.0f, 0.5f).SetEase(Ease.InSine);
    }

    public override void OnTriggerEnter(Collider other)
    {
        if (other.tag == StaticGlobalDel.TagDoorPost)
            TriggerEnterDoor(other);
    }
}
