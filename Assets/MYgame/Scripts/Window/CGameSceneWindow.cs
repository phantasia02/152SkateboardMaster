using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

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
    [SerializeField] Image  m_FeverBar          = null;

    [SerializeField] [Range(0.0f, 1.0f)] protected float m_InitAnimationVal = 0.5f;

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

    public void SetFeverBar(float Val)
    {
        Val = Mathf.Clamp(Val, 0.0f, 1.0f);

        m_FeverBar.fillAmount = Val;
    }
}
