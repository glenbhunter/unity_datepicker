﻿using GlenHunter;
using UnityEngine;
using UnityEngine.UI;
using System;

public class SelectedDisplayState : DisplayState
{
    [SerializeField] Color32 m_BtnImageColor;
    [SerializeField] Color32 m_BtnTextColor;

    [SerializeField] Text m_Text;
    [SerializeField] Image m_Image;

    public override void UpdateState(DateTime? buttonDate, DateTime? calenderDate, DateTime? selectedStartDate, DateTime? selectedEndDate)
    {
        UITween.ForceColor(m_Image, m_BtnImageColor, null, .1f);
        UITween.ForceColor(m_Text, m_BtnTextColor, null, .1f);
    }
}
