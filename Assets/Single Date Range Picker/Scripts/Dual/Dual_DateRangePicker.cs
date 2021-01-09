using UnityEngine;
using UnityEngine.UI;

using System;
using System.Collections.Generic;
using System.Globalization;

public class Dual_DateRangePicker : MonoBehaviour
{
    // FW == First Window Calender
    // SW == Second Window Calender
    [SerializeField] DayOfWeek m_FirstDayOfWeek;
    [SerializeField] bool m_ShowDaysInOtherMonths;
    [SerializeField] Calender FW_Calender;
    [SerializeField] Calender SW_Calender;

    private DateTime? m_StartDate;
    private int m_StartDate_SelectedBtnIndex;

    private DateTime? m_EndDate;
    private int m_EndDate_SelectedBtnIndex;

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

        FW_Calender.Setup(DateTime.Now.Year, DateTime.Now.Month, m_FirstDayOfWeek, m_ShowDaysInOtherMonths);
        SW_Calender.Setup(DateTime.Now.Year, DateTime.Now.AddMonths(1).Month, m_FirstDayOfWeek, m_ShowDaysInOtherMonths);
    }

    public void OnPointerEnter(int buttonIndex, Calender calender)
    {
        if (calender.CalenderButtons[buttonIndex].CurrentState == CalenderButton.State.Normal && m_EndDate == null)
        {
            calender.CalenderButtons[buttonIndex].UpdateState(CalenderButton.State.Hover, calender.Date, m_StartDate, m_EndDate);
        }
    }

    public void OnPointerDown(int buttonIndex, DateTime calenderDate, Calender calender)
    {
    }

    public void OnPointerExit(int buttonIndex, Calender calender)
    {
        if (calender.CalenderButtons[buttonIndex].CurrentState == CalenderButton.State.Hover)
        {
            if (calender.CalenderButtons[buttonIndex].CurrentState != CalenderButton.State.Disabled)
                calender.CalenderButtons[buttonIndex].UpdateState(CalenderButton.State.Normal, calender.Date, m_StartDate, m_EndDate);
        }
    }
}
