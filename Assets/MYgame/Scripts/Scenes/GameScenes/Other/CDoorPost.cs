using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UniRx;
using System;

[System.Serializable]
public class CDataNext
{
    public StaticGlobalDel.EStyle           m_Style             = StaticGlobalDel.EStyle.eNormal;
    public StaticGlobalDel.EMovableState    m_State             = StaticGlobalDel.EMovableState.eMove;
    public StaticGlobalDel.EMoveStyle       m_NextMoveStyle     = StaticGlobalDel.EMoveStyle.eNormal;  
}

public class CDoorPost : CGameObjBas
{
    static readonly int shPropColor = Shader.PropertyToID("_BaseColor");

    public override EObjType ObjType() { return EObjType.eDoorPost; }

    // ==================== SerializeField ===========================================

    [SerializeField] protected CGGameSceneData.EPostColor m_PostColor = CGGameSceneData.EPostColor.eYellowPost;
    public CGGameSceneData.EPostColor PostColor { get { return m_PostColor; } }

    //[SerializeField] protected StaticGlobalDel.EMovableState m_NextState = StaticGlobalDel.EMovableState.eMove;
    //public StaticGlobalDel.EMovableState NextState { get { return m_NextState; } }

    //  [SerializeField] protected Renderer m_MyObjRenderer = null;
    [SerializeField] protected GameObject[] m_AllPosObj = null;
    [SerializeField] protected MeshRenderer m_SpecialPosObj = null;

    [SerializeField] protected int m_TouchMaxCount = -1;
    public int TouchMaxCount { get { return m_TouchMaxCount; } }

    //[SerializeField] protected EStyle m_NextStyle = StaticGlobalDel.EMovableState.eMove;
    //public StaticGlobalDel.EMovableState NextState { get { return m_NextState; } }
    [SerializeField] protected CDataNext m_NextData;
    public CDataNext NextData { get { return m_NextData; } }

    [SerializeField]protected StaticGlobalDel.EMoveStyle m_MyMoveStyle = StaticGlobalDel.EMoveStyle.eNormal;
    [SerializeField]protected ColorType m_MyColorType = null;

    readonly protected string[] m_AnimationTag = { "YellowPos", "PinkPos", "BluePos", "GreenPos", "OrangePos" };
    // ==================== SerializeField ===========================================
    protected CDoorGroupPost m_MyDoorGroupPost = null;

    protected CDoorPost m_NextDoorPost = null;
    public CDoorPost NextDoorPost
    {
        set { m_NextDoorPost = value; }
        get { return m_NextDoorPost; }
    }

    MaterialPropertyBlock mpb;
    public MaterialPropertyBlock Mpb
    {
        get
        {
            if (mpb == null)
                mpb = new MaterialPropertyBlock();
            return mpb;
        }
    }

    protected Animator m_AnimatorPost = null;
    protected Outline m_MyOutline = null;
    protected int m_CurTouch = 0;
    public int CurTouch
    {
        set
        {
            m_CurTouch = value;
            if (TouchMaxCount == -1)
                return;

            if (TouchMaxCount <= m_CurTouch)
                ShowObj(false);
        }
        get { return m_CurTouch; }
    }

    private void OnValidate(){UpdatePos(); }

    protected override void Awake()
    {
        base.Awake();
        string lTempTag = m_AnimationTag[(int)m_PostColor];

        m_MyDoorGroupPost = this.GetComponentInParent<CDoorGroupPost>();
        if (m_MyDoorGroupPost != null)
            m_TouchMaxCount = -1;

        UpdatePos();
        //float lTemppostVal = 0.0f;
    }

    // Start is called before the first frame update
    protected override void Start()
    {
    }

    // Update is called once per frame
    protected override void Update()
    {
        
    }

    public void ShowObj(bool show)
    {
        this.gameObject.SetActive(show);
    }

    public void TouchOK(CPlayer lTempAddScore)
    {
        if (m_MyDoorGroupPost == null)
            lTempAddScore.FeverScoreVal += StaticGlobalDel.g_AddFever;
    }

    public void OnEnable()
    {
        m_MyGameManager.AddDoorPost(this);

        if (m_MyDoorGroupPost != null)
            m_MyDoorGroupPost.AddDoorPost(this);
    }
    public void OnDisable()
    {
        if (m_MyDoorGroupPost != null)
            m_MyDoorGroupPost.RemoveDoorPost(this);
    }

    public void UpdatePos()
    {
        for (int i = 0; i < m_AllPosObj.Length; i++)
            m_AllPosObj[i].SetActive(false);

        if (m_MyMoveStyle == StaticGlobalDel.EMoveStyle.eNormal)
            m_AllPosObj[(int)m_PostColor].SetActive(true);
        else
        {
            m_SpecialPosObj.gameObject.SetActive(true);
            Mpb.SetColor(shPropColor, m_MyColorType.m_AllPostColor[(int)m_PostColor]);
            m_SpecialPosObj.SetPropertyBlock(Mpb);
        }
    }
}
