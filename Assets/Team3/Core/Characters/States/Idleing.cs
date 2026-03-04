using Team3.Characters;
using Team3.StateMachine;
using Team3.Movement;
using UnityEngine;

public class Idleing : State
{
    [SerializeField] private CharacterMovement character;

    public override void Enter()
    {
    }

    public override void Exit()
    {
    }

    public override void PhysicsUpdate(float delta)
    {
        var newVelocity = character.Body.linearVelocity;

        GeneralMovement.CalculateFallVelocity(delta, ref newVelocity.y, character.Gravity, character.TerminalVelocity);

        character.Body.linearVelocity = newVelocity;
    }

    public override void Update()
    {
        FirstPersonMovement.Look(character.LookInput, character.Body, character.Sensitivity, character.HeadTransform);
    }
}
