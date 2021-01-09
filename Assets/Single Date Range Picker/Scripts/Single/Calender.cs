﻿
using UnityEngine;
using System;
using System.Collections.Generic;
using UnityEngine.UI;
using System.Globalization;

public delegate void OnPointerEnter(int buttonIndex, Calender calender);
public delegate void OnPointerDown(int buttonIndex, DateTime calenderDate, Calender calender);
public delegate void OnPointerExit(int buttonIndex, Calender calender);

public class Calender : MonoBehaviour
{
    public DateTime Date;
    private DayOfWeek m_FirstDayOfWeek;

    [Header("References")]
    public List<CalenderButton> CalenderButtons;
    [SerializeField] Text m_DateLabel;
    [SerializeField] List<Text> m_DaysOfWeekLabels;
    private bool m_ShowDatesInOtherMonths = false;

    public OnPointerEnter PointerEnter;
    public OnPointerDown PointerDown;
    public OnPointerExit PointerExit;

    public void Setup(int year, int month, DayOfWeek firstDayOfWeek, bool showDaysInOtherMonths)
    {
        Date = new DateTime(year, month, 1);
        m_FirstDayOfWeek = firstDayOfWeek;
        m_ShowDatesInOtherMonths = showDaysInOtherMonths;

        // Time to setup all the buttons! :)
        // create current month starting from 1
        DateTime currentDate = new DateTime(Date.Year, Date.Month, 1);

        DayOfWeek firstDayOfMonth = currentDate.DayOfWeek;


        if (firstDayOfMonth < m_FirstDayOfWeek)
        {

            // start current date based upon start day of week
            // this is used to show previous dates before
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

                if (dayIndex == (int)firstDayOfMonth)
                {
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
        string monthHeading = CultureInfo.CurrentCulture.DateTimeFormat.GetMonthName(Date.Month);
        m_DateLabel.text = monthHeading.ToUpper() + " " + Date.Year;

        //used for mon-sun labels
        int startingIndex = (int)m_FirstDayOfWeek;

        for (int i = 0; i < 42; i++)
        {

            // update days of weeks labels
            if (i < 7)
            {
                m_DaysOfWeekLabels[i].text = ((DayOfWeek)startingIndex).ToString().Remove(1).ToUpper();

                startingIndex++;

                if (startingIndex > 6)
                    startingIndex = 0;
            }

            // update buttons
            int btnIndex = i;
            CalenderButtons[i].Setup(btnIndex, this, currentDate, currentDate.Day.ToString(), (m_ShowDatesInOtherMonths) ? false : (currentDate.Month == Date.Month) ? false : true);

            /*
            // highlight
            if (m_StartDate != null && m_StartDate == currentDate)
            {
                CalenderButtons[i].UpdateState(CalenderButton.State.Selected, CalenderDate, m_StartDate, m_EndDate);
                m_StartDate_SelectedBtnIndex = i;
            }
            else if (m_EndDate != null && m_EndDate == currentDate)
            {
                CalenderButtons[i].UpdateState(CalenderButton.State.Selected, CalenderDate, m_StartDate, m_EndDate);
                m_EndDate_SelectedBtnIndex = i;
            }

            if (m_StartDate != null && m_EndDate != null && currentDate >= m_StartDate && currentDate <= m_EndDate)
            {
                CalenderButtons[i].UpdateState(CalenderButton.State.Highlighted, CalenderDate, m_StartDate, m_EndDate);
            }*/

            currentDate = currentDate.AddDays(1);
        }
    }
}

