using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CDoorGroupPost : CGameObjBas
{
    public override EObjType ObjType() { return EObjType.eDoorGroupPost; }

    protected List<CDoorPost> m_ChildAllDoorPost = new List<CDoorPost>();
    public List<CDoorPost> AllDoorPost { get { return m_ChildAllDoorPost; } }


    protected override void Awake()
    {
        base.Awake();
    }

    public void AddDoorPost(CDoorPost lAddDoorPost) { m_ChildAllDoorPost.Add(lAddDoorPost); }
    public void RemoveDoorPost(CDoorPost lRemoveDoorPost)
    {
        m_ChildAllDoorPost.Remove(lRemoveDoorPost);
        if (m_ChildAllDoorPost.Count == 0)
            m_MyGameManager.Player.FeverScoreVal += StaticGlobalDel.g_AddFever;
    }
}
