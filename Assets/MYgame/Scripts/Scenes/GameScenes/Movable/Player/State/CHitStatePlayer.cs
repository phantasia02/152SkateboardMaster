using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class CHitStatePlayer : CPlayerStateBase
{
    public override StaticGlobalDel.EMovableState StateType() { return StaticGlobalDel.EMovableState.eHit; }

    public CHitStatePlayer(CMovableBase pamMovableBase) : base(pamMovableBase)
    {

    }

    protected override void InState()
    {
        m_MyPlayerMemoryShare.m_PlayerFollwer.direction = Dreamteck.Splines.Spline.Direction.Backward;
     //   m_MyPlayerMemoryShare.m_PlayerFollwer.motion.applyPositionY = false;
        m_MyPlayerMemoryShare.m_MyPlayer.SetMoveBuff(CMovableBase.ESpeedBuff.eHit, 0.0f);
        m_MyPlayerMemoryShare.m_AddSpeedSecond = 10.0f;

        m_MyPlayerMemoryShare.m_MyRigidbody.AddForce(Vector3.up * Random.Range(150.0f, 200.0f));
        //Sequence TempSequence = DOTween.Sequence();
        //TempSequence.Append(m_MyPlayerMemoryShare.m_MyMovable.transform.DOLocalMoveY(1.0f, 0.25f));
        //TempSequence.Append(m_MyPlayerMemoryShare.m_MyMovable.transform.DOLocalMoveY(0.0f, 0.25f));
        //TempSequence.PlayForward();

        m_MyPlayerMemoryShare.m_AllObj.transform.DOLocalRotate(new Vector3(0.0f, Random.Range(-120.0f, 120.0f), 0.0f), 0.5f);
    }

    protected override void updataState()
    {
        UpdateSpeed();
      
        if (m_MyMemoryShare.m_TotleSpeed <= 1.0f)
            m_MyPlayerMemoryShare.m_MyMovable.ChangState = StaticGlobalDel.EMovableState.eMove;

    }

    protected override void OutState()
    {
        m_MyPlayerMemoryShare.m_PlayerFollwer.direction = Dreamteck.Splines.Spline.Direction.Forward;
      //  m_MyPlayerMemoryShare.m_PlayerFollwer.motion.applyPositionY = true;

        m_MyPlayerMemoryShare.m_AllObj.transform.DOLocalRotate(new Vector3(0.0f, 0.0f, 0.0f), 1.0f);
        //  m_MyPlayerMemoryShare.m_AddSpeedSecond = 5.0f;
    }

    public override void MouseDrag()
    {
        m_MyPlayerMemoryShare.m_MyPlayer.MouseDrag();
    }
}
