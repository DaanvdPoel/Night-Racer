using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class CountDown : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI m_textObject;          // Text that shows the counting.

    private Car m_carScript;
    private UImanager m_lapTime;
    //private int m_currentCountDown = 3;

    private void Start()
    {
        m_carScript = FindObjectOfType<Car>();
        m_lapTime = FindObjectOfType<UImanager>();
        m_carScript.enabled = false;
        m_lapTime.timeRunning = false;

        StartCoroutine(Counting());
    }

    private IEnumerator Counting()
    {
        for (int _i = 3; _i >= -1; _i--)
        {
            switch (_i)
            {
                case -1:
                    m_carScript.enabled = true;
                    m_lapTime.timeRunning = true;
                    yield return new WaitForSeconds(0);
                    break;

                case 0:
                    m_textObject.text = "Go!";
                    break;

                default:
                    m_textObject.text = _i.ToString();
                    break;
            }

            yield return new WaitForSeconds(1);
        }

        gameObject.SetActive(false);
    }
}
