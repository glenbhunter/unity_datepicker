using System;
using UnityEngine;

public class Single_DateRangePicker : MonoBehaviour
{
    [SerializeField] DayOfWeek m_FirstDayOfWeek = DayOfWeek.Monday;

    [SerializeField] UITweenManager UITweenManager;
    [SerializeField] bool m_ShowDaysInOtherMonths = false;

    private Calender m_Calender;

    private DateTime? m_StartDate;
    private CalenderButton m_StartDate_SelectedBTN;
    private DateTime? m_EndDate;

    public delegate void CalenderUpdate(DateTime? selectedStartDate, DateTime? selectedEndDate);
    public CalenderUpdate CalendersUpdated;

    private void Setup()
    {
        m_Calender.Setup(DateTime.Now.Year, DateTime.Now.Month, m_FirstDayOfWeek, m_ShowDaysInOtherMonths, m_StartDate, m_EndDate, UITweenManager);
    }

    public void OnPointerEnter(CalenderButton chosenCalenderButton, Calender calender)
    {
        if (chosenCalenderButton.CurrentState == CalenderButton.State.Normal && m_EndDate == null)
        {
            chosenCalenderButton.UpdateState(CalenderButton.State.Hover, calender.Date, m_StartDate, m_EndDate);
        }
    }

    public void OnPointerDown(CalenderButton chosenCalenderButton, DateTime chosenDate, Calender calender)
    {
        // clears selection
        if (m_StartDate != null && m_EndDate != null)
        {
            for (int i = 0; i < 42; i++)
                m_Calender.CalenderButtons[i].ResetToOriginal();

            m_StartDate = null;
            m_EndDate = null;

            // don't return on this one
        }

        if (m_StartDate == null && m_EndDate == null)
        {
            if (chosenCalenderButton.CurrentState != CalenderButton.State.Disabled)
            {
                m_StartDate = chosenDate;
                m_StartDate_SelectedBTN = chosenCalenderButton;

                CalendersUpdated?.Invoke(m_StartDate, m_EndDate);
                chosenCalenderButton.UpdateState(CalenderButton.State.Selected, chosenDate, m_StartDate, m_EndDate);
            }
            return;
        }
    }
}
