using System;
using UnityEngine;

public class NormalDisplayState : DisplayState
{
    [SerializeField] Color32 m_BtnImageColor;
    [SerializeField] Color32 m_BtnTextColor;

    [Header("Is current day, is not in month & days in month are displayed")]
    [SerializeField] Color32 m_Btn_ImageColor_NotInMonth;
    [SerializeField] Color32 m_Btn_TextColor_NotInMonth;

    public override void UpdateState(DateTime? buttonDate, DateTime? calenderDate, DateTime? selectedStartDate, DateTime? selectedEndDate)
    {
        SecondaryImage.color = Color.clear;

        if (buttonDate != null && calenderDate != null)
        {
            if (buttonDate.Value.Month == calenderDate.Value.Month)
            {
                UITweenManager.ForceTween(PrimaryImage, m_BtnImageColor, null, 0f);
                UITweenManager.ForceTween(ButtonText, m_BtnTextColor, null, 0f);
            }
            else
            {
                UITweenManager.ForceTween(PrimaryImage, m_Btn_ImageColor_NotInMonth, null, 0f);
                UITweenManager.ForceTween(ButtonText, m_Btn_TextColor_NotInMonth, null, 0f);
            }
        }
        else
        {
            Debug.LogError("UHOH: buttonDate or calenderDate == null");
        }
    }
}