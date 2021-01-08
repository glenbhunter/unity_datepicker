using System;
using UnityEngine;

public class DisplayState : MonoBehaviour
{
    /// <summary>
    /// 
    /// </summary>
    /// <param name="dayInMonth">Is the current calender button, in the current calender date? Or does it need to be greyed out</param>
    public virtual void UpdateState(DateTime? buttonDate, DateTime? calenderDate, DateTime? selectedStartDate, DateTime? selectedEndDate)
    {
    }

}
