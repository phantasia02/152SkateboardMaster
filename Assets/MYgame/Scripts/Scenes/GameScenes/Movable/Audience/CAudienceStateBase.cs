using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class CAudienceStateBase : CMovableStatePototype
{
    protected CAudienceMemoryShare m_MyAudienceMemoryShare = null;

    public CAudienceStateBase(CMovableBase pamMovableBase) : base(pamMovableBase)
    {
        m_MyAudienceMemoryShare = (CAudienceMemoryShare)m_MyMemoryShare;
    }
}
