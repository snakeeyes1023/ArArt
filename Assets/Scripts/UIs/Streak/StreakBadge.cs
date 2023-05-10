using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Correspond à un badge
/// </summary>
public class StreakBadge : MonoBehaviour
{
    [SerializeField]
    private int enableStreak; // le nombre de coups réussis consécutifs pour afficher ce badge

    /// <summary>
    /// Vérifie si le badge doit être afficher et si oui il l'affiche
    /// </summary>
    /// <param name="currentStreak">The current streak.</param>
    public void VerifyVisibility(int currentStreak)
    {
        bool isActive = currentStreak > enableStreak;
        gameObject.SetActive(isActive);
    }
}
