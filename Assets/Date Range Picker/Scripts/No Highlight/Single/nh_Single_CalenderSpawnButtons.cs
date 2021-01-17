using UnityEngine;

[ExecuteInEditMode]
public class nh_Single_CalenderSpawnButtons : MonoBehaviour
{
    [SerializeField] nh_Single_DateRangePicker m_DateRangePicker;
    [SerializeField] CalenderButton m_CalenderButtonPrefab;

    [SerializeField] Calender m_Calender;
    [SerializeField] Transform[] m_Rows;

    [SerializeField] bool m_Spawn = false;

    private void Update()
    {
        if (m_Spawn)
        {
            m_Spawn = false;

            Spawn_CalenderButtons();
        }
    }

    private void Spawn_CalenderButtons()
    {
        int currentRow = 0;

        m_Calender.CalenderButtons = new System.Collections.Generic.List<CalenderButton>();

        for (int i = 0; i < 42; i++)
        {
            if (i % 7 == 0)
            {
                currentRow = i / 7;

                CalenderButton[] btns = m_Rows[currentRow].GetComponentsInChildren<CalenderButton>();

                if (btns != null)
                {
                    for (int j = 0; j < btns.Length; j++)
                        DestroyImmediate(btns[j].gameObject);
                }
            }


            GameObject btnObj = Instantiate(m_CalenderButtonPrefab.gameObject, m_Rows[currentRow].transform, false);

            m_Calender.CalenderButtons.Add(btnObj.GetComponent<CalenderButton>());
        }

        m_DateRangePicker.Setup();
    }
}
