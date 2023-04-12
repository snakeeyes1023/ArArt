using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StreakController : MonoBehaviour
{
    [SerializeField]
    private StreakBadge[] streakBadges;

    [SerializeField]
    private GameController gameController;

    void Start()
    {
        GameController_OnStreakChanged(this, gameController.LongestStreak);
        gameController.OnStreakChanged += GameController_OnStreakChanged;
    }

    private void GameController_OnStreakChanged(object sender, int e)
    {
        int maxStreak = gameController.LongestStreak;
        
        foreach (var streakBadge in streakBadges)
        {
            streakBadge.VerifyVisibility(maxStreak);
        }
    }
}
