using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class CJumpNormalStatePlayer : CPlayerStateBase
{
    public override StaticGlobalDel.EMovableState StateType() { return StaticGlobalDel.EMovableState.eJump; }

    public CJumpNormalStatePlayer(CMovableBase pamMovableBase) : base(pamMovableBase)
    {

    }

    protected override void InState()
    {
        m_MyPlayerMemoryShare.m_MyRigidbody.useGravity = true;
        m_MyPlayerMemoryShare.m_MyRigidbody.AddForce(Vector3.up * Random.Range(370.0f, 430.0f));

        m_MyPlayerMemoryShare.m_AllObj.transform.DOLocalRotate(new Vector3(0.0f, -360.0f, 0.0f), 1.0f, RotateMode.LocalAxisAdd);

        Vector3 lTempLocalpos = m_MyPlayerMemoryShare.m_AnkleLSkateboard.localPosition;
        Quaternion lTempDORotate = m_MyPlayerMemoryShare.m_AnkleLSkateboard.rotation;

        ShowBuffAnkleLSkateboard(true);

        m_MyPlayerMemoryShare.m_BuffAnkleLSkateboard.transform.position = m_MyPlayerMemoryShare.m_AnkleLSkateboard.transform.position;
        m_MyPlayerMemoryShare.m_BuffAnkleLSkateboard.transform.rotation = m_MyPlayerMemoryShare.m_AnkleLSkateboard.transform.rotation;

        Sequence TempSequence = DOTween.Sequence();
        TempSequence.Append(m_MyPlayerMemoryShare.m_BuffAnkleLSkateboard.DOLocalMoveY(-0.5f, 0.5f).SetEase(Ease.Linear));
        TempSequence.Join(m_MyPlayerMemoryShare.m_BuffAnkleLSkateboard.DORotate(new Vector3(0.0f, 360.0f, 360.0f), 0.5f, RotateMode.FastBeyond360).SetEase(Ease.Linear));
        //TempSequence.Append(m_MyPlayerMemoryShare.m_BuffAnkleLSkateboard.DOLocalMoveY(m_MyPlayerMemoryShare.m_AnkleLSkateboardLocalpos.x, 0.5f).SetEase(Ease.Linear));
        //TempSequence.Join(m_MyPlayerMemoryShare.m_BuffAnkleLSkateboard.DORotateQuaternion(m_MyPlayerMemoryShare.m_AnkleLSkateboardRotate, 0.5f).SetEase(Ease.Linear));
        TempSequence.SetUpdate(true);
        TempSequence.PlayForward();
    }

    protected override void updataState()
    {
    }

    protected override void OutState()
    {
        ShowBuffAnkleLSkateboard(false);
    }

    public override void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.layer == (int)StaticGlobalDel.ELayerIndex.eFloor && m_OldStateTime > 0.5f)
            m_MyPlayerMemoryShare.m_MyMovable.ChangState = StaticGlobalDel.EMovableState.eMove;

    }

    public override void OnTriggerEnter(Collider other)
    {
        if (other.tag == StaticGlobalDel.TagDoorPost)
        {
            TriggerEnterDoor(other);
            //  m_OneTouch = true;
        }
    }

    public override void MouseDrag()
    {
        m_MyPlayerMemoryShare.m_MyPlayer.MouseDrag();
    }
}
