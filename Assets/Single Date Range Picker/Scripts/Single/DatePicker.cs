using System;
using UnityEngine;
using UnityEngine.UI;

public class DatePicker : MonoBehaviour
{
    [SerializeField] Calender m_Calender;
  
    [SerializeField] GameObject m_BG;
    [SerializeField] Text m_TextField;

    private void Awake()
    {
        //m_Calender.CalenderUpdated += CalenderUpdated;
    }

    public void OnClick_ToggleCalender()
    {
        bool isActive = !m_Calender.gameObject.activeInHierarchy;

        m_Calender.gameObject.SetActive(isActive);
    }

    public void CalenderUpdated(DateTime? startDate, DateTime? endDate)
    {
        string text = "";

        if(startDate != null)
        {
            text += startDate.Value.ToShortDateString();
        }

        if(endDate != null)
        {
            text += " - " + endDate.Value.ToShortDateString();
        }
        m_TextField.text = text;
    }
}
