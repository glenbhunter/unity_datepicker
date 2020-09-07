using System;
using UnityEngine;

namespace GlenHunter
{
    [ExecuteInEditMode]
    public class SpawnCalenderButtons : MonoBehaviour
    {
        [SerializeField] bool m_Spawn = false;
        [SerializeField] Calender m_Calender;
        [SerializeField] CalenderButton m_CalenderButtonPrefab;
        [SerializeField] Transform[] m_Rows;

        private void Update()
        {
            if (m_Spawn)
            {
                m_Spawn = false;

                Remove_CalenderButtons();

                m_Calender.CalenderButtons = new System.Collections.Generic.List<CalenderButton>();

                Spawn_CalenderButtons();
            }
        }

        /// <summary>
        /// Remove all buttons
        /// </summary>
        private void Remove_CalenderButtons()
        {
            for (int i = 0; i < m_Rows.Length; i++)
            {
                CalenderButton[] m_Buttons = m_Rows[i].GetComponentsInChildren<CalenderButton>();

                for (int x = 0; x < m_Buttons.Length; x++)
                {
                    DestroyImmediate(m_Buttons[x].gameObject);
                }
            }
        }

        private void Spawn_CalenderButtons()
        {
            int currentRow = 0;

            for (int i = 0; i < 42; i++)
            {
                if (i % 7 == 0)
                    currentRow = i / 7;

                GameObject gameObject = Instantiate(m_CalenderButtonPrefab.gameObject, m_Rows[currentRow].transform, false);
                m_Calender.CalenderButtons.Add(gameObject.GetComponent<CalenderButton>());
            }

            m_Calender.SetupCalenderGrid(DateTime.Now);
        }
    }
}
