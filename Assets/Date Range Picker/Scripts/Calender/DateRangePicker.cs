using UnityEngine;
using System;

public class DateRangePicker : MonoBehaviour
{
    public delegate void CalenderUpdate(DateTime? selectedStartDate, DateTime? selectedEndDate);
    public CalenderUpdate CalendersUpdated;
}
