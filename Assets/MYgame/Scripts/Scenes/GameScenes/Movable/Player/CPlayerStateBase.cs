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
            m_MyMemoryShare.m_TotleSpeed = Mathf.Lerp(m_MyMemoryShare.m_TotleSpeed, m_MyMemoryShare.m_TargetTotleSpeed, 5.0f * Time.deltaTime);

            if (Mathf.Abs(m_MyMemoryShare.m_TotleSpeed - m_MyMemoryShare.m_TargetTotleSpeed) < 0.01f)
                m_MyMemoryShare.m_TotleSpeed = m_MyMemoryShare.m_TargetTotleSpeed;

            m_MyPlayerMemoryShare.m_PlayerFollwer.followSpeed = m_MyMemoryShare.m_TotleSpeed;
           // m_MyPlayerMemoryShare.m_MyPlayer.UpdateCurSpeed();
        }
    }
}
