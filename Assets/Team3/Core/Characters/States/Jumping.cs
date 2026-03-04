using Team3.Characters;
using Team3.StateMachine;
using Team3.Movement;
using UnityEngine;

public class Jumping : State
{
    private float maxSpeed;
    public float MaxSpeed => maxSpeed;
    [SerializeField] private CharacterMovement character;
    [SerializeField] private PlayerStats stats;
    [SerializeField] private SOVFX jumpFX;

    public override void Enter()
    {
        var newVelocity = character.Body.linearVelocity;

        GeneralMovement.CalculateJumpTakeofVelocity(character.JumpTakeofSpeed, ref newVelocity.y);
        
        character.Body.linearVelocity = newVelocity;
    
        maxSpeed = character.SprintInput ? character.MaxSprintingSpeed : character.MaxWalkingSpeed;

        stats.PlayVFXAtLocation(jumpFX.ID,gameObject.transform.position);
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
        GeneralMovement.CalculateFallVelocity(delta, ref y, character.Gravity, character.TerminalVelocity);

        newVelocity.y = y;
        character.Body.linearVelocity = newVelocity;
    }

    public override void Update()
    {
        FirstPersonMovement.Look(character.LookInput, character.Body, character.Sensitivity, character.HeadTransform);
    }
}
