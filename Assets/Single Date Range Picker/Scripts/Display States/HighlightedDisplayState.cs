using UnityEngine;
using UnityEngine.UI;

using System;

public class HighlightedDisplayState : DisplayState
{
    [SerializeField] Sprite m_FirstSelectionDate_HighlightSprite;
    [SerializeField] Sprite m_InBetween_Dates_HighlightSprite;
    [SerializeField] Sprite m_LastSelectionDate_HighlightSprite;

    [SerializeField] Color32 m_Highlight_Image_Color;

    public override void UpdateState(DateTime? buttonDate, DateTime? calenderDate, DateTime? selectedStartDate, DateTime? selectedEndDate)
    {
        if(buttonDate == selectedStartDate)
        {
            SecondaryImage.sprite = m_FirstSelectionDate_HighlightSprite;
        }
        else if(buttonDate > selectedStartDate && buttonDate < selectedEndDate)
        {
            SecondaryImage.sprite = m_InBetween_Dates_HighlightSprite;
            PrimaryImage.color = Color.clear;
        }
        else if(buttonDate == selectedEndDate)
        {
            SecondaryImage.sprite = m_LastSelectionDate_HighlightSprite;
        }

        SecondaryImage.color = m_Highlight_Image_Color;
    }
}
