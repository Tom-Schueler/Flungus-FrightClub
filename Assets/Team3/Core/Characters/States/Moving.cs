using Team3.Characters;
using Team3.StateMachine;
using Team3.Movement;
using UnityEngine;

public class Moving : State
{
    [SerializeField] private CharacterMovement character;
    [SerializeField] private PlayerStats playerStats;

    public override void Enter() { }

    public override void Exit() { }

    public override void PhysicsUpdate(float delta)
    {
        float movementSpeed = character.SprintInput ? character.MaxSprintingSpeed : character.MaxWalkingSpeed;
        var newVelocity = character.Body.linearVelocity;

        var currentAcceleration = character.Acceleration - playerStats.accelerationModifier;
        var currentDecelation = character.Deceleration - playerStats.decelerationModifier;

        FirstPersonMovement.CalculateMoveVelocity(delta, currentAcceleration, currentDecelation, character.Body, ref newVelocity, character.MoveInput, movementSpeed, character.IsOnFloor, character.HitInfo, character.SlopeThreshold, character.MaxSlopeAngle);
        GeneralMovement.CalculateFallVelocity(delta, ref newVelocity.y, character.Gravity, character.TerminalVelocity);

        character.Body.linearVelocity = newVelocity;
    }

    public override void Update()
    {
        FirstPersonMovement.Look(character.LookInput, character.Body, character.Sensitivity, character.HeadTransform);
    }
}
