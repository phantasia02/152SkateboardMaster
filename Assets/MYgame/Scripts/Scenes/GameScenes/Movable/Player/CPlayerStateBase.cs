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
        //Debug.Log($"===================================");
        //Debug.Log($"lTempPostColor = {lTempPostColor}");
        //Debug.Log($" lTempDoorPost.PostColor = { lTempDoorPost.PostColor}");
        if (m_MyPlayerMemoryShare.m_BuffDoorInstanceID == lTempDoorPost.gameObject.GetInstanceID())
            return;

        //  Debug.Log($" m_MyPlayerMemoryShare.m_BuffDoorInstanceID = { m_MyPlayerMemoryShare.m_BuffDoorInstanceID}");
        // other.gameObject.SetActive(false);
        lTempDoorPost.CurTouch += 1;

        if (lTempPostColor != lTempDoorPost.PostColor)
            m_MyPlayerMemoryShare.m_MyMovable.ChangState = StaticGlobalDel.EMovableState.eHit;
        else
        {
            SetStateStyle(lTempDoorPost.NextData.m_Style);
            ConfirmDoorNextState(lTempDoorPost);
            m_MyPlayerMemoryShare.m_BuffDoorInstanceID = lTempDoorPost.gameObject.GetInstanceID();

            if (lTempDoorPost.TouchMaxCount == -1)
                lTempDoorPost.ShowObj(false);
        }

        if (lTempDoorPost.gameObject.activeSelf == false)
            m_MyPlayerMemoryShare.m_MyPlayer.SetMoveStyle(lTempDoorPost.NextData.m_NextMoveStyle);
    }

    public void UpdateUIMapBar()
    {

    }

    public void OpenUseGravity(bool open)
    {
        m_MyPlayerMemoryShare.m_MyRigidbody.useGravity = open;
    }

    public void SetStateStyle(StaticGlobalDel.EStyle lsetStyle)
    {
        m_MyPlayerMemoryShare.m_MyMovable.SetStateIndex(StaticGlobalDel.EMovableState.eJump, (int)lsetStyle);
    }

    public void ShowBuffAnkleLSkateboard(bool show)
    {
        m_MyPlayerMemoryShare.m_BuffAnkleLSkateboard.gameObject.SetActive(show);
        m_MyPlayerMemoryShare.m_AnkleLSkateboard.gameObject.SetActive(!show);
    }
}
