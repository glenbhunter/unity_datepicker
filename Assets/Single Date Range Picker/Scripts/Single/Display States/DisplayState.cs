using System;
using UnityEngine;
using UnityEngine.UI;
public class DisplayState : MonoBehaviour
{
    protected Image PrimaryImage { get; private set; }
    protected Image SecondaryImage { get; private set; }
    protected Text ButtonText { get; private set; }
    public virtual void Setup(Image primaryImage, Image secondaryImage, Text buttonText)
    {
        PrimaryImage = primaryImage;
        SecondaryImage = secondaryImage;
        ButtonText = buttonText;
    }

    /// <summary>
    /// 
    /// </summary>
    /// <param name="dayInMonth">Is the current calender button, in the current calender date? Or does it need to be greyed out</param>
    public virtual void UpdateState(DateTime? buttonDate, DateTime? calenderDate, DateTime? selectedStartDate, DateTime? selectedEndDate)
    {
    }
}

