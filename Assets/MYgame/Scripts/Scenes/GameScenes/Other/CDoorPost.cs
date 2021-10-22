using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UniRx;
using System;

public class CDoorPost : CGameObjBas
{
    public const string CAnimatorNamtTag = "MoveBlendTree";

    public override EObjType ObjType() { return EObjType.eDoorPost; }

    // ==================== SerializeField ===========================================

    [SerializeField] protected CGGameSceneData.EPostColor m_PostColor = CGGameSceneData.EPostColor.eYellowPost;
    public CGGameSceneData.EPostColor PostColor { get { return m_PostColor; } }

    [SerializeField] protected StaticGlobalDel.EMovableState m_NextState = StaticGlobalDel.EMovableState.eMove;
    public StaticGlobalDel.EMovableState NextState { get { return m_NextState; } }

    [SerializeField] protected Renderer m_MyObjRenderer = null;

    [SerializeField] protected int m_TouchMaxCount = -1;
    public int TouchMaxCount { get { return m_TouchMaxCount; } }

    readonly protected string[] m_AnimationTag = { "YellowPos", "PinkPos", "BluePos", "GreenPos", "OrangePos" };
    // ==================== SerializeField ===========================================
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

    

    protected override void Awake()
    {
        m_AnimatorPost = this.GetComponentInChildren<Animator>();
        // m_MyOutline = this.GetComponentInChildren<Outline>();
        m_MyObjRenderer.gameObject.layer = CGGameSceneData.SharedInstance.m_AllLayerOutlineColor[(int)m_PostColor];
        string lTempTag = m_AnimationTag[(int)m_PostColor];

        m_AnimatorPost.SetTrigger(lTempTag);
        //float lTemppostVal = 0.0f;

        //if (m_PostColor == CGGameSceneData.EPostColor.eYellowPost)
        //    lTemppostVal = 1.0f;
        //else if (m_PostColor == CGGameSceneData.EPostColor.eGreenPost)
        //    lTemppostVal = 0.5f;
        //else if (m_PostColor == CGGameSceneData.EPostColor.eOrangePost)
        //    lTemppostVal = 0.0f;

        //m_AnimatorPost.SetFloat("MoveVal", lTemppostVal);
    }

    // Start is called before the first frame update
    protected override void Start()
    {
        
        //Color lTempColor = CGGameSceneData.SharedInstance.PostColorToColor(m_PostColor);
        //m_MyOutline.SetOutlineColor = lTempColor;
        UniRx.Observable.Timer(TimeSpan.FromSeconds(0.5f)).Subscribe(_ => 
        {
            m_AnimatorPost.enabled = false;
        });
    }

    // Update is called once per frame
    protected override void Update()
    {
        
    }

    public void ShowObj(bool show)
    {
        this.gameObject.SetActive(show);
    }
}
