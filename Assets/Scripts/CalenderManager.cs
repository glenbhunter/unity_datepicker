using System;
using System.Globalization;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CalenderManager : MonoBehaviour
{
    public DayOfWeek FirstDayOfWeek;

    [SerializeField] Text m_CurrentDateText;
    [SerializeField] List<Text> m_DaysOfWeekHeadings;
    public List<CalenderButton> CalenderButtons;
    

    private DateTime m_CurrentDate;

    // keep track of all buttons selected
    Queue<CalenderButton> m_SelectedButtons = new Queue<CalenderButton>();

    void Start()
    {
        m_CurrentDate = DateTime.Now;

        Setup_CalenderGrid(DateTime.Now, FirstDayOfWeek);
    }

    public void Setup_CalenderGrid(DateTime date, DayOfWeek firstDayOfWeek)
    {
        int startingIndex = (int)firstDayOfWeek;
        // heading
        for (int i = 0; i < 7; i++)
        {

            m_DaysOfWeekHeadings[i].text = ((DayOfWeek)startingIndex).ToString().Remove(3).ToUpper();

            startingIndex++;

            if (startingIndex >= 6)
                startingIndex = 0;
        }


        // days


        // get ACTUAL the first day of the month
        DayOfWeek actualDayOfWeek = new DateTime(date.Year, date.Month, 1).DayOfWeek;

        int daysInBetween = actualDayOfWeek - firstDayOfWeek;
        int daysbefore = daysInBetween;
       
        DateTime startDate = new DateTime(date.Year, date.Month, 1);
        startDate = startDate.AddDays(-daysbefore);
 
        for (int i = 0; i < 42; i++)
        {
            //bool isCurrentMonth = (startDate.Month == DateTime..Month) ? true : false;
            //bool isCurrentDay = (startDate.Date == today.Date) ? true : false;
            CalenderButtons[i].Setup(startDate.Day, startDate.Month, this);

            if (startDate.Month != date.Month)
                CalenderButtons[i].DayIsPartOfPreviousMonth();
            else
                CalenderButtons[i].DayIsPartOfCurrentMonth();


            startDate = startDate.AddDays(1);
        }

        Update_CurrentDateText(m_CurrentDateText, date);
    }

    public void OnClick_ShowPreviousMonth()
    {
        m_CurrentDate = m_CurrentDate.AddMonths(-1);
        Setup_CalenderGrid(m_CurrentDate, FirstDayOfWeek);
    }

    public void OnClick_ShowNextMonth()
    {
        m_CurrentDate = m_CurrentDate.AddMonths(1);
        Setup_CalenderGrid(m_CurrentDate, FirstDayOfWeek);
    }

    public void OnClick_SelectedCalenderDay(CalenderButton button, int month, int day)
    {
        
        m_SelectedButtons.Enqueue(button);

        if(m_SelectedButtons.Count > 2)
        {
            m_SelectedButtons.Peek().OnDeselect();
            m_SelectedButtons.Dequeue();
        }
    }

    private void Update_CurrentDateText(Text textComponent, DateTime dateTime)
    {
        string month = CultureInfo.CurrentCulture.DateTimeFormat.GetMonthName(dateTime.Month);
        textComponent.text = month.ToUpper() + " " + DateTime.Now.Year;
    }
}