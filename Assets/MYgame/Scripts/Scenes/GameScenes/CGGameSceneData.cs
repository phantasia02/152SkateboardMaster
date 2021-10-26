using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class CGGameSceneData : CSingletonMonoBehaviour<CGGameSceneData>
{

    public enum EAllFXType
    {
        eGoodFxLight    = 0,
        eGoodFx1        = 1,
        eFail1Fx1       = 2,
        eMax,
    };

    public enum EOtherObj
    {
        eScoringBox     = 0,
        eMax,
    };

    public enum EArmsType
    {
        eMace           = 0,
        eArmature       = 1,
        eAxe            = 2,
        eHam            = 3,
        ePlunger        = 4,
        eLollipop_02    = 5,
        eLeg            = 6,
        eRolling_Pin    = 7,
        eBaby_Hammer    = 8,
        eMax,
    };

    public enum EPostColor
    {
        eYellowPost     = 0,
        ePinkPost       = 1,
        eBluePost       = 2,
        eGreenPost      = 3,
        eOrangePost     = 4,
        eMax
    }

    [SerializeField]  public GameObject[]    m_AllFX                 = null;
    [SerializeField]  public GameObject[]    m_AllOtherObj           = null;
    [SerializeField]  public Color[]         m_AllPostColor          = null;
    readonly public int[] m_AllLayerOutlineColor = {
        (int)StaticGlobalDel.ELayerIndex.eYellow,
        (int)StaticGlobalDel.ELayerIndex.ePink,
        (int)StaticGlobalDel.ELayerIndex.eBlue,
        (int)StaticGlobalDel.ELayerIndex.eGreen,
        (int)StaticGlobalDel.ELayerIndex.eOrange };


    private void Awake()
    {
        //for (int i = 0; i < (int)EArmsType.eMax; i++)
        //{
        //    m_CurNewArmsCount = i;
        //    m_AllArmsPool[i] = new CObjPool<GameObject>();
        //    m_AllArmsPool[i].NewObjFunc = NewArms;
        //    m_AllArmsPool[i].RemoveObjFunc = RemoveArms;
        //    m_AllArmsPool[i].InitDefPool(10);
        //}
    }

    public Color PostColorToColor(CGGameSceneData.EPostColor EIndexColor){return m_AllPostColor[(int)EIndexColor];}

}
