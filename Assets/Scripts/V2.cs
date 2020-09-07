using System;
using UnityEngine;

public class V2 : MonoBehaviour
{
    public enum State
    {
        // pointers
        Pointer_Normal,
        Pointer_Pressed,
        Pointer_Hover,
        Pointer_Highlighted,

        // overrides
        Override_HiglightFirstSelected,
        Override_HighlightInBetween,
        Override_HighlightSecondSelected
    }
    
    private DateTime m_CalenderDate;
    private DateTime m_ButtonDate;

    private void Setup(DateTime calenderDate, DateTime buttonDate)
    {
        m_CalenderDate = calenderDate;
        m_ButtonDate = buttonDate;
    }

    #region IPointers


    #endregion


    private void ChangePrimaryImage()
    {
    }

    private void ChangeSecondaryImage()
    {
    }
}
