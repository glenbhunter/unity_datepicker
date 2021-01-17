using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Used to spawn calender buttons, which is generally used for styling, ideally should be doing this to prefabs that have been unpacked.
/// </summary>
[ExecuteInEditMode]
public class SpawnCalenderButtons : MonoBehaviour
{
    [SerializeField] DateRangePicker m_DateRangePicker;

    [SerializeField] CalenderButton m_CalenderButtonPrefab;
    [SerializeField] List<CalenderData> m_Calenders;
    [SerializeField] bool m_Spawn = false;

    private void Update()
    {
        if(m_Spawn)
        {
            m_Spawn = false;

            Spawn_CalenderButtons();
        }
    }

    private void Spawn_CalenderButtons()
    {
        int currentRow = 0;


        for (int i = 0; i < m_Calenders.Count; i++)
        {
            m_Calenders[i].Calender.CalenderButtons = new List<CalenderButton>();

            if (m_Calenders[i] != null)
            {
                for (int j = 0; j < 42; j++)
                {
                    
                    if (j % 7 == 0)
                    {
                        currentRow = j / 7;

                        // want to remove any buttons that exist
                        CalenderButton[] btns = m_Calenders[i].Rows[currentRow].GetComponentsInChildren<CalenderButton>();

                        for (int fw = 0; fw < btns.Length; fw++)
                            DestroyImmediate(btns[fw].gameObject);

                    }

                    GameObject btnObj = Instantiate(m_CalenderButtonPrefab.gameObject, m_Calenders[i].Rows[currentRow].transform, false);
                    m_Calenders[i].Calender.CalenderButtons.Add(btnObj.GetComponent<CalenderButton>());
                   
                }
            }
        }

        m_DateRangePicker.Setup();
    }
}

[System.Serializable]
public class CalenderData
{
    public Calender Calender;
    public Transform[] Rows;
}
