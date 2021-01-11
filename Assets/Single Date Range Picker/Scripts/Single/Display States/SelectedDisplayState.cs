using GlenHunter;
using UnityEngine;
using UnityEngine.UI;
using System;

public class SelectedDisplayState : DisplayState
{
    [SerializeField] Color32 m_BtnImageColor;
    [SerializeField] Color32 m_BtnTextColor;

    public override void UpdateState(DateTime? buttonDate, DateTime? calenderDate, DateTime? selectedStartDate, DateTime? selectedEndDate)
    {
        UITween.ForceColor(PrimaryImage, m_BtnImageColor, null, .1f);
        UITween.ForceColor(ButtonText, m_BtnTextColor, null, .1f);
    }
}
