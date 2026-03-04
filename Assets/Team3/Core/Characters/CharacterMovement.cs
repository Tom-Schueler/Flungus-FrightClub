using UnityEngine;
using static UnityEngine.InputSystem.InputAction;
using Team3.StateMachine;
using Team3.Movement;
using Team3.SavingLoading.SaveData;
using System;
using Team3.Tools;
using UnityEngine.InputSystem;
using WebSocketSharp;

namespace Team3.Characters
{
    public class CharacterMovement : MonoBehaviour
    {
        #region InspectorVariables

        [Header("---Detect Ground Stuff!---")]
        [Space]

        [SerializeField] private LayerMask groundLayer;
        [SerializeField] private Vector3 castOriginOffset;
        [SerializeField, Min(0)] private float castDistance;
        [SerializeField, Min(0)] private float castRadius;

        [Space]
        [Header("---Movement Stuff!---")]
        [Space]

        [SerializeField, Min(0)] private float acceleration;
        [SerializeField, Min(0)] private float deceleration;
        [SerializeField, Min(0)] private float airControll;
        [SerializeField, Min(0)] private float maxWalkingSpeed;
        [SerializeField, Min(0)] private float maxSprintingSpeed;
        [SerializeField, Min(0)] private float jumpTakeofSpeed;
        [SerializeField, Min(0)] private int coyoteTime;
        [Tooltip("The coyoteTime has to be set in milli seconds"), SerializeField, Min(0)] private float slopeThreshold;
        [SerializeField, Min(0)] private float maxSlopeAngle;
        [SerializeField, Min(0)] private float slideSpeed;
        [SerializeField, Min(0)] private float maxSlideTimer;
        [SerializeField, Range(0, 1)] private float minLookInput;

        [Space]
        [Header("---Physics Stuff!---")]
        [Space]

        [SerializeField] private Rigidbody body;
        [SerializeField, Min(0)] private float gravity;
        [SerializeField] private float grvitationMuliplierAfterJump;
        [SerializeField, Min(0)] private float terminalVelocity;

        [Space]
        [Header("---Firstperson Camera---")]
        [Space]
        [SerializeField] private Transform headTransform;

        [Space]
        [Header("--Input---")]
        [Space]
        
        [SerializeField] private PlayerInput playerInput;
        [SerializeField] private InputActionReference walkActionReference;
        [SerializeField] private InputActionReference sprintActionReference;
        [SerializeField] private InputActionReference reloadActionReference;
        [SerializeField] private InputActionReference fireActionReference;
        [SerializeField] private InputActionReference jumpActionReference;
        

        [Space]
        [Header("--States---")]
        [Space]

        [SerializeField] private Idleing idleState;
        [SerializeField] private Moving moveState;
        [SerializeField] private Jumping jumpState;
        [SerializeField] private Falling fallState;
        [SerializeField] private Sliding slideState;

        [Space]
        [Header("--misc---")]
        [Space]

        [SerializeField] private Animator animator;

        #endregion // InspectorVariables


        #region MemberProperties

        public Vector3 MoveInput { get; private set; }
        public Vector2 LookInput { get; private set; }
        public bool JumpInput { get; private set; }
        public bool SprintInput { get; private set; }

        private RaycastHit hitInfo;
        public RaycastHit HitInfo => hitInfo;
        public bool IsSliding { get; private set; }
        public bool IsOnFloor { get; private set; }

        private float slideTimer;

        private float initalWalkingSpeed;
        private float initalSprintingSpeed;

        // States and fsm

        public FiniteStateMachine FSM { get; private set; }
        public Jumping JumpState
        {
            get { return jumpState; }
            private set { jumpState = value; }
        }

        public float Acceleration => acceleration;
        public float Deceleration => deceleration;
        public float AirControll => airControll;
        public float MaxWalkingSpeed => maxWalkingSpeed;
        public float MaxSprintingSpeed => maxSprintingSpeed;
        public float JumpTakeofSpeed => jumpTakeofSpeed;
        public int CoyoteTime => coyoteTime;
        public float GrvitationMuliplierAfterJump => grvitationMuliplierAfterJump;
        public float SlopeThreshold => slopeThreshold;
        public float MaxSlopeAngle => maxSlopeAngle;
        public float SlideSpeed => slideSpeed;
        public float MinLookInput => minLookInput;
        public float Gravity => gravity;
        public float TerminalVelocity => terminalVelocity;
        public Rigidbody Body => body;
        public Transform HeadTransform => headTransform;
        public float Sensitivity => ((float) SettingsData.Singleton.sensitivity) * 0.1f;

        #endregion // MemberProperties

        #region Inputs

        public void OnMove(CallbackContext callbackcontext)
        {
            Vector2 tmpVector = callbackcontext.ReadValue<Vector2>();
            MoveInput = new Vector3(tmpVector.x, 0, tmpVector.y);
        }

        public void OnJump(CallbackContext callbackContext) => JumpInput = callbackContext.ReadValue<float>() == 1;
        public void OnSprint(CallbackContext callbackContext) => SprintInput = callbackContext.ReadValue<float>() == 1;
        public void OnLook(CallbackContext callbackContext) => LookInput = callbackContext.ReadValue<Vector2>();

        #endregion // Inputs

        private void OnEnable()
        {
            SettingsChangedEmitter.OnSettingsChanged += UpdateInputSettings;
        }

        private void OnDisable()
        {
            SettingsChangedEmitter.OnSettingsChanged -= UpdateInputSettings;
        }

        private void Start()
        {
            if (slopeThreshold >= maxSlopeAngle)
            {
                throw new ArgumentException($"The {nameof(slopeThreshold)} cannot be greater or equal to {nameof(maxSlopeAngle)}");
            }

            UpdateInputSettings();

            FSM = new FiniteStateMachine(idleState);

            initalWalkingSpeed = MaxWalkingSpeed;
            initalSprintingSpeed = maxSprintingSpeed;
        }

        private void UpdateInputSettings()
        {
            InputAction walkAction = playerInput.actions.FindAction(walkActionReference.action.id);

            for (int i = 0; i < walkAction.bindings.Count; i++)
            {
                var binding = walkAction.bindings[i];

                string newPath = null;
                switch (binding.name)
                {
                    case "up": newPath = SettingsData.Singleton.GetKMAction(SavingLoading.KeyBoardMousePlayerAction.Walk).path; break;
                    case "left": newPath = SettingsData.Singleton.GetKMAction(SavingLoading.KeyBoardMousePlayerAction.StriveLeft).path; break;
                    case "down": newPath = SettingsData.Singleton.GetKMAction(SavingLoading.KeyBoardMousePlayerAction.WalkBack).path; break;
                    case "right": newPath = SettingsData.Singleton.GetKMAction(SavingLoading.KeyBoardMousePlayerAction.StriveRight).path; break;
                }

                if (newPath.IsNullOrEmpty())
                {
                    Debug.Log("Couldnt set new binding");
                    continue;
                }

                walkAction.ApplyBindingOverride(i, newPath);
            }

            InputAction sprintAction = playerInput.actions.FindAction(sprintActionReference.action.id);

            sprintAction.ApplyBindingOverride(0, SettingsData.Singleton.GetKMAction(SavingLoading.KeyBoardMousePlayerAction.Sprint).path);
            sprintAction.ApplyBindingOverride(1, SettingsData.Singleton.GetGPAction(SavingLoading.GamePadPlayerAction.Sprint).path);

            InputAction reloadAction = playerInput.actions.FindAction(reloadActionReference.action.id);

            reloadAction.ApplyBindingOverride(0, SettingsData.Singleton.GetKMAction(SavingLoading.KeyBoardMousePlayerAction.Reload).path);
            reloadAction.ApplyBindingOverride(1, SettingsData.Singleton.GetGPAction(SavingLoading.GamePadPlayerAction.Reload).path);

            InputAction fireAction = playerInput.actions.FindAction(fireActionReference.action.id);

            fireAction.ApplyBindingOverride(0, SettingsData.Singleton.GetKMAction(SavingLoading.KeyBoardMousePlayerAction.Shoot).path);
            fireAction.ApplyBindingOverride(1, SettingsData.Singleton.GetGPAction(SavingLoading.GamePadPlayerAction.Shoot).path);

            InputAction jumpAction = playerInput.actions.FindAction(jumpActionReference.action.id);
            
            jumpAction.ApplyBindingOverride(0, SettingsData.Singleton.GetKMAction(SavingLoading.KeyBoardMousePlayerAction.Jump).path);
            jumpAction.ApplyBindingOverride(1, SettingsData.Singleton.GetGPAction(SavingLoading.GamePadPlayerAction.Jump).path);
        }

        private void Update()
        {
            animator.SetFloat("Speed", body.linearVelocity.magnitude / MaxSprintingSpeed);
            
            FSM.CurrentState.Update();
        }

        private void FixedUpdate()
        {
            float delta = Time.fixedDeltaTime;
            IsOnFloor = GeneralMovement.CheckIfOnFloor(castRadius, body.position + castOriginOffset, castDistance, groundLayer, out hitInfo);

            FSM.CurrentState.PhysicsUpdate(delta);

            MaybeChangeState(delta);
        }

        private void MaybeChangeState(float delta)
        {
            if (FSM.CurrentState == JumpState)
            {
                if (body.linearVelocity.y <= 0)
                {
                    FSM.ChangeState(fallState);
                }
                else if (IsOnFloor)
                {
                    FSM.ChangeState(moveState);
                }

                return;
            }

            if (FSM.CurrentState == slideState)
            {
                slideTimer += delta;
                if (GeneralMovement.GetSlopeAngle(HitInfo, Vector3.up) >= slopeThreshold && maxSlideTimer > slideTimer)
                {
                    return;
                }

                slideTimer = 0;
            }

            State newState = null;

            if (!IsOnFloor)
            {
                newState = fallState;
            }
            else if (GeneralMovement.GetSlopeAngle(HitInfo, Vector3.up) > MaxSlopeAngle)
            {
                // newState = slideState;
            }
            else if (JumpInput)
            {
                newState = JumpState;
            }
            else if (!MoveInput.Equals(Vector3.zero) || body.linearVelocity.sqrMagnitude > 0)
            {
                newState = moveState;
            }
            else if (MoveInput.Equals(Vector3.zero) && body.linearVelocity.sqrMagnitude < 0.1f * 0.1f)
            {
                newState = idleState;
            }

            if (newState == null || newState == FSM.CurrentState)
            { return; }
            
            FSM.ChangeState(newState);
        }

        public void IncreaseMovementSpeed(float speedModifier)
        {
            maxSprintingSpeed = initalSprintingSpeed * speedModifier;
            maxWalkingSpeed = initalWalkingSpeed * speedModifier;
        }


        #region EditorFunctions

        void OnDrawGizmosSelected()
        {
            Spherecast(IsOnFloor, castRadius, transform.position + castOriginOffset, castDistance);
        }
        
        private void Spherecast(bool isOnFloor, float sphereRadius, Vector3 origin, float maxDistance)
        {
            Gizmos.color = isOnFloor ? Color.green : Color.red;

            Vector3 maxPosition = origin + (Vector3.down * maxDistance);

            Gizmos.DrawLine(origin, maxPosition);
            Gizmos.DrawWireSphere(origin, sphereRadius);
            Gizmos.DrawWireSphere(maxPosition, sphereRadius);
        }

        #endregion // Editor Functions
    }

}