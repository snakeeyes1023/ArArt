using System;
using System.Collections.Generic;
using System.Linq;
using Unity.VisualScripting;
using UnityEngine;
using Vuforia;

/// <summary>
/// 
/// </summary>
public class PuzzleAnim : Puzzle
{

    [SerializeField]
    private event Action<PuzzleAnim> OnFoundHidedElement;
    
    private VirtualButtonBehaviour Vb;
    private Animation Animation;

    public override int AvailablePoint => base.AvailablePoint + BonusPoint;
    public int BonusPoint => 1;

    private void Start()
    {
        Vb = GetComponentInChildren<VirtualButtonBehaviour>();
        Animation = GetComponentInChildren<Animation>();

        if (Vb == null)
        {
            throw new Exception("Aucun bouton trouvé.");
        }
        
        Vb.RegisterOnButtonReleased((vb) => OnButtonRelease());
    }

    private void OnButtonRelease()
    {
        if (Animation.isPlaying)
        {
            return;
        }

        Animation.Play();
        
        OnFoundHidedElement?.Invoke(this);
    }

    public override void AttachController(GameController controller)
    {
        base.AttachController(controller);

        OnFoundHidedElement += controller.OnTreasureFounded;
    }

    public override void DetachController(GameController controller)
    {
        base.DetachController(controller);

        OnFoundHidedElement -= controller.OnTreasureFounded;
    }
}
