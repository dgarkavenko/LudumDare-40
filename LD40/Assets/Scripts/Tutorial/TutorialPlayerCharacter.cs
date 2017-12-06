using System;

public class TutorialPlayerCharacter : PlayerCharacter
{
    public Action OnPoleGrab;

    public override void Init(RaftStick stick, float xScale, float zScale, Action<string> onInteractionEnter, Action onInteractionExit,
        Action<float, Action> onInteraction, Action<bool> raftControl)
    {
        base.Init(stick, xScale, zScale, onInteractionEnter, onInteractionExit, onInteraction, raftControl);
    }
}