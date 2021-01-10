﻿using UnityEngine;
using UnityEngine.UI;

using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;

public class Dual_DateRangePicker : MonoBehaviour
{
    // FW == First Window Calender
    // SW == Second Window Calender
    [SerializeField] DayOfWeek m_FirstDayOfWeek;
    [SerializeField] bool m_ShowDaysInOtherMonths;
    [SerializeField] Calender FW_Calender;
    [SerializeField] Calender SW_Calender;

    public delegate void CalenderUpdate(DateTime? selectedStartDate, DateTime? selectedEndDate);
    public CalenderUpdate CalendersUpdated;

    private DateTime? m_StartDate;
    private CalenderButton m_StartDate_SelectedBTN;

    private DateTime? m_EndDate;
    private CalenderButton m_EndDate_SelectedBTN;

    private List<CalenderButton> m_FW_CalenderButtons_ToRefresh = new List<CalenderButton>();
    private List<CalenderButton> m_SW_CalenderButtons_ToRefresh = new List<CalenderButton>();

    private void Start()
    {
        Setup();
    }

    private void Setup()
    {
        FW_Calender.Date = new DateTime(DateTime.Now.Year, DateTime.Now.Month, DateTime.Now.Day);
        SW_Calender.Date = FW_Calender.Date.AddMonths(1);

        FW_Calender.PointerEnter += OnPointerEnter;
        FW_Calender.PointerDown += OnPointerDown;
        FW_Calender.PointerExit += OnPointerExit;

        SW_Calender.PointerEnter += OnPointerEnter;
        SW_Calender.PointerDown += OnPointerDown;
        SW_Calender.PointerExit += OnPointerExit;

        FW_Calender.Setup(DateTime.Now.Year, DateTime.Now.Month, m_FirstDayOfWeek, m_ShowDaysInOtherMonths, m_StartDate, m_EndDate);
        SW_Calender.Setup(DateTime.Now.Year, DateTime.Now.AddMonths(1).Month, m_FirstDayOfWeek, m_ShowDaysInOtherMonths, m_StartDate, m_EndDate);
    }

    public void OnPointerEnter(CalenderButton chosenCalenderButton, Calender calender)
    {
        if (chosenCalenderButton.CurrentState == CalenderButton.State.Normal && m_EndDate == null)
        {
            chosenCalenderButton.UpdateState(CalenderButton.State.Hover, calender.Date, m_StartDate, m_EndDate);
        }
    }

    public void OnPointerDown(CalenderButton chosenCalenderButton, DateTime chosenDate, Calender calender)
    {
        // clears selection
        if (m_StartDate != null && m_EndDate != null)
        {
            for (int i = 0; i < m_FW_CalenderButtons_ToRefresh.Count; i++)
            {
                m_FW_CalenderButtons_ToRefresh[i].ResetToOriginal();
            }

        
            for (int i = 0; i < m_SW_CalenderButtons_ToRefresh.Count; i++)
            {
                m_SW_CalenderButtons_ToRefresh[i].ResetToOriginal();
            }

            m_StartDate = null;
            m_EndDate = null;

            // don't return on this one
        }

        if (m_StartDate == null && m_EndDate == null)
        {
            if (chosenCalenderButton.CurrentState != CalenderButton.State.Disabled)
            {
                m_StartDate = chosenDate;
                m_StartDate_SelectedBTN = chosenCalenderButton;

                CalendersUpdated?.Invoke(m_StartDate, m_EndDate);
                chosenCalenderButton.UpdateState(CalenderButton.State.Selected, chosenDate, m_StartDate, m_EndDate);
            }
            return;
        }


        // revert, if second date is selected is less that the first chosen ddate
        if (m_StartDate != null && chosenDate < m_StartDate && m_EndDate == null)
        {
            if (chosenCalenderButton.CurrentState != CalenderButton.State.Disabled)
            {
                // revert previous selected start date
                m_StartDate_SelectedBTN.UpdateState(CalenderButton.State.Normal, chosenDate, m_StartDate, m_EndDate);

                m_StartDate = chosenDate;
                m_StartDate_SelectedBTN = chosenCalenderButton;

                CalendersUpdated?.Invoke(m_StartDate, m_EndDate);
                chosenCalenderButton.UpdateState(CalenderButton.State.Selected, chosenDate, m_StartDate, m_EndDate);
            }
                
            return;
        }

        if (m_StartDate != null && m_EndDate == null)
        {
            m_EndDate = chosenDate;
            m_EndDate_SelectedBTN = chosenCalenderButton;

            // select end button
            chosenCalenderButton.UpdateState(CalenderButton.State.Selected, chosenDate, m_StartDate, m_EndDate);

            DateTime date = m_StartDate.Value;

            // - 1 to remove first and last selected
            for (int i = 0; i < (m_EndDate - m_StartDate).Value.TotalDays + 1; i++)
            {
                
                CalenderButton fw_CalenderBTN = FW_Calender.CalenderButtons.Where(x => x.Date == date).FirstOrDefault();
                CalenderButton sw_CalenderBTN = SW_Calender.CalenderButtons.Where(x => x.Date == date).FirstOrDefault();

                if (fw_CalenderBTN != null)
                {
                    Debug.Log("here");
                    fw_CalenderBTN.UpdateState(CalenderButton.State.Highlighted, date, m_StartDate, m_EndDate);
                    m_FW_CalenderButtons_ToRefresh.Add(fw_CalenderBTN);
                }

                if (sw_CalenderBTN != null)
                {
                    sw_CalenderBTN.UpdateState(CalenderButton.State.Highlighted, date, m_StartDate, m_EndDate);
                    m_SW_CalenderButtons_ToRefresh.Add(sw_CalenderBTN);
                }


                date = date.AddDays(1);
            }

            CalendersUpdated?.Invoke(m_StartDate, m_EndDate);

            return;
        }
    }

   

    public void OnPointerExit(CalenderButton chosenCalenderButton, Calender calender)
    {
        if (chosenCalenderButton.CurrentState == CalenderButton.State.Hover)
        {
            if (chosenCalenderButton.CurrentState != CalenderButton.State.Disabled)
                chosenCalenderButton.UpdateState(CalenderButton.State.Normal, calender.Date, m_StartDate, m_EndDate);
        }
    }

    public void OnClick_NextCalenderMonth()
    { 
        FW_Calender.Date = FW_Calender.Date.AddMonths(1);
        SW_Calender.Date = SW_Calender.Date.AddMonths(1);

        FW_Calender.Setup(FW_Calender.Date.Year, FW_Calender.Date.Month, m_FirstDayOfWeek, m_ShowDaysInOtherMonths, m_StartDate, m_EndDate);
        SW_Calender.Setup(SW_Calender.Date.Year, SW_Calender.Date.Month, m_FirstDayOfWeek, m_ShowDaysInOtherMonths, m_StartDate, m_EndDate);
    }

    public void OnClick_PreviousCalenderMonth()
    {
        FW_Calender.Date = FW_Calender.Date.AddMonths(-1);
        SW_Calender.Date = SW_Calender.Date.AddMonths(-1);

        FW_Calender.Setup(FW_Calender.Date.Year, FW_Calender.Date.Month, m_FirstDayOfWeek, m_ShowDaysInOtherMonths, m_StartDate, m_EndDate);
        SW_Calender.Setup(SW_Calender.Date.Year, SW_Calender.Date.Month, m_FirstDayOfWeek, m_ShowDaysInOtherMonths, m_StartDate, m_EndDate);
    }
}
