using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class CWinStatePlayer : CPlayerStateBase
{
    public override StaticGlobalDel.EMovableState StateType() { return StaticGlobalDel.EMovableState.eWin; }
    public bool m_ResetTimeFlag = false;
    public float m_ResetTim = 0.0f;
    public float m_DeltaTimeResetTime = 10.0f;

    public CWinStatePlayer(CMovableBase pamMovableBase) : base(pamMovableBase)
    {

    }

    protected override void InState()
    {

        CAllScoringBox lTempAllScoringBox = CAllScoringBox.SharedInstance;
        float lTempJumpPower = (float)m_MyPlayerMemoryShare.m_EndIndex * 2.0f + 8.0f;
        float lTempJumpTime = (float)m_MyPlayerMemoryShare.m_EndIndex * 0.8f;
        lTempJumpTime = Mathf.Max(2.0f, lTempJumpTime);
        int RotationsCount = Mathf.FloorToInt(lTempJumpTime * 0.5f);
        RotationsCount = Mathf.Max(RotationsCount, 1);
        m_ResetTim = lTempJumpTime * 0.5f;

        //m_MyPlayerMemoryShare.m_MyRigidbody.constraints = RigidbodyConstraints.FreezeRotation | RigidbodyConstraints.FreezePositionX;
        // m_MyPlayerMemoryShare.m_MyRigidbody.AddForce(Vector3.up * lTempJumpPower * 100.0f + Vector3.forward * lTempJumpPower * 50.0f);
        m_MyPlayerMemoryShare.m_PlayerFollwer.enabled = false;
        OpenUseGravity(true);

        float lTempRotateTime = lTempJumpTime * 0.8f;
        m_MyPlayerMemoryShare.m_AllObj.transform.DOLocalRotate(new Vector3(0.0f, -360.0f * (float)RotationsCount, 0.0f), lTempRotateTime, RotateMode.LocalAxisAdd);
     //   m_MyPlayerMemoryShare.m_AllObj.transform.DOLocalRotate(new Vector3(0.0f, 0.0f, 360.0f), lTempRotateTime, RotateMode.FastBeyond360);
        m_MyPlayerMemoryShare.m_MyMovable.transform.DOJump(lTempAllScoringBox.AllScoringBox[m_MyPlayerMemoryShare.m_EndIndex].transform.position, lTempJumpPower, 1, lTempJumpTime).SetEase(Ease.Linear);

        ShowBuffAnkleLSkateboard(true);
        m_MyPlayerMemoryShare.m_BuffAnkleLSkateboard.transform.position = m_MyPlayerMemoryShare.m_AnkleLSkateboard.transform.position;
        m_MyPlayerMemoryShare.m_BuffAnkleLSkateboard.transform.rotation = m_MyPlayerMemoryShare.m_AnkleLSkateboard.transform.rotation;

        Sequence TempSequence = DOTween.Sequence();
      //  TempSequence.AppendInterval(0.5f);
        TempSequence.Append(m_MyPlayerMemoryShare.m_BuffAnkleLSkateboard.DOLocalMoveY(-m_ResetTim, lTempRotateTime * 0.5f).SetEase(Ease.Linear));
        TempSequence.Join(m_MyPlayerMemoryShare.m_BuffAnkleLSkateboard.DORotate(new Vector3(360.0f * (float)RotationsCount, 0.0f, 0.0f), lTempRotateTime, RotateMode.FastBeyond360).SetEase(Ease.Linear));
        TempSequence.AppendCallback(() => { m_ResetTimeFlag = true; });
        //TempSequence.Append(m_MyPlayerMemoryShare.m_BuffAnkleLSkateboard.DOLocalMoveY(m_MyPlayerMemoryShare.m_AnkleLSkateboardLocalpos.x, 0.5f).SetEase(Ease.Linear));
        //TempSequence.Join(m_MyPlayerMemoryShare.m_BuffAnkleLSkateboard.DORotateQuaternion(m_MyPlayerMemoryShare.m_AnkleLSkateboardRotate, 0.5f).SetEase(Ease.Linear));
        // TempSequence.SetUpdate(true);
        TempSequence.PlayForward();
        m_ResetTimeFlag = false;
        // m_MyPlayerMemoryShare.m_MyMovable.transform.DOJump(lTempAllScoringBox.AllScoringBox[m_MyPlayerMemoryShare.m_EndIndex].transform.position, 2.0f, 1, lTempJumpTime).SetEase(Ease.InOutExpo);
        //m_MyPlayerMemoryShare.m_EndIndex = (int)(lTempResult * (float)lTempAllScoringBox.AllScoringBox.Count) + 1;

        m_DeltaTimeResetTime = 10.0f - lTempRotateTime * 0.5f;
        m_DeltaTimeResetTime = Mathf.Clamp(m_DeltaTimeResetTime, 3.0f, 10.0f);
    }

    protected override void updataState()
    {
        float TempTimeT = Time.deltaTime * m_DeltaTimeResetTime;

        m_MyPlayerMemoryShare.m_MyMovable.transform.up = Vector3.Lerp(m_MyPlayerMemoryShare.m_MyMovable.transform.up, Vector3.up, TempTimeT * 0.2f);

        if (m_ResetTimeFlag)
        {
            m_MyPlayerMemoryShare.m_BuffAnkleLSkateboard.localPosition = Vector3.Lerp(m_MyPlayerMemoryShare.m_BuffAnkleLSkateboard.localPosition, m_MyPlayerMemoryShare.m_AnkleLSkateboard.localPosition, TempTimeT);
            m_MyPlayerMemoryShare.m_BuffAnkleLSkateboard.localRotation = Quaternion.Lerp(m_MyPlayerMemoryShare.m_BuffAnkleLSkateboard.localRotation, m_MyPlayerMemoryShare.m_AnkleLSkateboard.localRotation, TempTimeT);
        }

    }

    protected override void OutState()
    {

    }

}
