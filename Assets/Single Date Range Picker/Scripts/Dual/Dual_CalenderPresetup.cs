using System;
using UnityEngine;

[ExecuteInEditMode]
public class Dual_CalenderPresetup : MonoBehaviour
{
    [SerializeField] Dual_DateRangePicker m_DateRangePicker;
    [SerializeField] CalenderButton m_CalenderButtonPrefab;
    [SerializeField] Calender fw_Calender;
    [SerializeField] Transform[] fw_Rows;
    [SerializeField] bool m_Debug = false;

    private void Update()
    {
        if (m_Debug)
        {
            m_Debug = false;

            Spawn_CalenderButtons();
            //m_DateRangePicker.Setup();
        }
    }

    private void Spawn_CalenderButtons()
    {
        int currentRow = 0;

        

        for (int i = 0; i < 42; i++)
        {
            if (i % 7 == 0)
            {
                currentRow = i / 7;

                CalenderButton[] btns = fw_Rows[currentRow].GetComponentsInChildren<CalenderButton>();
                Debug.Log(btns.Length);
                for (int j = 0; j < btns.Length; j++)
                {
                    DestroyImmediate(btns[j].gameObject);
                }
            }

                

          
            GameObject gameObject = Instantiate(m_CalenderButtonPrefab.gameObject, fw_Rows[currentRow].transform, false);
            //fw_Calender.CalenderButtons.Add(gameObject.GetComponent<CalenderButton>());
        }

        //m_Calender.CalenderDate = new DateTime(DateTime.Now.Year, DateTime.Now.Month, DateTime.Now.Day);
        //fw_Calender.Setup(DateTime.Now.Year, DateTime.Now.Month, DayOfWeek.Monday, false, null, null);
    }
}
