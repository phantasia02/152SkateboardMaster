using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CDoorGroupPost : CGameObjBas
{
    public override EObjType ObjType() { return EObjType.eDoorGroupPost; }

    protected List<CDoorPost> m_AllDoorPost = new List<CDoorPost>();
    public List<CDoorPost> AllDoorPost { get { return m_AllDoorPost; } }


    protected override void Awake()
    {
        base.Awake();
    }

    public void AddDoorPost(CDoorPost lAddDoorPost) { m_AllDoorPost.Add(lAddDoorPost); }
    public void RemoveDoorPost(CDoorPost lRemoveDoorPost)
    {
        m_AllDoorPost.Remove(lRemoveDoorPost);
        if (m_AllDoorPost.Count == 0)
            m_MyGameManager.Player.FeverScoreVal += StaticGlobalDel.g_AddFever;
    }
}
