using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CMoveStateAudience : CAudienceStateBase
{

    const float CTargetSqrDis = 16.0f;
    public override StaticGlobalDel.EMovableState StateType() { return StaticGlobalDel.EMovableState.eMove; }

    public CMoveStateAudience(CMovableBase pamMovableBase) : base(pamMovableBase)
    {

    }

    protected override void InState()
    {
        SetAnimationState(CAnimatorStateCtl.EState.eRun);
    }

    protected override void updataState()
    {
        Vector3 lTargetDir = Vector3.zero - m_MyAudienceMemoryShare.m_MyTransform.localPosition;
        lTargetDir.y = 0.0f;
        lTargetDir.Normalize();
        m_MyAudienceMemoryShare.m_MyTransform.forward = Vector3.Lerp(m_MyAudienceMemoryShare.m_MyTransform.forward, lTargetDir, Time.deltaTime * 2.0f);

        Vector3 lTargetDis = m_MyAudienceMemoryShare.m_MyTransform.localPosition;
        lTargetDis.y = 0.0f;
        if (lTargetDis.sqrMagnitude <= CTargetSqrDis)
        {
            m_MyAudienceMemoryShare.m_MyMovable.ChangState = StaticGlobalDel.EMovableState.eWin;
            return;
        }


        m_MyAudienceMemoryShare.m_MyTransform.Translate(new Vector3(0.0f, 0.0f, Time.deltaTime * m_MyAudienceMemoryShare.m_MyMovable.DefSpeed));
        // m_MyAudienceMemoryShare.m_MyMovable.ChangState = StaticGlobalDel.EMovableState.eWait;
    }

    protected override void OutState()
    {

    }
}
