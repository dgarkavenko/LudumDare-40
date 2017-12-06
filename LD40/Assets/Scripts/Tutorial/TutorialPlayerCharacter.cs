using System;

public class TutorialPlayerCharacter : PlayerCharacter
{
    public Action OnPoleGrab;

    protected override void GrabThePole()
    {
        if (OnPoleGrab == null)
            return;

        OnPoleGrab.Invoke();

        base.GrabThePole();

        OnPoleGrab = null;
    }
}