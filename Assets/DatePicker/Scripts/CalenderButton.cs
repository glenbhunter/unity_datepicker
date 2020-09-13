
using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class CalenderButton : MonoBehaviour, IPointerEnterHandler, IPointerDownHandler, IPointerExitHandler
{
    public enum State
    {
        Normal,
        Hover,
        Selected,
        Highlighted,
    }

    [SerializeField] Text m_Text;

    private Dictionary<State, DisplayState> m_DisplayDictionary;
    [SerializeField] DisplayState m_NormalState;
    [SerializeField] DisplayState m_SelectedState;
    [SerializeField] DisplayState m_HoverState;
    [SerializeField] DisplayState m_HighlightedState;

    public State CurrentState { get; private set; }

    private int m_ButtonIndex;
    private Calender m_Calender;
    private DateTime m_ButtonDate;

    public void Setup(int buttonIndex, Calender calender, DateTime buttonDate, string text)
    {
        m_ButtonIndex = buttonIndex;
        m_Calender = calender;
        m_ButtonDate = buttonDate;

        m_Text.text = text;

        m_DisplayDictionary = new Dictionary<State, DisplayState>();
        m_DisplayDictionary.Add(State.Normal, m_NormalState);
        m_DisplayDictionary.Add(State.Hover, m_HoverState);
        m_DisplayDictionary.Add(State.Selected, m_SelectedState);
        m_DisplayDictionary.Add(State.Highlighted, m_HighlightedState);

        // Force normal display script to trigger
        UpdateState(State.Normal, m_Calender.CalenderDate, null, null);
    }


    public void OnPointerEnter(PointerEventData eventData)
    {
        m_Calender.OnPointerEnter(m_ButtonIndex);
    }

    public void OnPointerDown(PointerEventData eventData)
    {
        m_Calender.OnPointerDown(m_ButtonIndex, m_ButtonDate);
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        m_Calender.OnPointerExit(m_ButtonIndex);
    }

    public void UpdateState(State newState, DateTime? calenderDate, DateTime? selectedStartDate, DateTime? selectedEndDate)
    {
        CurrentState = newState;
        m_DisplayDictionary[newState].UpdateState(m_ButtonDate, calenderDate, selectedStartDate, selectedEndDate);
    }
}
