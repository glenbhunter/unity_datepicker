using UnityEngine;
using UnityEngine.UI;

using System;
using System.Collections.Generic;
using System.Globalization;

namespace GlenHunter
{
    public class Calender : MonoBehaviour
    {
        public DayOfWeek FirstDayOfWeek;

        [SerializeField] Text m_DateHeading;

        public List<Text> DaysOfWeekLabels;
        public List<CalenderButton> CalenderButtons;

        private int m_ButtonClickCount;
        private DateTime? m_StartDate;
        private DateTime? m_EndDate;

        private CalenderButton m_StartDateBTN;
        private CalenderButton m_EndDateBTN;


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

                if (m_StartDate != null && startDate == m_StartDate)
                {
                    m_StartDateBTN = CalenderButtons[i];
                }
                else if (m_EndDateBTN != null && startDate == m_EndDate)
                {
                    m_EndDateBTN = CalenderButtons[i];
                }
                else if (startDate > m_StartDate && startDate < m_EndDate)
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

        private void Refresh()
        {
            DateTime currentDate = new DateTime(m_CurrentCalenderDate.Year, m_CurrentCalenderDate.Month, 1);
            DayOfWeek firstDayOfMonth = currentDate.DayOfWeek;

            int daysBefore = firstDayOfMonth - FirstDayOfWeek;
            currentDate = currentDate.AddDays(-daysBefore);
            //Debug.Log(startDate);

            for (int i = 0; i < 42; i++)
            {
                // one click registered
                if(m_StartDate != null && currentDate == m_StartDate && m_EndDate == null)
                {
                    CalenderButtons[i].ForcePressed(true, false);
                   
                }
            // two clicks have registered
                // is start date    
                else if(m_StartDate != null && currentDate == m_StartDate)
                {
                    CalenderButtons[i].ForcePressed(false, true);
                }
                // is end date
                else if(m_EndDate != null && currentDate == m_EndDate)
                {
                    CalenderButtons[i].ForcePressed(false, false);
                }
                // is inbetween
                else if(currentDate > m_StartDate && currentDate < m_EndDate)
                {
                    CalenderButtons[i].IsInBetweenFirstAndSecondSelectedDates();
                }
                else
                {
                    CalenderButtons[i].ForceClear();
                }

                currentDate = currentDate.AddDays(1);
            }
        }


        public void OnCalenderButtonClick(DateTime chosenDate, CalenderButton button)
        {
            if(m_EndDate != null)
            {
                m_StartDate = null;
                m_EndDate = null;
            }

            // assume it's first click
            if(m_StartDate == null)
            {
                m_StartDate = chosenDate;
                m_StartDateBTN = button;
                Refresh();
                return;
            }
            else if(chosenDate < m_StartDate)
            {
                m_StartDate = chosenDate;
                m_StartDateBTN = button;
                Refresh();
                return;
            }

            // if we get here we can assume the user is selecting the last date
            m_EndDate = chosenDate;
            m_EndDateBTN = button;
            Refresh();
               
            }
        }
    }
}