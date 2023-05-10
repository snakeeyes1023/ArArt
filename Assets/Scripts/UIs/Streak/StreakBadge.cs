using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Correspond � un badge
/// </summary>
public class StreakBadge : MonoBehaviour
{
    [SerializeField]
    private int enableStreak; // le nombre de coups r�ussis cons�cutifs pour afficher ce badge

    /// <summary>
    /// V�rifie si le badge doit �tre afficher et si oui il l'affiche
    /// </summary>
    /// <param name="currentStreak">The current streak.</param>
    public void VerifyVisibility(int currentStreak)
    {
        bool isActive = currentStreak > enableStreak;
        gameObject.SetActive(isActive);
    }
}
