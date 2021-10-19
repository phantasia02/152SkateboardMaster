using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CDoorPost : CGameObjBas
{
    public const string CAnimatorNamtTag = "MoveBlendTree";

    public override EObjType ObjType() { return EObjType.eDoorPost; }

    // ==================== SerializeField ===========================================


    [SerializeField] protected CGGameSceneData.EPostColor m_PostColor = CGGameSceneData.EPostColor.eYellowPost;

    // ==================== SerializeField ===========================================
    protected Animator m_AnimatorPost = null;
    protected Outline m_MyOutline = null;


    protected override void Awake()
    {
        m_AnimatorPost = this.GetComponentInChildren<Animator>();
        m_MyOutline = this.GetComponentInChildren<Outline>();
    }

    // Start is called before the first frame update
    protected override void Start()
    {
        m_AnimatorPost.SetTrigger(CAnimatorNamtTag);
        float lTemppostVal = 0.0f;

        if (m_PostColor == CGGameSceneData.EPostColor.eYellowPost)
            lTemppostVal = 1.0f;
        else if (m_PostColor == CGGameSceneData.EPostColor.eGreenPost)
            lTemppostVal = 0.5f;
        else if (m_PostColor == CGGameSceneData.EPostColor.eOrangePost)
            lTemppostVal = 0.0f;

        m_AnimatorPost.SetFloat("MoveVal", lTemppostVal);

        Color lTempColor = CGGameSceneData.SharedInstance.PostColorToColor(m_PostColor);
        m_MyOutline.SetOutlineColor = lTempColor;
    }

    // Update is called once per frame
    protected override void Update()
    {
        
    }

    public static Color PostColorToColor(CGGameSceneData.EPostColor EIndexColor)
    {
       

        return new Color();
    }
}
