using System;
using UnityEngine;

public class DisableDisplayState : DisplayState
{
    public override void UpdateState(DateTime? buttonDate, DateTime? calenderDate, DateTime? selectedStartDate, DateTime? selectedEndDate)
    {
        UITweenManager.ForceTween(PrimaryImage, Color.clear, null, 0f);
        UITweenManager.ForceTween(ButtonText, Color.clear, null, 0f);
        UITweenManager.ForceTween(SecondaryImage, Color.clear, null, 0f);
    }
}
