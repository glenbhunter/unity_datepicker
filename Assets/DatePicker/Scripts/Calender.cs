
using UnityEngine;
using System;
using System.Collections.Generic;
using UnityEngine.UI;
using System.Globalization;

public class Calender : MonoBehaviour
{
    [SerializeField] private DayOfWeek m_FirstDayOfWeek;

    [SerializeField] Text m_DateLabel;
    [SerializeField] List<Text> m_DaysOfWeekLabels;

    public List<CalenderButton> CalenderButtons;

    public DateTime CalenderDate;

    private DateTime? m_StartDate;
    private int m_StartDate_SelectedBtnIndex;

    private DateTime? m_EndDate;
    private int m_EndDate_SelectedBtnIndex;

   

    private void Start()
    {
        CalenderDate = new DateTime(DateTime.Now.Year, DateTime.Now.Month, DateTime.Now.Day);

        Setup();
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
            for (int i = 0; i < 42; i++)
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

    public void Setup()
    {
        DateTime currentDate = new DateTime(CalenderDate.Year, CalenderDate.Month, 1);

        DayOfWeek firstDayOfMonth = currentDate.DayOfWeek;


        if(firstDayOfMonth < m_FirstDayOfWeek)
        {
           
            // start current date based upon start day of week

            int dayIndex = (int)m_FirstDayOfWeek;
            int daysBehind = 0;

            for (int i = 0; i < 6; i++)
            {
                dayIndex++;
                daysBehind++;

                if (dayIndex > 6)
                {
                    dayIndex = 0;
                }

                if(dayIndex == (int)firstDayOfMonth)
                {
                    Debug.Log("is ture");
                    currentDate = currentDate.AddDays(-daysBehind);
                    break;
                }
            }
        }
        else
        {
            // start current date based upon start day of week
            currentDate = currentDate.AddDays(-(firstDayOfMonth - m_FirstDayOfWeek));
        }
        

        // update main date heading
        string month = CultureInfo.CurrentCulture.DateTimeFormat.GetMonthName(CalenderDate.Month);
        m_DateLabel.text = month.ToUpper() + " " + CalenderDate.Year;

        //used for mon-sun labels
        int startingIndex = (int)m_FirstDayOfWeek;

        for (int i = 0; i < 42; i++)
        {

            // update days of weeks labels
            if(i < 7)
            {
                m_DaysOfWeekLabels[i].text = ((DayOfWeek)startingIndex).ToString().Remove(3).ToUpper();

                startingIndex++;

                if (startingIndex > 6)
                    startingIndex = 0;
            }

            // update buttons
            int btnIndex = i;
            CalenderButtons[i].Setup(btnIndex, this, currentDate, currentDate.Day.ToString());

            // highlight
            if(m_StartDate != null && m_StartDate == currentDate)
            {
                CalenderButtons[i].UpdateState(CalenderButton.State.Selected, CalenderDate, m_StartDate, m_EndDate);
                m_StartDate_SelectedBtnIndex = i;
            }
            else if(m_EndDate != null && m_EndDate == currentDate)
            {
                CalenderButtons[i].UpdateState(CalenderButton.State.Selected, CalenderDate, m_StartDate, m_EndDate);
                m_EndDate_SelectedBtnIndex = i;
            }

            if(m_StartDate != null && m_EndDate != null && currentDate >= m_StartDate && currentDate <= m_EndDate)
            {
                CalenderButtons[i].UpdateState(CalenderButton.State.Highlighted, CalenderDate, m_StartDate, m_EndDate);
            }

            currentDate = currentDate.AddDays(1);
        }
    }

    /// <summary>
    /// Change to next calender month
    /// </summary>
    public void OnClick_NextCalenderMonth()
    {
        CalenderDate = CalenderDate.AddMonths(1);
        Setup();
    }

    public void OnClick_PreviousCalenderMonth()
    {
        CalenderDate = CalenderDate.AddMonths(-1);
        Setup();
    }
}
