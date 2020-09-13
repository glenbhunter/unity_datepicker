
using UnityEngine;
using System;
using System.Collections.Generic;

public class Calender : MonoBehaviour
{
    [SerializeField] private DayOfWeek m_FirstDayOfWeek;

    public List<CalenderButton> CalenderButtons;

    public DateTime CalenderDate;

    private DateTime? m_StartDate;
    private int m_StartDate_SelectedBtnIndex;

    private DateTime? m_EndDate;
    private int m_EndDate_SelectedBtnIndex;

    private void Start()
    {
        CalenderDate = new DateTime(DateTime.Now.Year, DateTime.Now.Month, DateTime.Now.Day);

        Refresh(true);
    }

    public void OnPointerEnter(int buttonIndex)
    {
        if(CalenderButtons[buttonIndex].CurrentState == CalenderButton.State.Normal && m_EndDate == null)
        {
            CalenderButtons[buttonIndex].UpdateState(CalenderButton.State.Hover, CalenderDate, m_StartDate, m_EndDate);
        }
    }

    public void OnPointerDown(int buttonIndex, DateTime chosenDate)
    {
        if(m_StartDate != null && m_EndDate != null)
        {
            // unhighlight
            for (int i = m_StartDate_SelectedBtnIndex; i < m_EndDate_SelectedBtnIndex + 1; i++)
            {
                CalenderButtons[i].UpdateState(CalenderButton.State.Normal, CalenderDate, m_StartDate, m_EndDate);
            }

            m_StartDate = null;
            m_EndDate = null;

            // don't return on this one
        }

        if(m_StartDate == null && m_EndDate == null)
        {
            m_StartDate = chosenDate;
            m_StartDate_SelectedBtnIndex = buttonIndex;

            CalenderButtons[buttonIndex].UpdateState(CalenderButton.State.Selected, CalenderDate, m_StartDate, m_EndDate);

            return;
        }

        if(m_StartDate != null && chosenDate < m_StartDate && m_EndDate == null)
        {
            // revert previous select btn and select new btn
            CalenderButtons[m_StartDate_SelectedBtnIndex].UpdateState(CalenderButton.State.Normal, CalenderDate, m_StartDate, m_EndDate);

            m_StartDate = chosenDate;
            m_StartDate_SelectedBtnIndex = buttonIndex;

            CalenderButtons[buttonIndex].UpdateState(CalenderButton.State.Selected, CalenderDate, m_StartDate, m_EndDate);

            return;
        }

        if(m_StartDate != null && m_EndDate == null)
        {
            m_EndDate = chosenDate;
            m_EndDate_SelectedBtnIndex = buttonIndex;

            CalenderButtons[buttonIndex].UpdateState(CalenderButton.State.Selected, CalenderDate, m_StartDate, m_EndDate);

            // hightlight
            for (int i = m_StartDate_SelectedBtnIndex; i < m_EndDate_SelectedBtnIndex + 1; i++)
            {
                CalenderButtons[i].UpdateState(CalenderButton.State.Highlighted, CalenderDate, m_StartDate, m_EndDate);
            }
            
            return;
        }
    }

    public void OnPointerExit(int buttonIndex)
    {
        if(CalenderButtons[buttonIndex].CurrentState == CalenderButton.State.Hover)
        {
            CalenderButtons[buttonIndex].UpdateState(CalenderButton.State.Normal, CalenderDate, m_StartDate, m_EndDate);
        }
    }

    public void Refresh(bool setup)
    {
        DateTime currentDate = new DateTime(CalenderDate.Year, DateTime.Now.Month, 1);

        DayOfWeek firstDayOfMonth = currentDate.DayOfWeek;

        // start current date based upon start day of week
        currentDate = currentDate.AddDays(-(firstDayOfMonth - m_FirstDayOfWeek));

        for (int i = 0; i < 42; i++)
        {
            // Setup
            int btnIndex = i;

            if(setup)
            {
                CalenderButtons[i].Setup(btnIndex, this, currentDate, currentDate.Day.ToString());
            }

            currentDate = currentDate.AddDays(1);
        }
    }
}
