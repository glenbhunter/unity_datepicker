using UnityEngine;
using System;

public class HoverDisplayState : DisplayState
{
    [SerializeField] Color32 m_BtnImageColor;
    [SerializeField] Color32 m_BtnTextColor;

    public override void UpdateState(DateTime? buttonDate, DateTime? calenderDate, DateTime? selectedStartDate, DateTime? selectedEndDate)
    {
        UITweenManager.ForceTween(PrimaryImage, m_BtnImageColor, null, 0f);
        UITweenManager.ForceTween(ButtonText, m_BtnTextColor, null, 0f);
    }
}
