using System;
using System.Collections.Generic;
using System.Linq;
using Unity.VisualScripting;
using UnityEngine;

/// <summary>
/// 
/// </summary>
public class PuzzleAnim : Puzzle
{
    public GameObject TriggerContent;
    
    [SerializeField]
    private event Action OnFoundHidedElement;

    
}
