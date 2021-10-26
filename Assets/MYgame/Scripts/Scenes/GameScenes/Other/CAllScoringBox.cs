using System.Collections;
using System.Collections.Generic;
using UnityEngine;



public class CAllScoringBox : CSingletonMonoBehaviour<CAllScoringBox>
{
   // [SerializeField] protected int m_ScoringBoxCount = 10;
    protected List<CScoringBox> m_AllScoringBox = new List<CScoringBox>();
    public List<CScoringBox> AllScoringBox { get { return m_AllScoringBox; } }
    [SerializeField] protected List<CDataInitScoringBox> m_AllDataInitScoringBox = new List<CDataInitScoringBox>();

    private void Awake()
    {
        CGGameSceneData lTempGameSceneData = CGGameSceneData.SharedInstance;

        Vector3 lTempVector3 = Vector3.zero;
        CScoringBox lTempScoringBox = null;
        GameObject lTempObj = null;
        float lTempZ = 5.0f;


        for (int i = 0; i < m_AllDataInitScoringBox.Count; i++)
        {
            lTempObj = GameObject.Instantiate(lTempGameSceneData.m_AllOtherObj[(int)CGGameSceneData.EOtherObj.eScoringBox], this.transform);
            lTempScoringBox = lTempObj.GetComponent<CScoringBox>();
            m_AllScoringBox.Add(lTempScoringBox);

            lTempVector3 = lTempScoringBox.transform.localPosition;
            lTempVector3.z = lTempZ;
            lTempScoringBox.transform.localPosition = lTempVector3;

            if (m_AllDataInitScoringBox[i].m_Number == 0)
                m_AllDataInitScoringBox[i].m_Number = i + 1;

            lTempScoringBox.InitSet(m_AllDataInitScoringBox[i]);

            lTempZ += 10.0f;
        }
    }
}
