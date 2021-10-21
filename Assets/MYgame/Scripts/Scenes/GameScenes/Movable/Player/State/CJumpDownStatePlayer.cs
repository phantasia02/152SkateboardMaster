using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class CJumpDownStatePlayer : CPlayerStateBase
{
    public override StaticGlobalDel.EMovableState StateType() { return StaticGlobalDel.EMovableState.eJumpDown; }

    public CJumpDownStatePlayer(CMovableBase pamMovableBase) : base(pamMovableBase)
    {

    }

    protected override void InState()
    {
        m_MyPlayerMemoryShare.m_MyRigidbody.useGravity = true;
        m_MyPlayerMemoryShare.m_MyRigidbody.AddForce(Vector3.up * 200.0f);

        m_MyPlayerMemoryShare.m_AllObj.transform.DOLocalRotate(new Vector3(0.0f, -360.0f, 0.0f), 1.0f, RotateMode.LocalAxisAdd);

        Vector3 lTempLocalpos = m_MyPlayerMemoryShare.m_AnkleLSkateboard.localPosition;
        Quaternion lTempDORotate = m_MyPlayerMemoryShare.m_AnkleLSkateboard.rotation;

        Sequence TempSequence = DOTween.Sequence();
        //TempSequence.AppendInterval(0.5f);
        TempSequence.Append(m_MyPlayerMemoryShare.m_AnkleLSkateboard.DOLocalMoveX(2.0f, 1.0f).SetEase(Ease.Linear));
        TempSequence.Join(m_MyPlayerMemoryShare.m_AnkleLSkateboard.DORotate(new Vector3(0.0f, Random.Range(0.0f, 360.0f), Random.Range(360.0f, 720.0f)), 1.0f, RotateMode.FastBeyond360).SetEase(Ease.Linear));
        TempSequence.Append(m_MyPlayerMemoryShare.m_AnkleLSkateboard.DOLocalMoveX(m_MyPlayerMemoryShare.m_AnkleLSkateboardLocalpos.x, 1.0f).SetEase(Ease.Linear));
        TempSequence.Join(m_MyPlayerMemoryShare.m_AnkleLSkateboard.DORotateQuaternion(m_MyPlayerMemoryShare.m_AnkleLSkateboardRotate, 0.5f).SetEase(Ease.Linear));
        TempSequence.SetUpdate(true);
        TempSequence.PlayForward();
    }

    protected override void updataState()
    {
    }

    protected override void OutState()
    {
    }

    public override void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.layer == (int)StaticGlobalDel.ELayerIndex.eFloor)
            m_MyPlayerMemoryShare.m_MyMovable.ChangState = StaticGlobalDel.EMovableState.eMove;

    }

    //public override void MouseDrag()
    //{
    //    m_MyPlayerMemoryShare.m_MyPlayer.MouseDrag();
    //}


}
