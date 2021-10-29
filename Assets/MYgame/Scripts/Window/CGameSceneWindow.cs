using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;
using UniRx;

public class CGameSceneWindow : CSingletonMonoBehaviour<CGameSceneWindow>
{
    public enum EButtonState
    {
        eNormal     = 0,
        eDisable    = 1,
        eHide       = 2,
    }

    CChangeScenes m_ChangeScenes = new CChangeScenes();
    [SerializeField] GameObject m_ShowObj       = null;
    [SerializeField] Button m_ResetButton       = null;
    [SerializeField] Button m_GoButton          = null;
    [SerializeField] Image  m_MoveBar           = null;
    [SerializeField] Image  m_MapProgress       = null;
    [SerializeField] Image[] m_FeverBar         = null;
    [SerializeField] Image m_MaxFeverBar        = null;
    [SerializeField] RectTransform m_ReadyUI    = null;
    [SerializeField] RectTransform m_GamePlayUI = null;

    [SerializeField] [Range(0.0f, 1.0f)] protected float m_InitAnimationVal = 0.5f;

    //const float
    protected float m_CurFever      = 0.0f;
    protected float m_TargetFever   = 0.0f;
    protected Color m_TargetColor   = Color.white;


    private void OnValidate()
    {
        // ApplyColor();
        SetVal(m_InitAnimationVal);
    }

    public void SetTemptextPos(Vector3 pos)
    {
    }

    private void Awake()
    {
        m_ResetButton.onClick.AddListener(() => {
            m_ChangeScenes.ResetScene();
        });

        m_GoButton.onClick.AddListener(() => {
            SetGoButton( EButtonState.eHide);
        });
    }

    // Start is called before the first frame update
    void Start()
    {
    }


    // Update is called once per frame
    void Update()
    {
        if (Mathf.Abs(m_CurFever - m_TargetFever) > 0.001f)
            UpdateFeverBar();
        else
            m_CurFever = m_TargetFever;
    }

    public void UpdateFeverBar()
    {
        float lTempCurFever = Mathf.Lerp(m_CurFever, m_TargetFever, Time.deltaTime * 5.0f);
        int lTempIndex = (int)(lTempCurFever / 0.33f);
        float lTempRatio = lTempCurFever % 0.33f;
        lTempRatio = lTempRatio / 0.33f;
     
        for (int i = 0; i < lTempIndex; i++)
            m_FeverBar[i].fillAmount = 1.0f;

        if (lTempIndex < m_FeverBar.Length)
            m_FeverBar[lTempIndex].fillAmount = lTempRatio;

        m_CurFever = lTempCurFever;
    }

    public bool GetShow() { return m_ShowObj.activeSelf; }
    public void ShowObj(bool showObj)
    {
        if (showObj)
        {

        }
        m_ShowObj.SetActive(showObj);
        // CGameSceneWindow.SharedInstance.SetCurState(CGameSceneWindow.EState.eEndStop);
    }

    public void SetGoButton(EButtonState settype)
    {
        if (settype == EButtonState.eDisable)
        {
            m_GoButton.gameObject.SetActive(true);
            m_GoButton.interactable = false;
        }
        else if (settype == EButtonState.eHide)
        {
            m_GoButton.gameObject.SetActive(false);
        }
    }

    public void SetVal(float Val)
    {
        Val = Mathf.Clamp(Val, 0.0f, 1.0f);

        Val -= 0.5f;
        Val *= 2.0f;

        Vector3 lTempPosition = m_MoveBar.rectTransform.anchoredPosition;
        lTempPosition.y = Val * 140.0f;
        m_MoveBar.rectTransform.anchoredPosition = lTempPosition;
    }

    public void SetMapProgress(float Val)
    {
        Val = Mathf.Clamp(Val, 0.0f, 1.0f);

        m_MapProgress.fillAmount = Val;
    }

    public void SetFeverBar(float Val, bool InstantUpdate = false)
    {
        Val = Mathf.Clamp(Val, 0.0f, 1.0f);

        m_TargetFever = Val;

        if (InstantUpdate)
        {
            m_CurFever = m_TargetFever;
            UpdateFeverBar();
        }
    }
    Tween tween = null;
    public void ShowMaxFeverBar(bool show)
    {
        m_MaxFeverBar.gameObject.SetActive(show);

        if (show)
            DoMaxFeverBarColor();
    }

    public void DoMaxFeverBarColor()
    {
        tween = m_MaxFeverBar.DOColor(new Color(Random.Range(0.0f, 1.0f), Random.Range(0.0f, 1.0f), Random.Range(0.0f, 1.0f)), 0.1f).SetEase( Ease.Linear);
        if (m_MaxFeverBar.gameObject.activeSelf)
            tween.OnComplete(() => DoMaxFeverBarColor());
    }

    public void ShowReadyUI(bool show)
    {
        m_ReadyUI.gameObject.SetActive(show);


    }

    public void ShowGamePlayUI(bool show)
    {
        float EndVal = show ? 1.0f : 0.0f;
        CanvasGroup lTempCanvasGroup = m_GamePlayUI.GetComponent<CanvasGroup>();
        lTempCanvasGroup.DOFade(EndVal, 0.5f);
    }
}
