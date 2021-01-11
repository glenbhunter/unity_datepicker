using System;
using GlenHunter;
using UnityEngine;


public class DisableDisplayState : DisplayState
{
    public override void UpdateState(DateTime? buttonDate, DateTime? calenderDate, DateTime? selectedStartDate, DateTime? selectedEndDate)
    {
        UITween.ForceColor(PrimaryImage, Color.clear, null, 0f);
        UITween.ForceColor(ButtonText, Color.clear, null, 0f);
        UITween.ForceColor(SecondaryImage, Color.clear, null, 0f);
    }
}
