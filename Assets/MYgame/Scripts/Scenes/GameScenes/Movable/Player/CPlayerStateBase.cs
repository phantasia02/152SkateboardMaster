using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class CPlayerStateBase : CMovableStatePototype
{
    protected CPlayerMemoryShare m_MyPlayerMemoryShare = null;

    public CPlayerStateBase(CMovableBase pamMovableBase) : base(pamMovableBase)
    {
        m_MyPlayerMemoryShare = (CPlayerMemoryShare)m_MyMemoryShare;
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
        if (lsetDoorPost.NextState != StateType())
            m_MyPlayerMemoryShare.m_MyMovable.ChangState = lsetDoorPost.NextState;
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

        if (lTempPostColor != lTempDoorPost.PostColor)
            m_MyPlayerMemoryShare.m_MyMovable.ChangState = StaticGlobalDel.EMovableState.eHit;
        else
            ConfirmDoorNextState(lTempDoorPost);
    }
}
