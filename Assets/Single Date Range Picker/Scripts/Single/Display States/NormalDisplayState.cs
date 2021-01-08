using System;
using GlenHunter;
using UnityEngine;
using UnityEngine.UI;
public class NormalDisplayState : DisplayState
{
    [SerializeField] Color32 m_BtnImageColor;
    [SerializeField] Color32 m_BtnTextColor;

    [Header("Is current day, is not in month & days in month are displayed")]
    [SerializeField] Color32 m_Btn_ImageColor_NotInMonth;
    [SerializeField] Color32 m_Btn_TextColor_NotInMonth;

    [SerializeField] Text m_Text;
    [SerializeField] Image m_Image;
    [SerializeField] Image m_SecondaryImage;

    public override void UpdateState(DateTime? buttonDate, DateTime? calenderDate, DateTime? selectedStartDate, DateTime? selectedEndDate)
    {
        m_SecondaryImage.color = Color.clear;

        if (buttonDate != null && calenderDate != null)
        {
            if (buttonDate.Value.Month == calenderDate.Value.Month)
            {
                UITween.ForceColor(m_Image, m_BtnImageColor, null, 0f);
                UITween.ForceColor(m_Text, m_BtnTextColor, null, 0f);
            }
            else
            {
                UITween.ForceColor(m_Image, m_Btn_ImageColor_NotInMonth, null, 0f);
                UITween.ForceColor(m_Text, m_Btn_TextColor_NotInMonth, null, 0f);
            }
        }
        else
        {
            Debug.LogError("UHOH: buttonDate or calenderDate == null");
        }
    }
}