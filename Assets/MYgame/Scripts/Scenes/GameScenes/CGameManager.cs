using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using Cinemachine;
using UniRx;
using Dreamteck.Splines;

public class CGameManager : MonoBehaviour
{
    public enum EState
    {
        eReady              = 0,
        ePlay               = 1,
        ePlayHold           = 2,
        ePlayOKPerformance  = 3,
        eReadyEnd           = 4,
        eNextEnd            = 5,
        eGameOver           = 6,
        eWinUI              = 7,
        eMax
    };

    public enum EQAData
    {
        eQ_DataIndex = 0,
        eA_DataIndex = 1,
        eMax
    };

    bool m_bInitOK = false;
    bool m_bDown = false;

    CChangeScenes m_ChangeScenes = new CChangeScenes();
    protected ResultUI m_MyResultUI = null;
    public ResultUI MyResultUI { get { return m_MyResultUI; } }

    protected Camera m_Camera = null;
    public Camera MainCamera { get { return m_Camera; } }

    protected CPlayer m_Player = null;
    public CPlayer Player { get { return m_Player; } }
    // ==================== SerializeField ===========================================

    [SerializeField] protected SplineFollower m_EndLastSplineFollower = null;
    public SplineFollower EndLastSplineFollower { get { return m_EndLastSplineFollower; } }

    [SerializeField] protected Transform m_AllAudience = null;
    public Transform AllAudience { get { return m_AllAudience; } }

    [SerializeField] GameObject m_WinCamera = null;
    public GameObject WinCamera { get { return m_WinCamera; } }

    [VarRename(new string[] { "Top", "Middle", "Down" })]
    [SerializeField] protected CGGameSceneData.EPostColor[] m_UIPostColor = new CGGameSceneData.EPostColor[3];
    public CGGameSceneData.EPostColor[] UIPostColor { get { return m_UIPostColor; } }

    // ==================== SerializeField ===========================================

    public List<CDoorPost> m_AllDoorPost = new List<CDoorPost>();
    public List<CDoorPost> AllDoorPost { get { return m_AllDoorPost; } }

    private EState m_eCurState = EState.eReady;
    public EState CurState { get { return m_eCurState; } }
    protected float m_StateTime = 0.0f;
    protected float m_StateUnscaledTime = 0.0f;
    protected int m_StateCount = 0;
    protected Vector3 m_OldInput;
    protected float m_HalfScreenWidth = 600.0f;


    void Awake()
    {
        Application.targetFrameRate = 60;
        const float HWRatioPototype = StaticGlobalDel.g_fcbaseHeight / StaticGlobalDel.g_fcbaseWidth;
        float lTempNewHWRatio = ((float)Screen.height / (float)Screen.width);
        m_HalfScreenWidth = (StaticGlobalDel.g_fcbaseWidth / 2.0f) * (lTempNewHWRatio / HWRatioPototype);

        m_Player = this.GetComponentInChildren<CPlayer>();
        m_MyResultUI = gameObject.GetComponentInChildren<ResultUI>();

        if (m_MyResultUI != null)
        {
            m_MyResultUI.NextButton.onClick.AddListener(OnNext);
        }
    }

    // Start is called before the first frame update
    IEnumerator Start()
    {
        do
        {
            GameObject lTempCameraObj = GameObject.FindGameObjectWithTag("MainCamera");
            if (lTempCameraObj != null)
                m_Camera = lTempCameraObj.GetComponent<Camera>();

            

            if (m_Camera != null )
            {
                m_bInitOK = true;
            }

            yield return null;

        } while (!m_bInitOK);

        Observable.EveryUpdate().First(_ => Input.GetMouseButtonDown(0)).Subscribe( 
        OnNext  => 
        {
            if (m_eCurState == EState.eReady)
            {
                SetState(EState.ePlay);

                CReadyGameWindow lTempCReadyGameWindow = CReadyGameWindow.SharedInstance;
                if (lTempCReadyGameWindow && lTempCReadyGameWindow.GetShow())
                    lTempCReadyGameWindow.CloseShowUI();

                CGameSceneWindow lTempGameSceneWindow = CGameSceneWindow.SharedInstance;
                if (lTempGameSceneWindow)
                {
                    lTempGameSceneWindow.ShowObj(true);
                    lTempGameSceneWindow.ShowReadyUI(false);
                }
            }
        },
        OnCompleted =>{Debug.Log("OK");}
        ).AddTo(this);

        m_AllDoorPost.Sort((ObjX, ObjY) =>
        {
            return ObjX.transform.position.z.CompareTo(ObjY.transform.position.z);
        });

        for (int i = m_AllDoorPost.Count - 1; i > 0; i--)
            m_AllDoorPost[i - 1].NextDoorPost = m_AllDoorPost[i];
    }

    // Update is called once per frame
    void Update()
    {
        if (!m_bInitOK)
            return;

  
        m_StateTime += Time.deltaTime;
        m_StateCount++;
        m_StateUnscaledTime += Time.unscaledDeltaTime;

        switch (m_eCurState)
        {
            case EState.eReady:
                {
                 //   UsePlayTick();
                }
                break;
            case EState.ePlay:
                {
                   // UsePlayTick();
                }
                break;
            case EState.ePlayHold:
                {

                }
                break;
            case EState.ePlayOKPerformance:
                {
                }
                break;

            case EState.eReadyEnd:
                {
                }
                break;


            case EState.eNextEnd:
                {
                }
                break;
            case EState.eGameOver:
                {

                }
                break;
        }
    }

    public void SetState(EState lsetState)
    {
        if (lsetState == m_eCurState)
            return;

        EState lOldState = m_eCurState;
        m_StateTime = 0.0f;
        m_StateCount = 0;
        m_StateUnscaledTime = 0.0f;
        m_eCurState = lsetState;

        CGameSceneWindow lTempGameSceneWindow = CGameSceneWindow.SharedInstance;

        switch (m_eCurState)
        {
            case EState.eReady:
                {
                }
                break;
            case EState.ePlay:
                {
                    if (lTempGameSceneWindow != null)
                        lTempGameSceneWindow.SetGoButton(CGameSceneWindow.EButtonState.eNormal);
                }
                break;
            case EState.ePlayHold:
                {
                    if (lTempGameSceneWindow != null)
                        lTempGameSceneWindow.SetGoButton(CGameSceneWindow.EButtonState.eDisable);
                }
                break;
            case EState.ePlayOKPerformance:
                {
                }
                break;
            case EState.eReadyEnd:
                {
                }
                break;
            case EState.eNextEnd:
                {

                }
                break;
            case EState.eWinUI:
                {
                    if (lTempGameSceneWindow)
                    {
                       // lTempGameSceneWindow.ShowObj(false);
                    }
                    m_MyResultUI.ShowSuccessUI(4.0f);
                }
                break;
            case EState.eGameOver:
                {
                    //if (lTempGameSceneWindow)
                    //    lTempGameSceneWindow.ShowObj(false);

                    //m_MyResultUI.ShowFailedUI(1.0f);
                }
                break;
        }
    }

    public void UsePlayTick()
    {
//#if UNITY_EDITOR
        if (Input.GetMouseButtonDown(0))
        {
            m_bDown = true;
            m_OldInput = Input.mousePosition;
            //InputRay();
        }
        else if (Input.GetMouseButton(0))
        {
            //float moveX = (Input.mousePosition.x - m_OldInput.x) / m_HalfScreenWidth;
            //m_Player.SetXMove(moveX);
            //m_OldInput = Input.mousePosition;
        }
        else if (Input.GetMouseButtonUp(0))
        {
            if (m_bDown)
            {
                m_OldInput = Vector3.zero;
                m_bDown = false;
            }
        }

//#elif UNITY_ANDROID
//        testbugtext.Add("UsePlayTick 2222222222222222222222");
//        if (Input.touchCount == 1)
//        {
//        testbugtext.Add("UsePlayTick 3333333333333333");
//            Touch touch = Input.GetTouch(0);
//            bool isTouchUIElement = EventSystem.current.IsPointerOverGameObject(touch.fingerId);

//            if (!isTouchUIElement)
//            {
//                switch (touch.phase)
//                {
//                    case TouchPhase.Began:
//                        {
//        testbugtext.Add("UsePlayTick 4444444444444444444");
//                            m_bDown = true;
//                            m_OldInput = Input.mousePosition;
//                        }
//                        break;
//                    case TouchPhase.Moved:
//                        {
//                            m_OldInput = Input.mousePosition;
//                        }
//                        break;
//                    case TouchPhase.Ended:
//                        {
//                            if (m_bDown)
//                            {
//                                m_bDown = false;
//                            }
//                        }
//                        break;

//                }
//            }
//        }
//#endif


        //if (m_eCurState == EState.eReady)
        //{
        //    if (m_bDown)
        //    {
        //        SetState(EState.ePlay);

        //        CReadyGameWindow lTempCReadyGameWindow = CReadyGameWindow.SharedInstance;
        //        if (lTempCReadyGameWindow && lTempCReadyGameWindow.GetShow())
        //            lTempCReadyGameWindow.CloseShowUI();

        //        CGameSceneWindow lTempGameSceneWindow = CGameSceneWindow.SharedInstance;
        //        if (lTempGameSceneWindow && !lTempGameSceneWindow.GetShow())
        //            lTempGameSceneWindow.ShowObj(true);
        //    }
        //}

    }
    private GUIStyle guiStyle = new GUIStyle(); //create a new variable
    List<string> testbugtext = new List<string>();
    private void OnGUI()
    {
        string test = "";
        for (int i = testbugtext.Count - 1; i >= 0; i--)
            test += $"{testbugtext[i]}\n";

        guiStyle.fontSize = 60; //change the font size
        GUI.Label(new Rect(10, 10, 1000, 2000), test, guiStyle);
    }

    public void InputRay()
    {
        
    }

    public void OnNext()
    {
        m_ChangeScenes.LoadGameScenes();
    }

    public void OnReset()
    {
        m_ChangeScenes.ResetScene();
    }

    public void AddDoorPost(CDoorPost lAddDoorPost) { m_AllDoorPost.Add(lAddDoorPost); }
    public void RemoveDoorPost(CDoorPost lRemoveDoorPost){m_AllDoorPost.Remove(lRemoveDoorPost);}
}
