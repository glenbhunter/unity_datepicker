using UnityEngine;
using UnityEngine.UI;

using System;
using System.Collections.Generic;
using System.Globalization;


public class Dual_Calender : MonoBehaviour
{
    

    /// <summary>
    ///  FW - First calender window
    ///  SW = Second calender window
    /// </summary>

        /*
    [Header("Options")]
    [SerializeField] private DayOfWeek m_FirstDayOfWeek;
    [SerializeField] bool m_ShowDatesInOtherMonths = true;

    [Header("References")]
    [SerializeField] Text m_DateLabel;
    [SerializeField] List<Text> m_DaysOfWeekLabels;

    public List<CalenderButton> FW_CalenderButtons;
    public List<CalenderButton> SW_CalenderButtons;

    private DateTime? m_StartDate;
    private int m_StartDate_SelectedBtnIndex;

    private DateTime? m_EndDate;
    private int m_EndDate_SelectedBtnIndex;

    public delegate void CalenderUpdate(DateTime? startDate, DateTime? endDate);
    public CalenderUpdate CalenderUpdated;

    public DateTime FW_CalenderDate;
    public DateTime SW_CalenderDate;

    private void Start()
    {
        
    }*/

    /*
    private void Start()
    {
        FW_CalenderDate = new DateTime(DateTime.Now.Year, DateTime.Now.Month, DateTime.Now.Day);
        SW_CalenderDate = FW_CalenderDate.AddMonths(1);

        SetupCalender(FW_CalenderDate, FW_CalenderButtons);
        SetupCalender(SW_CalenderDate, FW_CalenderButtons);
    }

    private void SetupCalender(DateTime calenderDate, List<CalenderButton> calenderButtons)
    {
        DateTime currentDate = new DateTime(calenderDate.Year, calenderDate.Month, 1);

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
        string month = CultureInfo.CurrentCulture.DateTimeFormat.GetMonthName(calenderDate.Month);
        m_DateLabel.text = month.ToUpper() + " " + calenderDate.Year;

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
            calenderButtons[i].Setup(btnIndex, this, currentDate, currentDate.Day.ToString(), (m_ShowDatesInOtherMonths) ? false : (currentDate.Month == calenderDate.Month) ? false : true);

            // highlight
            if (m_StartDate != null && m_StartDate == currentDate)
            {
                calenderButtons[i].UpdateState(CalenderButton.State.Selected, calenderDate, m_StartDate, m_EndDate);
                m_StartDate_SelectedBtnIndex = i;
            }
            else if (m_EndDate != null && m_EndDate == currentDate)
            {
                calenderButtons[i].UpdateState(CalenderButton.State.Selected, calenderDate, m_StartDate, m_EndDate);
                m_EndDate_SelectedBtnIndex = i;
            }

            if (m_StartDate != null && m_EndDate != null && currentDate >= m_StartDate && currentDate <= m_EndDate)
            {
                calenderButtons[i].UpdateState(CalenderButton.State.Highlighted, calenderDate, m_StartDate, m_EndDate);
            }

            currentDate = currentDate.AddDays(1);
        }
    }*/
}
