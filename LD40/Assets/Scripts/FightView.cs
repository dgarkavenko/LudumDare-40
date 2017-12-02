using System.Collections.Generic;
using UnityEngine;

public class FightView : MonoBehaviour
{
    public Fight Fight;
}

public class Fight
{
    public readonly List<Cat> Participants;
    private readonly FightView _fightView;

    public Fight(Cat firstCat, Cat secondCat)
    {
        Participants = new List<Cat> { firstCat, secondCat };
        firstCat.State = new Cat.Fighting(firstCat, this);
        secondCat.State = new Cat.Fighting(secondCat, this);

        _fightView = Object.Instantiate(Links.Instance.FightView, firstCat.transform.position, Quaternion.identity, firstCat._raft.parent);
        _fightView.Fight = this;
    }

    public void Join(Cat cat)
    {
        Participants.Add(cat);
        cat.State = new Cat.Fighting(cat, this);
    }

    public void Stop()
    {
        foreach (var participant in Participants)
            ((Cat.Fighting)participant.State).Stop();

        Participants.Clear();

        Object.Destroy(_fightView.gameObject);
    }
}