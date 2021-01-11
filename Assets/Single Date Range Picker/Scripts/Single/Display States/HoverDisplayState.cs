using GlenHunter;
using UnityEngine;
using UnityEngine.UI;
using System;

public class HoverDisplayState : DisplayState
{
    [SerializeField] Color32 m_BtnImageColor;
    [SerializeField] Color32 m_BtnTextColor;

    public override void UpdateState(DateTime? buttonDate, DateTime? calenderDate, DateTime? selectedStartDate, DateTime? selectedEndDate)
    {
        UITween.ForceColor(PrimaryImage, m_BtnImageColor, null, 0f);
        UITween.ForceColor(ButtonText, m_BtnTextColor, null, 0f);
    }
}
