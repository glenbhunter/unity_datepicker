
using UnityEngine;
using System;
using System.Collections.Generic;
using UnityEngine.UI;
using System.Globalization;

public delegate void OnPointerEnter(CalenderButton button, Calender calender);
public delegate void OnPointerDown(CalenderButton button, DateTime calenderDate, Calender calender);
public delegate void OnPointerExit(CalenderButton button, Calender calender);
/// <summary>
/// Called when setup was called() and is in the process of updating/iterating over each button state, good for highlighting selected dates
/// of a calender button - Setup is generally called when a calender has changed month or year and needs to be refreshed,
/// 
/// </summary>
/// <param name="calenderButton"></param>
/// <param name="calenderButton"></param>
public delegate void OnCalenderRefreshed(DateTime calenderDate, CalenderButton calenderButton, DateTime buttonDate);

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
    public OnCalenderRefreshed CalenderRefreshed;

    public void Setup(int year, int month, DayOfWeek firstDayOfWeek, bool showDaysInOtherMonths, DateTime? startDate, DateTime? endDate, UITweenManager uiTweenManager)
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
          
            CalenderButtons[i].Setup(this, currentDate, currentDate.Day.ToString(), m_ShowDatesInOtherMonths, uiTweenManager);

            if(CalenderRefreshed != null)
            {
                CalenderRefreshed(Date, CalenderButtons[i], currentDate);
            }


            currentDate = currentDate.AddDays(1);
        }
    }


    private bool DateIsInCalenderMonth(DateTime chosenDate, DateTime calenderDate)
    {
        if (calenderDate.Month == chosenDate.Month)
        {
            return true;
        }

        return false;
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

