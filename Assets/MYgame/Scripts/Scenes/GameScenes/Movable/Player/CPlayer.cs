using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cinemachine;
using DG.Tweening;
using Dreamteck.Splines;
using UniRx;

public class CPlayerMemoryShare : CActorMemoryShare
{
    public bool                             m_bDown                     = false;
    public Vector3                          m_OldMouseDownPos           = Vector3.zero;
    public Vector3                          m_OldMouseDragDirNormal     = Vector3.zero;
    public CPlayer                          m_MyPlayer                  = null;
    public Vector3[]                        m_AllPathPoint              = new Vector3[8];
    public int                              m_CurStandPointindex        = 0;
    public Vector3                          m_TargetStandPoint          = Vector3.zero;
    public Collider                         m_SwordeCollider            = null;
    public SplineFollower                   m_PlayerFollwer             = null;
    public UniRx.ReactiveProperty<float>    m_AnimationVal              = new ReactiveProperty<float>(0.5f);
    public CGGameSceneData.EPostColor[]     m_UIPostColor               = null;
    public float                            m_AddSpeedSecond            = 5.0f;
    public Transform                        m_GSkateboard               = null;
    public Transform                        m_AnkleLSkateboard          = null;
    public Transform                        m_BuffAnkleLSkateboard      = null;
    public Vector3                          m_AnkleLSkateboardLocalpos  = Vector3.zero;
    public Quaternion                       m_AnkleLSkateboardRotate    = Quaternion.identity;
    public int                              m_BuffDoorInstanceID        = 0;
    public bool                             m_isupdateAnimation         = true;
    // public float                    m_        = null;
};

public class CPlayer : CActor
{
    public override EMovableType MyMovableType() { return EMovableType.ePlayer; }

    protected float m_MaxMoveDirSize = 0;

    protected CPlayerMemoryShare m_MyPlayerMemoryShare = null;

    // ==================== SerializeField ===========================================

    [SerializeField] protected Transform m_GSkateboard = null;
    [SerializeField] protected Transform m_AnkleLSkateboard = null;
    [SerializeField] protected Transform m_BuffAnkleLSkateboard = null;


    // ==================== SerializeField ===========================================



    public float AnimationVal
    {
        set {
                float lTempValue = Mathf.Clamp(value, 0.0f, 1.0f);
                m_MyPlayerMemoryShare.m_AnimationVal.Value = lTempValue;
            }
        get { return m_MyPlayerMemoryShare.m_AnimationVal.Value; }
    }

    protected Vector3 m_OldMouseDragDir = Vector3.zero;

    int m_MoveingHash = 0;
 

    protected override void AddInitState()
    {
        m_AllState[(int)StaticGlobalDel.EMovableState.eWait].AllThisState.Add(new CWaitStatePlayer(this));
        m_AllState[(int)StaticGlobalDel.EMovableState.eMove].AllThisState.Add(new CMoveStatePlayer(this));

        m_AllState[(int)StaticGlobalDel.EMovableState.eHit].AllThisState.Add(new CHitStatePlayer(this));
        m_AllState[(int)StaticGlobalDel.EMovableState.eHit].AllThisState.Add(new CHitJumpStatePlayer(this));

        m_AllState[(int)StaticGlobalDel.EMovableState.eJump].AllThisState.Add(new CJumpNormalStatePlayer(this));
        m_AllState[(int)StaticGlobalDel.EMovableState.eJump].AllThisState.Add(new CJumpStatePlayer(this));
        m_AllState[(int)StaticGlobalDel.EMovableState.eJumpDown].AllThisState.Add(new CJumpDownStatePlayer(this));

        m_AllState[(int)StaticGlobalDel.EMovableState.eFinish].AllThisState.Add(new CFinishStatePlayer(this));

    }

    protected override void CreateMemoryShare()
    {
        m_MyPlayerMemoryShare = new CPlayerMemoryShare();
        m_MyMemoryShare = m_MyPlayerMemoryShare;

        //m_MyPlayerMemoryShare.m_PlayerNormalCamera  = m_PlayerNormalCamera;
        //m_MyPlayerMemoryShare.m_PlayerWinLoseCamera = m_PlayerWinLoseCamera;
        m_MyPlayerMemoryShare.m_MyPlayer                    = this;
        m_MyPlayerMemoryShare.m_PlayerFollwer               = this.GetComponent<SplineFollower>();
        m_MyPlayerMemoryShare.m_UIPostColor                 = m_MyGameManager.UIPostColor;
        m_MyPlayerMemoryShare.m_GSkateboard                 = m_GSkateboard;
        m_MyPlayerMemoryShare.m_AnkleLSkateboard            = m_AnkleLSkateboard;
        m_MyPlayerMemoryShare.m_BuffAnkleLSkateboard        = m_BuffAnkleLSkateboard;


        m_MoveingHash = Animator.StringToHash("MoveVal");

        base.CreateMemoryShare();

        SetBaseMemoryShare();

        m_MaxMoveDirSize = Screen.width > Screen.height ? (float)Screen.width : (float)Screen.height;
        m_MaxMoveDirSize = m_MaxMoveDirSize / 5.0f;
    }

    // Start is called before the first frame update
    protected override void Start()
    {
        base.Start();
        SetCurState(StaticGlobalDel.EMovableState.eWait);

        UpdateAnimationVal().Subscribe(_ => {
            UpdateAnimationChangVal();
        }).AddTo(this.gameObject);

        m_MyPlayerMemoryShare.m_AnkleLSkateboardLocalpos = m_AnkleLSkateboard.localPosition;
        m_MyPlayerMemoryShare.m_AnkleLSkateboardRotate = m_AnkleLSkateboard.rotation;
    }

    public void UpdateAnimationChangVal()
    {
        if (m_MyPlayerMemoryShare.m_isupdateAnimation)
            m_AnimatorStateCtl.SetFloat(m_MoveingHash, m_MyPlayerMemoryShare.m_AnimationVal.Value);
    }

    // Update is called once per frame
    protected override void Update()
    {
        base.Update();

        //if (m_MyGameManager.CurState == CGameManager.EState.ePlay || m_MyGameManager.CurState == CGameManager.EState.eReady)
            InputUpdata();


        //transform.Translate(new Vector3(0.0f, 0.0f, Time.deltaTime * 3.0f));
       // m_MyPlayerMemoryShare.m_DamiCameraFollwer.
        //CGameSceneWindow lTempGameSceneWindow = CGameSceneWindow.SharedInstance;
        //if (lTempGameSceneWindow && lTempGameSceneWindow.GetShow())
        //    lTempGameSceneWindow.SetBouncingBedCount(m_MyGameManager.GetFloorBouncingBedBoxCount(m_MyMemoryShare.m_FloorNumber));
    }

    public override void InputUpdata()
    {
        if (Input.GetMouseButtonDown(0))
        {
            PlayerMouseDown();
        }
        else if (Input.GetMouseButton(0))
        {
            PlayerMouseDrag();
        }
        else if (Input.GetMouseButtonUp(0))
        {
            PlayerMouseUp();
        }
    }

    public void PlayerMouseDown()
    {
        //if (!PlayerCtrl())
        //{
        //    if (m_CurState != StaticGlobalDel.EMovableState.eNull && m_AllState[(int)m_CurState] != null)
        //    {
        //        m_AllState[(int)m_CurState].MouseDown();
        //    }
        //}

        DataState lTempDataState = m_AllState[(int)CurState];
        if (m_CurState != StaticGlobalDel.EMovableState.eNull && lTempDataState != null && lTempDataState.AllThisState[lTempDataState.index] != null)
            lTempDataState.AllThisState[lTempDataState.index].MouseDown();

        m_MyPlayerMemoryShare.m_bDown = true;
        m_MyPlayerMemoryShare.m_OldMouseDownPos = Input.mousePosition;
    }

    public void PlayerMouseDrag()
    {
        //if (!PlayerCtrl())
        //    return;
        if (!m_MyPlayerMemoryShare.m_bDown)
            return;

        DataState lTempDataState = m_AllState[(int)CurState];
        if (m_CurState != StaticGlobalDel.EMovableState.eNull && lTempDataState != null && lTempDataState.AllThisState[lTempDataState.index] != null)
            lTempDataState.AllThisState[lTempDataState.index].MouseDrag();

        m_MyPlayerMemoryShare.m_OldMouseDownPos = Input.mousePosition;
    }

    public void PlayerMouseUp()
    {
        if (m_MyPlayerMemoryShare.m_bDown)
        {
            DataState lTempDataState = m_AllState[(int)CurState];
            if (m_CurState != StaticGlobalDel.EMovableState.eNull && lTempDataState != null && lTempDataState.AllThisState[lTempDataState.index] != null)
                lTempDataState.AllThisState[lTempDataState.index].MouseUp();

            m_MyPlayerMemoryShare.m_bDown = false;
            m_MyPlayerMemoryShare.m_OldMouseDownPos = Vector3.zero;
        }
    }

    public override void UpdateCurSpeed()
    {
        base.UpdateCurSpeed();
        m_MyPlayerMemoryShare.m_PlayerFollwer.followSpeed = m_MyMemoryShare.m_TargetTotleSpeed;
    }

    public override void OnTriggerEnter(Collider other)
    {
        base.OnTriggerEnter(other);
    }

    public void MouseDrag()
    {
        float lTempMoveY = Input.mousePosition.y - m_MyPlayerMemoryShare.m_OldMouseDownPos.y;
        float lTempMoveRatio = m_MaxMoveDirSize;

        lTempMoveY = lTempMoveY / m_MaxMoveDirSize;

        m_MyPlayerMemoryShare.m_MyPlayer.AnimationVal += lTempMoveY;
        CGameSceneWindow.SharedInstance.SetVal(m_MyPlayerMemoryShare.m_MyPlayer.AnimationVal);
    }

    public void SetMoveStyle(StaticGlobalDel.EMoveStyle lTempStyle)
    {
        m_AnimatorStateCtl.SetStateIndividualIndex( CAnimatorStateCtl.EState.eRun, (int)lTempStyle);
    }

    // ===================== UniRx ======================

    public UniRx.ReactiveProperty<float> UpdateAnimationVal()
    {
        return m_MyPlayerMemoryShare.m_AnimationVal ?? (m_MyPlayerMemoryShare.m_AnimationVal = new ReactiveProperty<float>(0.5f));
    }
    // ===================== UniRx ======================
}
