using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StreakBadge : MonoBehaviour
{
    [SerializeField]
    private int enableStreak;

    public void VerifyVisibility(int currentStreak)
    {
        bool isActive = currentStreak > enableStreak;
        gameObject.SetActive(isActive);
    }
}
