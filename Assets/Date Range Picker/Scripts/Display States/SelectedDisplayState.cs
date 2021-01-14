using UnityEngine;
using System;

public class SelectedDisplayState : DisplayState
{
    [SerializeField] Color32 m_BtnImageColor;
    [SerializeField] Color32 m_BtnTextColor;

    public override void UpdateState(DateTime? buttonDate, DateTime? calenderDate, DateTime? selectedStartDate, DateTime? selectedEndDate)
    {
        UITweenManager.ForceTween(PrimaryImage, m_BtnImageColor, null, .1f);
        UITweenManager.ForceTween(ButtonText, m_BtnTextColor, null, .1f);
    }
}
