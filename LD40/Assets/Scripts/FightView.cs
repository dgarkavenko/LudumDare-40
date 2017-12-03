using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class FightView : MonoBehaviour
{
    public Fight Fight;

#if UNITY_EDITOR
    private void OnGUI()
    {
        var pos = Camera.main.WorldToScreenPoint(transform.position);
        GUI.Label(new Rect(pos.x - 40f, Screen.height - pos.y - 50f, 200, 200), "  Fight:\n" + string.Join("\n", Fight.Participants.Select(x => x.Name)));
    }
#endif
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

//        Debug.Log(firstCat.Name + " started a fight with " + secondCat.Name);

        _fightView = Object.Instantiate(Links.Instance.FightView, firstCat.transform.position, Quaternion.identity, firstCat._raft.parent);
        _fightView.Fight = this;
    }

    public void Join(Cat cat)
    {
        Participants.Add(cat);
//        Debug.Log(cat.Name + " joins a fight of " + string.Join(", ", Participants.Select(x => x.Name)));
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