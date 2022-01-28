using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class LapManager : MonoBehaviour
{
    [SerializeField] private List<Collider> m_checkpoints;                      // A list of all checkpoints that have to be met in order.
    [SerializeField] private UnityEvent m_onLapFinish;                          // Call the corresponding function when lap is finished.

    private List<Collider> m_passedCheckPoints = new List<Collider>();          // The checkpoints that have been passed this round.                                 

    private void OnTriggerExit(Collider other)
    {
        // Its unnesesary to compare a tag, but its done to prevent the need for constant looping.
        if (other.CompareTag("Checkpoint"))
        {
            if (m_passedCheckPoints.Count != 0)
            {
                // if you passed all the checkpoints, Only check for the first checkpoint to finish lap.
                if (m_passedCheckPoints.Count == m_checkpoints.Count && other.gameObject == m_checkpoints[0].gameObject)
                {
                    m_onLapFinish.Invoke();
                    m_passedCheckPoints.Clear();
                    return;
                }

                // Check if the checkpoint hasent already been passed.
                foreach (Collider _collider in m_passedCheckPoints)
                {
                    if (other.gameObject == _collider.gameObject)
                    {
                        return;
                    }
                }

                // Check if the touched checkoint is the same as the next in order of the last passed checkpoint.
                // (Checks if you didnt skip a checkpoint)
                if (other.gameObject == m_checkpoints[m_passedCheckPoints.Count].gameObject)
                {
                    m_passedCheckPoints.Add(other);
                    return;
                }
                else {return; }
            }
            else if ( other.gameObject == m_checkpoints[0].gameObject)
            {
                m_passedCheckPoints.Add(other);
            }
        }
    }
}
