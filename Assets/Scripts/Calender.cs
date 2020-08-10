using UnityEngine;
using UnityEngine.UI;

using System;
using System.Collections.Generic;
using System.Globalization;

public class Calender : MonoBehaviour
{
    public DayOfWeek FirstDayOfWeek;

    public List<CalenderButton> CalenderButtons;
    public List<Text> DaysOfWeekLabels;


    private DateTime m_CurrentCalenderDate;

    private void Start()
    {
        m_CurrentCalenderDate = new DateTime(DateTime.Now.Year, DateTime.Now.Month, DateTime.Now.Day);

        SetupCalenderGrid(m_CurrentCalenderDate);
    }

    public void SetupCalenderGrid(DateTime currentCalenderDate)
    {
        DateTime startDate = new DateTime(currentCalenderDate.Year, currentCalenderDate.Month, 1);
        DayOfWeek firstDayOfMonth = startDate.DayOfWeek;

        int daysBefore = firstDayOfMonth - FirstDayOfWeek;
        startDate = startDate.AddDays(-daysBefore);

        // Used for mon-sun labels
        int startingIndex = (int)FirstDayOfWeek;

        for (int i = 0; i < 42; i++)
        {
            // mon-sun labels
            if(i < 7)
            {
                DaysOfWeekLabels[i].text = ((DayOfWeek)startingIndex).ToString().Remove(3).ToUpper();

                startingIndex++;

                if (startingIndex > 6)
                    startingIndex = 0;
            }

            // calender buttons
            CalenderButtons[i].Setup(startDate, (startDate.Month == currentCalenderDate.Month), this);

            startDate = startDate.AddDays(1);
        }
    }

    private int m_ButtonClickCount;
    private DateTime? m_First_SelectedDate;
    private DateTime? m_Second_SelectedDate;

    private CalenderButton m_FirstButton;
    private CalenderButton m_SecondButton;


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

        UpdateCalenderGrid(m_CurrentCalenderDate);

    }

    private void UpdateCalenderGrid(DateTime currentCalenderDate)
    {
       
        if(m_ButtonClickCount == 1)
        {
            for (int i = 0; i < 42; i++)
            {
                CalenderButtons[i].ForceClear();
            }

            m_FirstButton.ForcePressed();

            return;
        }
        else if(m_ButtonClickCount == 2)
        {
            if(m_Second_SelectedDate < m_First_SelectedDate)
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
                DateTime startDate = new DateTime(currentCalenderDate.Year, currentCalenderDate.Month, 1);
                DayOfWeek firstDayOfMonth = startDate.DayOfWeek;

                int daysBefore = firstDayOfMonth - FirstDayOfWeek;
                startDate = startDate.AddDays(-daysBefore);

                for (int i = 0; i < 42; i++)
                {
            
                    if(startDate == m_First_SelectedDate)
                    {
                        CalenderButtons[i].Pressed();
                    }
                    else if(startDate == m_Second_SelectedDate)
                    {
                        CalenderButtons[i].ForcePressed();
                    
                    }
                    else if(startDate > m_First_SelectedDate && startDate < m_Second_SelectedDate)
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

    private void UpdateDayOfWeekLabel(Text textComponent, DateTime dateTime)
    {
        string month = CultureInfo.CurrentCulture.DateTimeFormat.GetMonthName(dateTime.Month);
        textComponent.text = month.ToUpper() + " " + DateTime.Now.Year;
    }
}
