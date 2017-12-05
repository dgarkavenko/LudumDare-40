using System;
using UnityEngine;

public class Cat : MonoBehaviour
{
    [SerializeField] public Transform _raft;
    [SerializeField] private CatMovement _sixWayMovement;
    [SerializeField] private CharacterController _characterController;
    [SerializeField] private SpriteRenderer _spriteRenderer;

    [SerializeField] private Renderer _renderer;
    [SerializeField] private Collider _collider;
    [SerializeField] private Rigidbody _rigidbody;

    [SerializeField] private DrowningCat _drowningCat;

    public bool DrownedForGood;

    public MainApplication MainApplication;

    public string Name;

    public float SlopeLimit = 3f;
    public float SlideFriction = 0.3f;

    public const float RaftSurfaceY = 0.58f;
    public const float HangingTime = 10f;

    public float MaxSpeed = 0.01f;

    public float LastFightTime;

    public abstract class CatState { };

    public bool FaceUp;
    public float X;

    public class Walking : CatState
    {
        public class AttackTarget
        {
            public readonly Cat Me;
            public readonly Cat Target;

            public AttackTarget(Cat me, Cat target)
            {
                Me = me;
                Target = target;
            }

            public void Attack()
            {
                if (Target._state is Walking)
                    new Fight(Me, Target);
            }
        }

        public class PossibleFight
        {
            public readonly Cat Me;
            public readonly Fight Fight;

            public PossibleFight(Cat me, Fight fight)
            {
                Me = me;
                Fight = fight;
            }

            public void Join()
            {
                Fight.Join(Me);
            }
        }

        public readonly Cat Cat;
        public Transform Waypoint;
        public AttackTarget PossibleAttackTarget;
        public PossibleFight NearbyFight;

        public Walking(Cat cat) { Cat = cat; }

        public void Move(Vector3 direction2D)
        {
            Cat.X = direction2D.x;

            if (direction2D.z != 0)
                Cat.FaceUp = direction2D.z > 0;

            Cat.UpdateVisuals();

            var clampedDirection = Vector2.ClampMagnitude(new Vector2(direction2D.x, direction2D.z), Cat.MaxSpeed);
            var direction3D = new Vector3(clampedDirection.x, -1, clampedDirection.y);

            //RaycastHit hit;
            //if (Physics.Raycast(Cat.transform.position, Vector3.down, out hit, LayerMask.GetMask("Raft"))) {
            //    var hitNormal = hit.normal;

            //    if (Vector3.Angle(Vector3.up, hitNormal) <= Cat.SlopeLimit) {
            //        direction3D.x += (1f - hitNormal.y) * hitNormal.x * (1f - Cat.SlideFriction);
            //        direction3D.z += (1f - hitNormal.y) * hitNormal.z * (1f - Cat.SlideFriction);
            //    }
            //}

            Cat._characterController.Move(direction3D * Time.deltaTime * 35f);

            if (Waypoint != null && Vector3.Distance(Cat.transform.position, Waypoint.position) < 0.5f) {
                Destroy(Waypoint.gameObject);
                Waypoint = null;
            }
        }

        public void SetWaypoint(Vector3 waypointPosition)
        {
            if (Waypoint == null) {
                Waypoint = new GameObject("Waypoint").transform;
                Waypoint.SetParent(Cat._raft);
            }

            Waypoint.position = waypointPosition;
        }

        public void OnDrawGizmos()
        {
            if (Waypoint != null) {
                Gizmos.DrawCube(Waypoint.position, Vector3.one * 0.1f);
                Gizmos.DrawLine(Cat.transform.position, Waypoint.position);
            }

            if (PossibleAttackTarget != null) {
                Gizmos.color = Color.yellow;
                Gizmos.DrawSphere(Cat.transform.position + Vector3.up, 0.3f);
            }

            if (NearbyFight != null) {
                Gizmos.color = Color.red;
                Gizmos.DrawSphere(Cat.transform.position + Vector3.up * 1.5f, 0.3f);
            }
        }

        public void OnTriggerEnter(Collider other)
        {
            var edge = other.GetComponent<Edge>();

            if (edge != null) {
                Cat.State = new Hanging(Cat);
            } else {
                var anotherCat = other.transform.parent?.GetComponent<Cat>();

                if (anotherCat?._state is Walking) {
                    PossibleAttackTarget = new AttackTarget(Cat, anotherCat);
                }

                var fight = other.GetComponent<FightView>();

                if (fight != null) {
                    NearbyFight = new PossibleFight(Cat, fight.Fight);
                }
            }
        }

        public void OnTriggerExit(Collider other)
        {
            var anotherCat = other.transform.parent?.GetComponent<Cat>();

            if (anotherCat != null) {
                PossibleAttackTarget = null;
            }

            var fight = other.GetComponent<FightView>();

            if (fight != null) {
                NearbyFight = null;
            }
        }
    }

    public class Hanging : CatState
    {
        public readonly float StartTime;

        public Hanging(Cat cat)
        {
            StartTime = Time.time;
            var directionToRaftCenter = cat.transform.localPosition;
            var directionToCamera = cat.transform.position - Camera.main.transform.position;
            var angle = Vector3.Angle(directionToRaftCenter, directionToCamera);
            Debug.DrawLine(cat.transform.position + Vector3.up, Vector3.up, Color.red, 3f);
            cat.FaceUp = angle > 90f;
        }
    }

    public class Fighting : CatState
    {
        public readonly Cat Cat;
        public readonly Fight Fight;

        public Fighting(Cat cat, Fight fight)
        {
            Cat = cat;
            Fight = fight;

            Cat._collider.enabled = false;
        }

        public void Stop()
        {
            Cat.State = new Walking(Cat);
            Cat.LastFightTime = Time.time;
            Cat._collider.enabled = true;
        }

        public void Catapult()
        {
            Cat.State = new Flying(Cat);
            Cat._collider.enabled = true;
        }
    }

    public class Drowning : CatState
    {
        public readonly Cat Cat;

        public Drowning(Cat cat)
        {
            Cat = cat;
        }
    }

    public class Flying : CatState
    {
        public readonly Cat Cat;
        private readonly float _startTime;

        public Flying(Cat cat)
        {
            Cat = cat;
            _startTime = Time.time;
            Cat._rigidbody.isKinematic = false;
            var force = new Vector3(0f, 300f, 100f * (UnityEngine.Random.Range(0, 2) - 1));
            force = Cat.transform.TransformDirection(force);
            Cat._rigidbody.AddForce(force);
        }

        public void OnDrawGizmos()
        {
            var rigidbody = Cat.GetComponent<Rigidbody>();
            Gizmos.DrawLine(Cat.transform.position, Cat.transform.position + rigidbody.velocity * 10f);
        }

        public void OnTriggerEnter(Collider other)
        {
            if ((Time.time - _startTime > 0.5f) && other.gameObject.layer == LayerMask.NameToLayer("Raft")) {
                Cat.State = new Walking(Cat);
                Cat.transform.parent = Cat._raft.parent;
                Cat.MainApplication.PickCat(Cat);
            }
        }
    }

    private CatState _state;
    public CatState State
    {
        get { return _state; }
        set
        {
            //Debug.Log($"{Name} -> {_state?.GetType().Name} to {value.GetType().Name}");

            _state = value;
            _rigidbody.isKinematic = !(value is Flying);

            if (_drowningCat.Model != null)
                _drowningCat.Model.GetComponent<SimpleFloating>().enabled = _state is Drowning;

            _drowningCat.enabled = _state is Drowning;

            if (value is Walking) {
                var localPos = transform.localPosition;
                localPos.y = RaftSurfaceY;
                transform.localPosition = localPos;
            }

            UpdateVisuals();
        }
    }

    public void PickKitty()
    {
        var shift = Vector3.ClampMagnitude(new Vector3(0f, RaftSurfaceY, 0f) - transform.localPosition, 1f);
        transform.localPosition += shift;
        State = new Walking(this);
    }

    private void Update()
    {
        var walking = _state as Walking;

        if (walking != null && walking.Waypoint != null)
            walking.Move(walking.Waypoint.position - transform.position);

        var hanging = _state as Hanging;

        if (hanging != null && Time.time > hanging.StartTime + HangingTime) {
            State = new Drowning(this);
            transform.SetParent(_raft.parent.parent);
            var model = GetComponent<DrowningCat>().Model;
            if (model != null) {
                var collider = model.GetComponent<Collider>();

                if (collider != null)
                    collider.enabled = false;
            }

            DrownedForGood = true;

            //_rigidbody.AddForce(transform.TransformDirection(transform.localPosition) * 100f, ForceMode.Acceleration);
            MainApplication.LoseCat(this);
        }

        var flying = _state as Flying;

        if (flying != null) {
            if (transform.position.y <= Stream.WATER_LEVEL) {
                State = new Drowning(this);
                transform.SetParent(_raft.parent.parent);
                MainApplication.LoseCat(this);
            }
        }
    }

#if UNITY_EDITOR
    private void OnGUI()
    {
        //var pos = Camera.main.WorldToScreenPoint(transform.position);
        //GUI.Label(new Rect(pos.x - 40f, Screen.height - pos.y - 50f, 200, 200), $"  {Name} -- {_state.GetType().Name}");
    }
#endif

    private void OnDrawGizmos()
    {
        (_state as Walking)?.OnDrawGizmos();
        (_state as Flying)?.OnDrawGizmos();
    }

    private void OnTriggerEnter(Collider other)
    {
        (_state as Walking)?.OnTriggerEnter(other);
        (_state as Flying)?.OnTriggerEnter(other);
    }

    private void OnTriggerExit(Collider other)
    {
        (_state as Walking)?.OnTriggerExit(other);
    }

    private void UpdateVisuals()
    {
        _spriteRenderer.sprite = GetSprite();
    }

    private Sprite GetSprite()
    {
        _renderer.enabled = !(_state is Fighting);

        return _sixWayMovement.GetSprite(FaceUp, X, _state);
    }
}
