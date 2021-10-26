using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class CPlayerStateBase : CMovableStatePototype
{
    protected CPlayerMemoryShare m_MyPlayerMemoryShare = null;
    protected bool m_OneTouch = false;
    protected int m_BuffDoorInstanceID = 0;
    protected float m_End = 0;

    public CPlayerStateBase(CMovableBase pamMovableBase) : base(pamMovableBase)
    {
        m_MyPlayerMemoryShare = (CPlayerMemoryShare)m_MyMemoryShare;
    }

    protected override void InState()
    {
        m_OneTouch = false;
    }


    public void UpdateSpeed()
    {
        if (m_MyMemoryShare.m_TotleSpeed != m_MyMemoryShare.m_TargetTotleSpeed)
        {
           
           //m_MyMemoryShare.m_TotleSpeed = Mathf.Lerp(m_MyMemoryShare.m_TotleSpeed, m_MyMemoryShare.m_TargetTotleSpeed, m_MyPlayerMemoryShare.m_AddSpeedSecond * Time.deltaTime);
           m_MyMemoryShare.m_TotleSpeed = Mathf.MoveTowards(m_MyMemoryShare.m_TotleSpeed, m_MyMemoryShare.m_TargetTotleSpeed, m_MyPlayerMemoryShare.m_AddSpeedSecond * Time.deltaTime);
            
            if (Mathf.Abs(m_MyMemoryShare.m_TotleSpeed - m_MyMemoryShare.m_TargetTotleSpeed) < 0.1f)
                m_MyMemoryShare.m_TotleSpeed = m_MyMemoryShare.m_TargetTotleSpeed;

            m_MyPlayerMemoryShare.m_PlayerFollwer.followSpeed = m_MyMemoryShare.m_TotleSpeed;
           // m_MyPlayerMemoryShare.m_MyPlayer.UpdateCurSpeed();
        }
    }

    public void ConfirmDoorNextState(CDoorPost lsetDoorPost)
    {
        if (lsetDoorPost.NextData.m_State != StateType())
            m_MyPlayerMemoryShare.m_MyMovable.ChangState = lsetDoorPost.NextData.m_State;
    }

    public CGGameSceneData.EPostColor CurPostColor()
    {
        CGGameSceneData.EPostColor lReturnPostColor = CGGameSceneData.EPostColor.eGreenPost;

        if (m_MyPlayerMemoryShare.m_AnimationVal.Value > 0.66f)
            lReturnPostColor = m_MyPlayerMemoryShare.m_UIPostColor[0];
        else if (m_MyPlayerMemoryShare.m_AnimationVal.Value < 0.33f)
            lReturnPostColor = m_MyPlayerMemoryShare.m_UIPostColor[2];
        else
            lReturnPostColor = m_MyPlayerMemoryShare.m_UIPostColor[1];

        return lReturnPostColor;
    }

    public void TriggerEnterDoor(Collider other)
    {
        CDoorPost lTempDoorPost = other.GetComponentInParent<CDoorPost>();
        CGGameSceneData.EPostColor lTempPostColor = CurPostColor();

        if (m_MyPlayerMemoryShare.m_BuffDoorInstanceID == lTempDoorPost.gameObject.GetInstanceID())
            return;

        lTempDoorPost.CurTouch += 1;


        StaticGlobalDel.EStyle lTempCurStyle = m_MyPlayerMemoryShare.m_CurStyle;
        SetStateStyle(lTempDoorPost.NextData.m_Style);
        ConfirmDoorNextState(lTempDoorPost);

        if (lTempPostColor != lTempDoorPost.PostColor)
        {
            ShowFailFx(true);
            m_MyPlayerMemoryShare.m_MyPlayer.FeverScoreVal -= 10;

            if (lTempCurStyle == StaticGlobalDel.EStyle.eSlow)
                m_MyPlayerMemoryShare.m_MyMovable.ChangState = StaticGlobalDel.EMovableState.eHit;
        }
        else
        {
            ShowGoodFx(true);
            m_MyPlayerMemoryShare.m_MyPlayer.FeverScoreVal += 10;
        }

        m_MyPlayerMemoryShare.m_BuffDoorInstanceID = lTempDoorPost.gameObject.GetInstanceID();

        if (lTempDoorPost.TouchMaxCount == -1)
            lTempDoorPost.ShowObj(false);

        if (lTempDoorPost.gameObject.activeSelf == false)
            m_MyPlayerMemoryShare.m_MyPlayer.SetMoveStyle(lTempDoorPost.NextData.m_NextMoveStyle);
    }

    public void OpenUseGravity(bool open)
    {
        m_MyPlayerMemoryShare.m_MyRigidbody.useGravity = open;
    }

    public void SetStateStyle(StaticGlobalDel.EStyle lsetStyle)
    {
        m_MyPlayerMemoryShare.m_CurStyle = lsetStyle;
        m_MyPlayerMemoryShare.m_MyMovable.SetStateIndex(StaticGlobalDel.EMovableState.eJump, (int)m_MyPlayerMemoryShare.m_CurStyle);
    }

    public void ShowBuffAnkleLSkateboard(bool show)
    {
        m_MyPlayerMemoryShare.m_BuffAnkleLSkateboard.gameObject.SetActive(show);
        m_MyPlayerMemoryShare.m_AnkleLSkateboard.gameObject.SetActive(!show);
    }

    public void ShowGoodFx(bool show)
    {
        //for (int i = 0; i < m_MyPlayerMemoryShare.m_GoodFx.Count; i++)
        //    m_MyPlayerMemoryShare.m_GoodFx[i].SetActive(show);

        m_MyPlayerMemoryShare.m_MyMovable.AnimatorStateCtl.transform.NewFxAddParentShow(CGGameSceneData.EAllFXType.eGoodFxLight, new Vector3(0.0f, 1.6f, 0.0f));
        m_MyPlayerMemoryShare.m_MyMovable.transform.NewFxAddParentShow(CGGameSceneData.EAllFXType.eGoodFx1, new Vector3(0.0f, 2.2f, 0.0f));
    }

    public void ShowFailFx(bool show)
    {
        //for (int i = 0; i < m_MyPlayerMemoryShare.m_FailFx.Count; i++)
        //    m_MyPlayerMemoryShare.m_FailFx[i].SetActive(show);
        m_MyPlayerMemoryShare.m_MyMovable.transform.NewFxAddParentShow(CGGameSceneData.EAllFXType.eFail1Fx1, new Vector3(0.0f, 2.2f, 0.0f));
    }
}
