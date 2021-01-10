﻿using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Dual_DateText : MonoBehaviour
{
    [SerializeField] Dual_DateRangePicker m_DatePicker;
    [SerializeField] Text m_DateText;

    private void Start()
    {
        m_DatePicker.CalendersUpdated += CalenderUpdated;
    }

    public void CalenderUpdated(DateTime? selectedStartDate, DateTime? selectedEndDate)
    {
        string text = "";

        if(selectedStartDate != null)
        {
            text += selectedStartDate.Value.ToShortDateString();
        }

        if(selectedEndDate != null)
        {
            text += " - " + selectedEndDate.Value.ToShortDateString();
        }

        m_DateText.text = text;
    }
}
