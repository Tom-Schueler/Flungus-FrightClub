using System.Threading.Tasks;
using Team3.Characters;
using Team3.StateMachine;
using Team3.Movement;
using UnityEngine;

public class Falling : State
{
    private float maxSpeed, gravity;
    private bool canCoyoteJump;
    [SerializeField] private CharacterMovement character;

    public override void Enter()
    {
        State lastState = character.FSM.LastState;

        if (lastState == null)
        { return; }

        if (lastState.GetType() == typeof(Jumping))
        {
            maxSpeed = ((Jumping)lastState).MaxSpeed;
            gravity = character.Gravity * character.GrvitationMuliplierAfterJump;
            canCoyoteJump = false;
        }
        else
        {
            maxSpeed = character.SprintInput ? character.MaxSprintingSpeed : character.MaxWalkingSpeed;
            gravity = character.Gravity;
            canCoyoteJump = true;
            StartCoyoteTimer();
        }
    }

    public override void Exit()
    {
    }

    public override void PhysicsUpdate(float delta)
    {
        Vector3 newVelocity = character.Body.linearVelocity;
        float y = newVelocity.y;
        newVelocity.y = 0;

        FirstPersonMovement.CalculateMoveVelocity(delta, character.AirControll, 0, character.Body, ref newVelocity, character.MoveInput, maxSpeed, character.IsOnFloor, character.HitInfo, character.SlopeThreshold, character.MaxSlopeAngle);
        GeneralMovement.CalculateFallVelocity(delta, ref y, gravity, character.TerminalVelocity);

        newVelocity.y = y;
        character.Body.linearVelocity = newVelocity;

        if (canCoyoteJump && character.JumpInput)
        {
            character.FSM.ChangeState(character.JumpState);
        }
    }

    public override void Update()
    {
        FirstPersonMovement.Look(character.LookInput, character.Body, character.Sensitivity, character.HeadTransform);
    }

    private async void StartCoyoteTimer()
    {
        await Task.Delay(character.CoyoteTime);
        canCoyoteJump = false;   
    }
}
