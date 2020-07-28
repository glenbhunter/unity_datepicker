using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MultiSelectionTest : MonoBehaviour
{
    [SerializeField] Button one;
    [SerializeField] Button two;
    [SerializeField] bool m_Debug = false;

    private void Update()
    {
        if(m_Debug)
        {
            Test();
            m_Debug = false;
        }
    }

    private void Test()
    {
        one.OnSelect(null);
   
        two.OnSelect(null);
    }
}
