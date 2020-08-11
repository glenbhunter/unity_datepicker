using UnityEngine;
using UnityEngine.UI;

using System;
using System.Collections.Generic;
using System.Globalization;

public class Calender : MonoBehaviour
{
    public DayOfWeek FirstDayOfWeek;

    [SerializeField] Text m_DateHeading;

    public List<Text> DaysOfWeekLabels;
    public List<CalenderButton> CalenderButtons;

    private int m_ButtonClickCount;
    private DateTime? m_First_SelectedDate;
    private DateTime? m_Second_SelectedDate;

    private CalenderButton m_FirstButton;
    private CalenderButton m_SecondButton;


    private DateTime m_CurrentCalenderDate;


    private void Start()
    {
        m_CurrentCalenderDate = new DateTime(DateTime.Now.Year, DateTime.Now.Month, DateTime.Now.Day);

        SetupCalenderGrid(m_CurrentCalenderDate);
    }

    public void SetupCalenderGrid(DateTime calenderDate)
    {
        int calenderMonth = calenderDate.Month;
        int calenderYear = calenderDate.Year;

        Update_CurrentDateHeading(calenderMonth, calenderYear);

        Update_DaysOfWeekHeadings(calenderMonth, calenderYear);

        Update_CalenderButtons(calenderMonth, calenderYear);
    }

    private void Update_CurrentDateHeading(int calenderMonth, int calenderYear)
    {
        string month = CultureInfo.CurrentCulture.DateTimeFormat.GetMonthName(calenderMonth);
        m_DateHeading.text = month.ToUpper() + " " + calenderYear;
    }

    private void Update_DaysOfWeekHeadings(int calenderMonth, int calenderYear)
    {
        DateTime startDate = new DateTime(calenderYear, calenderMonth, 1);
        DayOfWeek firstDayOfMonth = startDate.DayOfWeek;


        // Used for mon-sun labels
        int startingIndex = (int)FirstDayOfWeek;

        for (int i = 0; i < 7; i++)
        {
            DaysOfWeekLabels[i].text = ((DayOfWeek)startingIndex).ToString().Remove(3).ToUpper();

            startingIndex++;

            if (startingIndex > 6)
                startingIndex = 0;
        }
    }

    private void Update_CalenderButtons(int calenderMonth, int calenderYear)
    {
        DateTime startDate = new DateTime(calenderYear, calenderMonth, 1);
        DayOfWeek firstDayOfMonth = startDate.DayOfWeek;

        int daysBefore = firstDayOfMonth - FirstDayOfWeek;
        startDate = startDate.AddDays(-daysBefore);

        for (int i = 0; i < 42; i++)
        {
            CalenderButtons[i].Setup(startDate, (startDate.Month == calenderMonth), this);

            if(m_First_SelectedDate != null && startDate == m_First_SelectedDate)
            {
                m_FirstButton = CalenderButtons[i];
                m_FirstButton.ForcePressed();
            }
            else if(m_SecondButton != null && startDate == m_Second_SelectedDate)
            {
                m_SecondButton = CalenderButtons[i];
                m_SecondButton.ForcePressed();
            }
            else if (startDate > m_First_SelectedDate && startDate < m_Second_SelectedDate)
            {
                CalenderButtons[i].Highlight();
            }
            else
            {
                CalenderButtons[i].ForceClear();
            }

            startDate = startDate.AddDays(1);
        }
    }

    public void OnClick_NextMonth()
    {
        m_CurrentCalenderDate = m_CurrentCalenderDate.AddMonths(1);
        SetupCalenderGrid(m_CurrentCalenderDate);
    }

    public void OnClick_PreviousMonth()
    {
        m_CurrentCalenderDate = m_CurrentCalenderDate.AddMonths(-1);
        SetupCalenderGrid(m_CurrentCalenderDate);
    }

    public void OnCalenderButtonClick(DateTime chosenDate, CalenderButton button)
    {
        m_ButtonClickCount++;

        if (m_ButtonClickCount == 3)
            m_ButtonClickCount = 1;

        switch (m_ButtonClickCount)
        {
            case 1:
                m_SecondButton = null;
                m_Second_SelectedDate = null;
                
                m_FirstButton = button;
                m_First_SelectedDate = chosenDate;
                break;
            case 2:
                m_SecondButton = button;
                m_Second_SelectedDate = chosenDate;
                break;
        }

        if (m_ButtonClickCount == 1)
        {
            for (int i = 0; i < 42; i++)
            {
                CalenderButtons[i].ForceClear();
            }

            m_FirstButton.ForcePressed();

            return;
        }
        else if (m_ButtonClickCount == 2)
        {
            if (m_Second_SelectedDate < m_First_SelectedDate)
            {
                m_FirstButton.ForceClear();
                m_FirstButton = m_SecondButton;
                m_First_SelectedDate = m_Second_SelectedDate;
                m_FirstButton.Pressed();
                m_ButtonClickCount = 1;
                return;
            }
            else
            {
                
                DateTime startDate = new DateTime(m_CurrentCalenderDate.Year, m_CurrentCalenderDate.Month, 1);
                DayOfWeek firstDayOfMonth = startDate.DayOfWeek;

                int daysBefore = firstDayOfMonth - FirstDayOfWeek;
                startDate = startDate.AddDays(-daysBefore);
                Debug.Log(startDate);

                for (int i = 0; i < 42; i++)
                {

                    if (startDate == m_First_SelectedDate)
                    {
                        CalenderButtons[i].Pressed();
                    }
                    else if (startDate == m_Second_SelectedDate)
                    {
                        CalenderButtons[i].ForcePressed();

                    }
                    else if (startDate > m_First_SelectedDate && startDate < m_Second_SelectedDate)
                    {
                       
                        CalenderButtons[i].Highlight();
                    }
                    else
                    {
                        CalenderButtons[i].ForceClear();
                    }

                    startDate = startDate.AddDays(1);
                }

                return;
            }
        }

    }
}
