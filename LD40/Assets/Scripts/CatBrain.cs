using System.Collections;
using UnityEngine;

public class CatBrain : MonoBehaviour
{
    public Cat Cat;

    public float FightCooldown = 5f;

    private void Start()
    {
        StartCoroutine(Idle());
    }

    private void CheckForAction()
    {
        if (Time.time < Cat.LastFightTime + FightCooldown)
            return;

        var walkingCat = (Cat.Walking)Cat.State;

        if (walkingCat.PossibleAttackTarget != null) {
            walkingCat.PossibleAttackTarget.Attack();
        } else if (walkingCat.NearbyFight != null) {
            walkingCat.NearbyFight.Join();
        }
    }

    private IEnumerator Idle()
    {
        while (true) {
            while ((Cat.State as Cat.Walking)?.Waypoint != null) {
                CheckForAction();
                yield return null;
            }

            if (Cat.State is Cat.Walking) {
                var restTime = Time.time + Random.Range(1f, 4f);

                while (Cat.State is Cat.Walking && Time.time < restTime) {
                    CheckForAction();
                    yield return null;
                }
            }

            var walkingCat = Cat.State as Cat.Walking;

            if (walkingCat != null && walkingCat.Waypoint == null) {
                var raftSize = Cat.RaftTransform.localScale;
                var random = new Vector3(
                    Random.Range(raftSize.x * -0.5f + 1.5f, raftSize.x * 0.5f - 1.5f),
                    0f,
                    Random.Range(raftSize.z * -0.5f + 1.5f, raftSize.z * 0.5f - 1.5f));

                var nextWaypointLocal = random;
                random.y = Cat.transform.localPosition.y;
                var nextWaypointWorld = Cat.transform.TransformPoint(nextWaypointLocal);

                walkingCat.SetWaypoint(nextWaypointWorld);
            }

            yield return null;
        }
    }
}
