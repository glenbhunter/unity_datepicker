
using UnityEngine;
using System;
using System.Collections.Generic;
using UnityEngine.UI;
using System.Globalization;

public delegate void OnPointerEnter(CalenderButton button, Calender calender);
public delegate void OnPointerDown(CalenderButton button, DateTime calenderDate, Calender calender);
public delegate void OnPointerExit(CalenderButton button, Calender calender);

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

    public void Setup(int year, int month, DayOfWeek firstDayOfWeek, bool showDaysInOtherMonths, DateTime? startDate, DateTime? endDate)
    {
        Date = new DateTime(year, month, 1);
        m_FirstDayOfWeek = firstDayOfWeek;
        m_ShowDatesInOtherMonths = showDaysInOtherMonths;

        // Time to setup all the buttons! :)
        // create current month starting from 1
        DateTime currentDate;
        currentDate = (DateTime)StartDate();
      


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
            CalenderButtons[i].Setup(this, currentDate, currentDate.Day.ToString(), m_ShowDatesInOtherMonths);

            // highlight
            if (startDate != null && startDate == currentDate)
            {
                CalenderButtons[i].UpdateState(CalenderButton.State.Selected, Date, startDate, endDate);
                CalenderButtons[i].UpdateState(CalenderButton.State.Highlighted, Date, startDate, endDate);
            }
            else if (endDate != null && endDate == currentDate)
            {
                CalenderButtons[i].UpdateState(CalenderButton.State.Selected, Date, startDate, endDate);
                CalenderButtons[i].UpdateState(CalenderButton.State.Highlighted, Date, startDate, endDate);
            }
            else if (startDate != null && endDate != null && currentDate >= startDate && currentDate <= endDate)
            {
                CalenderButtons[i].UpdateState(CalenderButton.State.Highlighted, Date, startDate, endDate);
            }

            currentDate = currentDate.AddDays(1);
        }
    }

    public DateTime? StartDate()
    {
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
                    return currentDate = currentDate.AddDays(-daysBehind);
                }
            }
        }
        else
        {
            // start current date based upon start day of week
            return currentDate = currentDate.AddDays(-(firstDayOfMonth - m_FirstDayOfWeek));
        }

        Debug.LogError("Something went wrong, should not be getting here.");
        return null;

    }
}

