using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class CDataInitScoringBox
{
    public Color    m_Color     = Color.white;
    public int      m_Number    = 0;
}

public class CScoringBox : CGameObjBas
{
    static readonly int shPropColor = Shader.PropertyToID("_BaseColor");

    public override EObjType ObjType() { return EObjType.eScoringBox; }

    protected int m_RateNumber = 1;
    protected TMPro.TextMeshPro m_MyTextMesh   = null;

    //private void Awake()
    //{

    //}

    public void InitSet(CDataInitScoringBox lTempSetData)
    {
        m_RateNumber = lTempSetData.m_Number;

        m_MyTextMesh = this.GetComponentInChildren<TMPro.TextMeshPro>();
        MeshRenderer lTempMesh = this.GetComponent<MeshRenderer>();

        lTempMesh.material.SetColor(shPropColor, lTempSetData.m_Color);

        m_MyTextMesh.text = $"X {m_RateNumber.ToString()}";
    }
}
