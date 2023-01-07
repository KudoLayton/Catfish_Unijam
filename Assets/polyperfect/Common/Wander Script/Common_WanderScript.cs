using System;
using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine.AI;
using Quaternion = UnityEngine.Quaternion;
using Random = UnityEngine.Random;
using Vector3 = UnityEngine.Vector3;

#if UNITY_EDITOR
using UnityEditor;
#endif

namespace Polyperfect.Common
{
    [RequireComponent(typeof(Animator)), RequireComponent(typeof(CharacterController))]
    public class Common_WanderScript : MonoBehaviour
    {
        private const float contingencyDistance = 1f;

        [SerializeField] public IdleState[] idleStates;
        [SerializeField] private MovementState[] movementStates;
        [SerializeField] private AIState[] attackingStates;
        [SerializeField] private AIState[] deathStates;

        [SerializeField] public string species = "NA";

        [SerializeField, Tooltip("This specific animal stats asset, create a new one from the asset menu under (LowPolyAnimals/NewAnimalStats)")]
        public AIStats stats;

        [SerializeField, Tooltip("How far away from it's origin this animal will wander by itself.")]
        private float wanderZone = 10f;

        public float MaxDistance
        {
            get { return wanderZone; }
            set
            {
#if UNITY_EDITOR
                SceneView.RepaintAll();
#endif
                wanderZone = value;
            }
        }

        // [SerializeField, Tooltip("How dominent this animal is in the food chain, agressive animals will attack less dominant animals.")]
        private int dominance = 1;
        private int originalDominance = 0;

        [SerializeField, Tooltip("How far this animal can sense a predator.")]
        private float awareness = 30f;

        [SerializeField, Tooltip("How far this animal can sense it's prey.")]
        private float scent = 30f;

        private float originalScent = 0f;

        // [SerializeField, Tooltip("How many seconds this animal can run for before it gets tired.")]
        private float stamina = 10f;

        // [SerializeField, Tooltip("How much this damage this animal does to another animal.")]
        private float power = 10f;

        // [SerializeField, Tooltip("How much health this animal has.")]
        private float toughness = 5f;

        // [SerializeField, Tooltip("Chance of this animal attacking another animal."), Range(0f, 100f)]
        private float aggression = 0f;
        private float originalAggression = 0f;

        // [SerializeField, Tooltip("How quickly the animal does damage to another animal (every 'attackSpeed' seconds will cause 'power' amount of damage).")]
        private float attackSpeed = 0.5f;

        // [SerializeField, Tooltip("If true, this animal will attack other animals of the same specices.")]
        private bool territorial = false;

        // [SerializeField, Tooltip("Stealthy animals can't be detected by other animals.")]
        private bool stealthy = false;

        [SerializeField, Tooltip("If true, this animal will never leave it's zone, even if it's chasing or running away from another animal.")]
        private bool constainedToWanderZone = false;

        [SerializeField, Tooltip("This animal will be peaceful towards species in this list.")]
        private string[] nonAgressiveTowards;

        private static List<Common_WanderScript> allAnimals = new List<Common_WanderScript>();

        public static List<Common_WanderScript> AllAnimals
        {
            get { return allAnimals; }
        }

        //[Space(), Space(5)]
        [SerializeField, Tooltip("If true, this animal will rotate to match the terrain. Ensure you have set the layer of the terrain as 'Terrain'.")]
        private bool matchSurfaceRotation = false;

        [SerializeField, Tooltip("How fast the animnal rotates to match the surface rotation.")]
        private float surfaceRotationSpeed = 2f;

        //[Space(), Space(5)]
        [SerializeField, Tooltip("If true, AI changes to this animal will be logged in the console.")]
        private bool logChanges = false;

        [SerializeField, Tooltip("If true, gizmos will be drawn in the editor.")]
        private bool showGizmos = false;

        [SerializeField] private bool drawWanderRange = true;
        [SerializeField] private bool drawScentRange = true;
        [SerializeField] private bool drawAwarenessRange = true;

        public UnityEngine.Events.UnityEvent deathEvent;
        public UnityEngine.Events.UnityEvent attackingEvent;
        public UnityEngine.Events.UnityEvent idleEvent;
        public UnityEngine.Events.UnityEvent movementEvent;


        private Color distanceColor = new Color(0f, 0f, 205f);
        private Color awarnessColor = new Color(1f, 0f, 1f, 1f);
        private Color scentColor = new Color(1f, 0f, 0f, 1f);
        private Animator animator;
        private CharacterController characterController;
        private NavMeshAgent navMeshAgent;
        private Vector3 origin;

        private int totalIdleStateWeight;

        private bool useNavMesh = false;
        private Vector3 targetLocation = Vector3.zero;

        private float turnSpeed = 0f;

        public enum WanderState
        {
            Idle,
            Wander,
            Chase,
            Evade,
            Attack,
            Dead
        }

        float attackTimer = 0;
        float MinimumStaminaForAggression
        {
            get { return stats.stamina * .9f; }
        }

        float MinimumStaminaForFlee
        {
            get { return stats.stamina * .1f; }
        }

        public WanderState CurrentState;
        Common_WanderScript primaryPrey;
        Common_WanderScript primaryPursuer;
        Common_WanderScript attackTarget;
        float moveSpeed = 0f;
        float attackReach =2f;
        bool forceUpdate = false;
        float idleStateDuration;
        Vector3 startPosition;
        Vector3 wanderTarget;
        IdleState currentIdleState;
        float idleUpdateTime;
        

        public void OnDrawGizmosSelected()
        {
            if (!showGizmos)
                return;

            if (drawWanderRange)
            {
                // Draw circle of radius wander zone
                Gizmos.color = distanceColor;
                Gizmos.DrawWireSphere(origin == Vector3.zero ? transform.position : origin, wanderZone);

                Vector3 IconWander = new Vector3(transform.position.x, transform.position.y + wanderZone, transform.position.z);
                Gizmos.DrawIcon(IconWander, "ico-wander", true);
            }

            if (drawAwarenessRange)
            {
                //Draw circle radius for Awarness.
                Gizmos.color = awarnessColor;
                Gizmos.DrawWireSphere(transform.position, awareness);


                Vector3 IconAwareness = new Vector3(transform.position.x, transform.position.y + awareness, transform.position.z);
                Gizmos.DrawIcon(IconAwareness, "ico-awareness", true);
            }

            if (drawScentRange)
            {
                //Draw circle radius for Scent.
                Gizmos.color = scentColor;
                Gizmos.DrawWireSphere(transform.position, scent);

                Vector3 IconScent = new Vector3(transform.position.x, transform.position.y + scent, transform.position.z);
                Gizmos.DrawIcon(IconScent, "ico-scent", true);
            }

            if (!Application.isPlaying)
                return;

            // Draw target position.
            if (useNavMesh)
            {
                if (navMeshAgent.remainingDistance > 1f)
                {
                    Gizmos.DrawSphere(navMeshAgent.destination + new Vector3(0f, 0.1f, 0f), 0.2f);
                    Gizmos.DrawLine(transform.position, navMeshAgent.destination);
                }
            }
            else
            {
                if (targetLocation != Vector3.zero)
                {
                    Gizmos.DrawSphere(targetLocation + new Vector3(0f, 0.1f, 0f), 0.2f);
                    Gizmos.DrawLine(transform.position, targetLocation);
                }
            }
        }

        private void Awake()
        {
            if (!stats)
            {
                Debug.LogError(string.Format("No stats attached to {0}'s Wander Script.", gameObject.name));
                enabled = false;
                return;
            }

            animator = GetComponent<Animator>();

            var runtimeController = animator.runtimeAnimatorController;
            if (animator)
                animatorParameters.UnionWith(animator.parameters.Select(p=>p.name));
            
            if (logChanges)
            {
                if (runtimeController == null)
                {
                    Debug.LogError(string.Format(
                        "{0} has no animator controller, make sure you put one in to allow the character to walk. See documentation for more details (1)",
                        gameObject.name));
                    enabled = false;
                    return;
                }

                if (animator.avatar == null)
                {
                    Debug.LogError(string.Format("{0} has no avatar, make sure you put one in to allow the character to animate. See documentation for more details (2)",
                        gameObject.name));
                    enabled = false;
                    return;
                }

                if (animator.hasRootMotion == true)
                {
                    Debug.LogError(string.Format(
                        "{0} has root motion applied, consider turning this off as our script will deactivate this on play as we do not use it (3)", gameObject.name));
                    animator.applyRootMotion = false;
                }

                if (idleStates.Length == 0 || movementStates.Length == 0)
                {
                    Debug.LogError(string.Format("{0} has no idle or movement states, make sure you fill these out. See documentation for more details (4)",
                        gameObject.name));
                    enabled = false;
                    return;
                }

                if (idleStates.Length > 0)
                {
                    for (int i = 0; i < idleStates.Length; i++)
                    {
                        if (idleStates[i].animationBool == "")
                        {
                            Debug.LogError(string.Format(
                                "{0} has " + idleStates.Length +
                                " Idle states, you need to make sure that each state has an animation boolean. See documentation for more details (4)", gameObject.name));
                            enabled = false;
                            return;
                        }
                    }
                }

                if (movementStates.Length > 0)
                {
                    for (int i = 0; i < movementStates.Length; i++)
                    {
                        if (movementStates[i].animationBool == "")
                        {
                            Debug.LogError(string.Format(
                                "{0} has " + movementStates.Length +
                                " Movement states, you need to make sure that each state has an animation boolean to see the character walk. See documentation for more details (4)",
                                gameObject.name));
                            enabled = false;
                            return;
                        }

                        if (movementStates[i].moveSpeed <= 0)
                        {
                            Debug.LogError(string.Format(
                                "{0} has a movement state with a speed of 0 or less, you need to set the speed higher than 0 to see the character move. See documentation for more details (4)",
                                gameObject.name));
                            enabled = false;
                            return;
                        }

                        if (movementStates[i].turnSpeed <= 0)
                        {
                            Debug.LogError(string.Format(
                                "{0} has a turn speed state with a speed of 0 or less, you need to set the speed higher than 0 to see the character turn. See documentation for more details (4)",
                                gameObject.name));
                            enabled = false;
                            return;
                        }
                    }
                }

                if (attackingStates.Length == 0)
                {
                    Debug.Log(string.Format("{0} has " + attackingStates.Length + " this character will not be able to attack. See documentation for more details (4)",
                        gameObject.name));
                }

                if (attackingStates.Length > 0)
                {
                    for (int i = 0; i < attackingStates.Length; i++)
                    {
                        if (attackingStates[i].animationBool == "")
                        {
                            Debug.LogError(string.Format(
                                "{0} has " + attackingStates.Length +
                                " attacking states, you need to make sure that each state has an animation boolean. See documentation for more details (4)",
                                gameObject.name));
                            enabled = false;
                            return;
                        }
                    }
                }

                if (stats == null)
                {
                    Debug.LogError(string.Format("{0} has no AI stats, make sure you assign one to the wander script. See documentation for more details (5)",
                        gameObject.name));
                    enabled = false;
                    return;
                }

                if (animator)
                {
                    foreach (var item in AllStates)
                    {
                        if (!animatorParameters.Contains(item.animationBool))
                        {
                            Debug.LogError(string.Format(
                                "{0} did not contain {1}. Make sure you set it in the Animation States on the character, and have a matching parameter in the Animator Controller assigned.",
                                gameObject.name, item.animationBool));
                            enabled = false;
                            return;
                        }
                    }
                }
            }

            foreach (IdleState state in idleStates)
            {
                totalIdleStateWeight += state.stateWeight;
            }

            origin = transform.position;
            animator.applyRootMotion = false;
            characterController = GetComponent<CharacterController>();
            navMeshAgent = GetComponent<NavMeshAgent>();

            //Assign the stats to variables
            originalDominance = stats.dominance;
            dominance = originalDominance;

            toughness = stats.toughness;
            territorial = stats.territorial;

            stamina = stats.stamina;

            originalAggression = stats.agression;
            aggression = originalAggression;

            attackSpeed = stats.attackSpeed;
            stealthy = stats.stealthy;

            originalScent = scent;
            scent = originalScent;

            if (navMeshAgent)
            {
                useNavMesh = true;
                navMeshAgent.stoppingDistance = contingencyDistance;
            }

            if (matchSurfaceRotation && transform.childCount > 0)
            {
                transform.GetChild(0).gameObject.AddComponent<Common_SurfaceRotation>().SetRotationSpeed(surfaceRotationSpeed);
            }
        }

        IEnumerable<AIState> AllStates
        {
            get
            {
                foreach (var item in idleStates)
                    yield return item;
                foreach (var item in movementStates)
                    yield return item;
                foreach (var item in attackingStates)
                    yield return item;
                foreach (var item in deathStates)
                    yield return item;
            }
        }

        void OnEnable()
        {
            allAnimals.Add(this);
        }

        void OnDisable()
        {
            allAnimals.Remove(this);
            StopAllCoroutines();
        }


        private void Start()
        {
            startPosition = transform.position;
            if (Common_WanderManager.Instance != null && Common_WanderManager.Instance.PeaceTime)
            {
                SetPeaceTime(true);
            }

            StartCoroutine(RandomStartingDelay());
        }

        bool started = false;
        readonly HashSet<string> animatorParameters = new HashSet<string>();

        void Update()
        {
            if (!started)
                return;
            if (forceUpdate)
            {
                UpdateAI();
                forceUpdate = false;
            }

            if (CurrentState == WanderState.Attack)
            {
                if (!attackTarget || attackTarget.CurrentState == WanderState.Dead)
                {
                    var previous = attackTarget;
                    UpdateAI();
                    if (previous && previous == attackTarget)
                        Debug.LogError(string.Format("Target was same {0}", previous.gameObject.name));
                }

                attackTimer += Time.deltaTime;
            }

            if (attackTimer>attackSpeed)
            {
                attackTimer -= attackSpeed;
                if (attackTarget)
                    attackTarget.TakeDamage(power);
                if (attackTarget.CurrentState == WanderState.Dead) 
                    UpdateAI();
            }

            var position = transform.position;
            var targetPosition = position;
            switch (CurrentState)
            {
                case WanderState.Attack:
                    FaceDirection((attackTarget.transform.position - position).normalized);
                    targetPosition = position;
                    break;
                case WanderState.Chase:
                    if (!primaryPrey || primaryPrey.CurrentState == WanderState.Dead)
                    {
                        primaryPrey = null;
                        SetState(WanderState.Idle);
                        goto case WanderState.Idle;
                    }
                    targetPosition = primaryPrey.transform.position;
                    ValidatePosition(ref targetPosition);
                    if (!IsValidLocation(targetPosition))
                    {
                        SetState(WanderState.Idle);
                        targetPosition = position;
                        UpdateAI();
                        break;
                    }

                    FaceDirection((targetPosition - position).normalized);
                    stamina -= Time.deltaTime;
                    if (stamina<=0f)
                        UpdateAI();
                    break;
                case WanderState.Evade:
                    targetPosition = position + Vector3.ProjectOnPlane(position - primaryPursuer.transform.position, Vector3.up);
                    if (!IsValidLocation(targetPosition))
                        targetPosition = startPosition;
                    ValidatePosition(ref targetPosition);
                    FaceDirection((targetPosition - position).normalized);
                    stamina -= Time.deltaTime;
                    if (stamina<=0f)
                        UpdateAI();
                    break;
                case WanderState.Wander:
                    stamina = Mathf.MoveTowards(stamina, stats.stamina, Time.deltaTime);
                    targetPosition = wanderTarget;
                    Debug.DrawLine(position,targetPosition,Color.yellow);
                    FaceDirection((targetPosition-position).normalized);
                    var displacementFromTarget = Vector3.ProjectOnPlane(targetPosition - transform.position, Vector3.up);
                    if (displacementFromTarget.magnitude < contingencyDistance)
                    {
                        SetState(WanderState.Idle);
                        UpdateAI();
                    }

                    break;
                case WanderState.Idle:
                    stamina = Mathf.MoveTowards(stamina, stats.stamina, Time.deltaTime);
                    if (Time.time>=idleUpdateTime)
                    {
                        SetState(WanderState.Wander);
                        UpdateAI();
                    }
                    break;
            }

            if (navMeshAgent)
            {
                navMeshAgent.destination = targetPosition;
                navMeshAgent.speed = moveSpeed;
                navMeshAgent.angularSpeed = turnSpeed;
            }
            else
                characterController.SimpleMove(moveSpeed * UnityEngine.Vector3.ProjectOnPlane(targetPosition - position,Vector3.up).normalized);


        }

        void FaceDirection(Vector3 facePosition)
        {
            transform.rotation = Quaternion.LookRotation(Vector3.ProjectOnPlane(Vector3.RotateTowards(transform.forward,
                facePosition, turnSpeed * Time.deltaTime*Mathf.Deg2Rad, 0f), Vector3.up), Vector3.up);
        }

        public void TakeDamage(float damage)
        {
            toughness -= damage;
            if (toughness <= 0f)
                Die();
        }
        public void Die()
        {
            SetState(WanderState.Dead);
        }

        public void SetPeaceTime(bool peace)
        {
            if (peace)
            {
                dominance = 0;
                scent = 0f;
                aggression = 0f;
            }
            else
            {
                dominance = originalDominance;
                scent = originalScent;
                aggression = originalAggression;
            }
        }
        
        void UpdateAI()
        {
            if (CurrentState == WanderState.Dead)
            {
                Debug.LogError("Trying to update the AI of a dead animal, something probably went wrong somewhere.");
                return;
            }

            var position = transform.position;
            primaryPursuer = null;
            if (awareness > 0)
            {
                var closestDistance = awareness;
                if (allAnimals.Count > 0)
                {
                    foreach (var chaser in allAnimals)
                    {
                        if (chaser.primaryPrey != this && chaser.attackTarget != this)
                            continue;

                        if (chaser.CurrentState == WanderState.Dead)
                            continue;
                        var distance = Vector3.Distance(position, chaser.transform.position);
                        if ((chaser.attackTarget!=this&&chaser.stealthy) || chaser.dominance <= this.dominance || distance > closestDistance)
                            continue;
                        
                        closestDistance = distance;
                        primaryPursuer = chaser;
                    }
                }
            }

            var wasSameTarget = false;
            if (primaryPrey)
            {
                if (primaryPrey.CurrentState == WanderState.Dead)
                    primaryPrey = null;
                else
                {
                    var distanceToPrey = Vector3.Distance(position, primaryPrey.transform.position);
                    if (distanceToPrey > scent)
                        primaryPrey = null;
                    else
                        wasSameTarget = true;
                }
            }
            if (!primaryPrey)
            {
                primaryPrey = null;
                if (dominance > 0 && attackingStates.Length>0)
                {
                    var aggFrac = aggression * .01f;
                    aggFrac *= aggFrac;
                    var closestDistance = scent;
                    foreach (var potentialPrey in allAnimals)
                    {
                        if (potentialPrey.CurrentState == WanderState.Dead)
                            Debug.LogError(string.Format("Dead animal found: {0}", potentialPrey.gameObject.name));
                        if (potentialPrey == this || (potentialPrey.species == species && !territorial) ||
                            potentialPrey.dominance > dominance || potentialPrey.stealthy)
                            continue;
                        if (nonAgressiveTowards.Contains(potentialPrey.species))
                            continue;
                        if (Random.Range(0f,0.99999f) >= aggFrac)
                            continue;
                        
                        var preyPosition = potentialPrey.transform.position;
                        if (!IsValidLocation(preyPosition)) 
                            continue;

                        var distance = Vector3.Distance(position, preyPosition);
                        if (distance > closestDistance)
                            continue;
                        if (logChanges)
                            Debug.Log(string.Format("{0}: Found prey ({1}), chasing.", gameObject.name, potentialPrey.gameObject.name));

                        closestDistance = distance;
                        primaryPrey = potentialPrey;
                    }
                }
            }

            var aggressiveOption = false;
            if (primaryPrey)
            {
                if ((wasSameTarget&&stamina>0) || stamina > MinimumStaminaForAggression)
                    aggressiveOption = true;
                else
                    primaryPrey = null;
            }

            var defensiveOption = false;
            if (primaryPursuer && !aggressiveOption)
            {
                if (stamina > MinimumStaminaForFlee)
                    defensiveOption = true;
            }

            var updateTargetAI = false;
            var isPreyInAttackRange = aggressiveOption && Vector3.Distance(position, primaryPrey.transform.position) < CalcAttackRange(primaryPrey);
            var isPursuerInAttackRange = defensiveOption && Vector3.Distance(position, primaryPursuer.transform.position) < CalcAttackRange(primaryPursuer);
            if (isPursuerInAttackRange)
            {
                attackTarget = primaryPursuer;
            }
            else if (isPreyInAttackRange)
            {
                attackTarget = primaryPrey;
                if (!attackTarget.attackTarget==this)
                    updateTargetAI = true;
            }
            else
                attackTarget = null;
            var shouldAttack = attackingStates.Length > 0 && (isPreyInAttackRange || isPursuerInAttackRange);

            if (shouldAttack)
                SetState(WanderState.Attack);
            else if (aggressiveOption)
                SetState(WanderState.Chase);
            else if (defensiveOption)
                SetState(WanderState.Evade);
            else if (CurrentState!= WanderState.Idle && CurrentState != WanderState.Wander)
                SetState(WanderState.Idle);
            if (shouldAttack&&updateTargetAI) 
                attackTarget.forceUpdate = true;
        }

        bool IsValidLocation(Vector3 targetPosition)
        {
            if (!constainedToWanderZone)
                return true;
            var distanceFromWander = Vector3.Distance(startPosition, targetPosition);
            var isInWander = distanceFromWander < wanderZone;
            return isInWander;
        }

        float CalcAttackRange(Common_WanderScript other)
        {
            var thisRange = navMeshAgent ? navMeshAgent.radius : characterController.radius;
            var thatRange = other.navMeshAgent ? other.navMeshAgent.radius : other.characterController.radius;
            return attackReach+thisRange+thatRange;
        }

        void SetState(WanderState state)
        {
            var previousState = CurrentState;
            if (previousState == WanderState.Dead)
            {
                Debug.LogError("Attempting to set a state to a dead animal.");
                return;
            }
            //if (state != previousState)
            {
                CurrentState = state;
                switch (CurrentState)
                {
                    case WanderState.Idle:
                        HandleBeginIdle();
                        break;
                    case WanderState.Chase:
                        HandleBeginChase();
                        break;
                    case WanderState.Evade:
                        HandleBeginEvade();
                        break;
                    case WanderState.Attack:
                        HandleBeginAttack();
                        break;
                    case WanderState.Dead:
                        HandleBeginDeath();
                        break;
                    case WanderState.Wander:
                        HandleBeginWander();
                        break;
                    default:
                        throw new ArgumentOutOfRangeException();
                }
            }
        }


        void ClearAnimatorBools()
        {
            foreach (var item in idleStates) 
                TrySetBool(item.animationBool, false);
            foreach (var item in movementStates) 
                TrySetBool(item.animationBool, false);
            foreach (var item in attackingStates) 
                TrySetBool(item.animationBool, false);
            foreach (var item in deathStates) 
                TrySetBool(item.animationBool, false);
        }
        void TrySetBool(string parameterName,bool value)
        {
            if (!string.IsNullOrEmpty(parameterName))
            {
                if (logChanges||animatorParameters.Contains(parameterName))
                    animator.SetBool(parameterName, value);
            }
        }

        void HandleBeginDeath()
        {
            ClearAnimatorBools();
            if (deathStates.Length > 0) 
                TrySetBool(deathStates[Random.Range(0, deathStates.Length)].animationBool, true);

            deathEvent.Invoke();
            if (navMeshAgent && navMeshAgent.isOnNavMesh)
                navMeshAgent.destination = transform.position;
            enabled = false;
        }

        void HandleBeginAttack()
        {
            var attackState = Random.Range(0, attackingStates.Length);
            turnSpeed = 120f;
            ClearAnimatorBools();
            TrySetBool(attackingStates[attackState].animationBool,true);
            attackingEvent.Invoke();
        }

        void HandleBeginEvade()
        {
            SetMoveFast();
            movementEvent.Invoke();
        }

        void HandleBeginChase()
        {
            SetMoveFast();
            movementEvent.Invoke();
        }

        void SetMoveFast()
        {
            MovementState moveState = null;
            var maxSpeed = 0f;
            foreach (var state in movementStates)
            {
                var stateSpeed = state.moveSpeed;
                if (stateSpeed > maxSpeed)
                {
                    moveState = state;
                    maxSpeed = stateSpeed;
                }
            }

            UnityEngine.Assertions.Assert.IsNotNull(moveState, string.Format("{0}'s wander script does not have any movement states.", gameObject.name));
            turnSpeed = moveState.turnSpeed;
            moveSpeed = maxSpeed;
            ClearAnimatorBools();
            TrySetBool(moveState.animationBool,true);
        }

        void SetMoveSlow()
        {
            MovementState moveState = null;
            var minSpeed = float.MaxValue;
            foreach (var state in movementStates)
            {
                var stateSpeed = state.moveSpeed;
                if (stateSpeed < minSpeed)
                {
                    moveState = state;
                    minSpeed = stateSpeed;
                }
            }

            UnityEngine.Assertions.Assert.IsNotNull(moveState, string.Format("{0}'s wander script does not have any movement states.", gameObject.name));
            turnSpeed = moveState.turnSpeed;
            moveSpeed = minSpeed;
            ClearAnimatorBools();
            TrySetBool(moveState.animationBool, true);
        }
        void HandleBeginIdle()
        {
            primaryPrey = null;
            var targetWeight = Random.Range(0, totalIdleStateWeight);
            var curWeight = 0;
            foreach (var idleState in idleStates)
            {
                curWeight += idleState.stateWeight;
                if (targetWeight > curWeight)
                    continue;
                idleUpdateTime = Time.time + Random.Range(idleState.minStateTime, idleState.maxStateTime);
                ClearAnimatorBools();
                TrySetBool(idleState.animationBool,true);
                moveSpeed = 0f;
                break;
            }
            idleEvent.Invoke();
        }
        void HandleBeginWander()
        {
            primaryPrey = null;
            var rand = Random.insideUnitSphere * wanderZone;
            var targetPos = startPosition + rand;
            ValidatePosition(ref targetPos);

            wanderTarget = targetPos;
            SetMoveSlow();
        }

        void ValidatePosition(ref Vector3 targetPos)
        {
            if (navMeshAgent)
            {
                NavMeshHit hit;
                if (!NavMesh.SamplePosition(targetPos, out hit, Mathf.Infinity, 1 << NavMesh.GetAreaFromName("Walkable")))
                {
                    Debug.LogError("Unable to sample nav mesh. Please ensure there's a Nav Mesh layer with the name Walkable");
                    enabled = false;
                    return;
                }

                targetPos = hit.position;
            }
        }


        IEnumerator RandomStartingDelay()
        {
            yield return new WaitForSeconds(Random.Range(0f, 2f));
            started = true;
            StartCoroutine(ConstantTicking(Random.Range(.7f,1f)));
        }

        IEnumerator ConstantTicking(float delay)
        {
            while (true)
            {
                UpdateAI();
                yield return new WaitForSeconds(delay);
            }
            // ReSharper disable once IteratorNeverReturns
        }

        [ContextMenu("This will delete any states you have set, and replace them with the default ones, you can't undo!")]
        public void BasicWanderSetUp()
        {
            MovementState walking = new MovementState(), running = new MovementState();
            IdleState idle = new IdleState();
            AIState attacking = new AIState(), death = new AIState();

            walking.stateName = "Walking";
            walking.animationBool = "isWalking";
            running.stateName = "Running";
            running.animationBool = "isRunning";
            movementStates = new MovementState[2];
            movementStates[0] = walking;
            movementStates[1] = running;


            idle.stateName = "Idle";
            idle.animationBool = "isIdling";
            idleStates = new IdleState[1];
            idleStates[0] = idle;

            attacking.stateName = "Attacking";
            attacking.animationBool = "isAttacking";
            attackingStates = new AIState[1];
            attackingStates[0] = attacking;

            death.stateName = "Dead";
            death.animationBool = "isDead";
            deathStates = new AIState[1];
            deathStates[0] = death;
        }
    }
}