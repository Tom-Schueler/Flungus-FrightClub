using System;
using Team3.Characters;
using Team3.StateMachine;
using UnityEngine;

public class Stunned : State
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
        throw new NotImplementedException($"This State: {nameof(Sliding)} is not yet implemented");
        // CharacterControlls.Gravity(delta, character.Body, character.IsOnFloor, ref character.VerticalVelocity, character.Gravity, character.TerminalVelocity);
    }

    public override void Update()
    {

    }
}
