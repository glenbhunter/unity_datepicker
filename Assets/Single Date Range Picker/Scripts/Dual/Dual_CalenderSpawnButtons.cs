using System;
using UnityEngine;

[ExecuteInEditMode]
public class Dual_CalenderSpawnButtons : MonoBehaviour
{
    [SerializeField] Dual_DateRangePicker m_DateRangePicker;
    [SerializeField] CalenderButton m_CalenderButtonPrefab;

    [Header("First calender window")]
    [SerializeField] Calender fw_Calender;
    [SerializeField] Transform[] fw_Rows;

    [Header("Second calender window")]
    [SerializeField] Calender sw_Calender;
    [SerializeField] Transform[] sw_Rows;

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

        fw_Calender.CalenderButtons = new System.Collections.Generic.List<CalenderButton>();
        sw_Calender.CalenderButtons = new System.Collections.Generic.List<CalenderButton>();

        for (int i = 0; i < 42; i++)
        {
            if (i % 7 == 0)
            {
                currentRow = i / 7;

                CalenderButton[] fw_Btns = fw_Rows[currentRow].GetComponentsInChildren<CalenderButton>();
                CalenderButton[] sw_Btns = sw_Rows[currentRow].GetComponentsInChildren<CalenderButton>();

                for (int fw  = 0; fw < fw_Btns.Length; fw++)
                    DestroyImmediate(fw_Btns[fw].gameObject);

                for (int sw = 0; sw < sw_Btns.Length; sw++)
                    DestroyImmediate(sw_Btns[sw].gameObject);
            }

                

          
            GameObject fw_BtnObj = Instantiate(m_CalenderButtonPrefab.gameObject, fw_Rows[currentRow].transform, false);
            GameObject sw_BtnObj = Instantiate(m_CalenderButtonPrefab.gameObject, sw_Rows[currentRow].transform, false);

            fw_Calender.CalenderButtons.Add(fw_BtnObj.GetComponent<CalenderButton>());
            sw_Calender.CalenderButtons.Add(sw_BtnObj.GetComponent<CalenderButton>());
        }

        m_DateRangePicker.Setup();
    }
}
