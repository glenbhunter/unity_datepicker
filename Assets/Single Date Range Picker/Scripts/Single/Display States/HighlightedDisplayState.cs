using UnityEngine;
using UnityEngine.UI;

using System;

public class HighlightedDisplayState : DisplayState
{
    [SerializeField] Image m_Primary_Image;
    [SerializeField] Image m_Highlight_Image;

    [SerializeField] Sprite m_FirstSelectionDate_HighlightSprite;
    [SerializeField] Sprite m_InBetween_Dates_HighlightSprite;
    [SerializeField] Sprite m_LastSelectionDate_HighlightSprite;

    [SerializeField] Color32 m_Highlight_Image_Color;

    public override void UpdateState(DateTime? buttonDate, DateTime? calenderDate, DateTime? selectedStartDate, DateTime? selectedEndDate)
    {
        if(buttonDate == selectedStartDate)
        {
            m_Highlight_Image.sprite = m_FirstSelectionDate_HighlightSprite;
        }
        else if(buttonDate > selectedStartDate && buttonDate < selectedEndDate)
        {
            m_Highlight_Image.sprite = m_InBetween_Dates_HighlightSprite;
            m_Primary_Image.color = Color.clear;
        }
        else if(buttonDate == selectedEndDate)
        {
            m_Highlight_Image.sprite = m_LastSelectionDate_HighlightSprite;
        }

        m_Highlight_Image.color = m_Highlight_Image_Color;
    }
}
