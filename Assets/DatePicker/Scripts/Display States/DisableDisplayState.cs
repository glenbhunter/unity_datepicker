using System;
using GlenHunter;
using UnityEngine;
using UnityEngine.UI;

public class DisableDisplayState : DisplayState
{
    [SerializeField] Text m_Text;
    [SerializeField] Image m_Image;
    [SerializeField] Image m_SecondaryImage;

    public override void UpdateState(DateTime? buttonDate, DateTime? calenderDate, DateTime? selectedStartDate, DateTime? selectedEndDate)
    {
        UITween.ForceColor(m_Image, Color.clear, null, 0f);
        UITween.ForceColor(m_Text, Color.clear, null, 0f);
    }
}
