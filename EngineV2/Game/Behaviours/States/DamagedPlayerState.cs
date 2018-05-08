using Engine.Interfaces;
using Engine.Managers;
using Engine.Service_Locator;
using Engine.State_Machines;
using ProjectHastings.Entities;
using ProjectHastings.Entities.Player;

namespace ProjectHastings.Behaviours.States
{
    class DamagedPlayerState<T> : IState<T> where T : IEntity
    {
        public bool success
        { get;}

        IStateMachine<T> States;
        IAnimationMachine<T> Animations;
        Player Player;
        ISoundManager sound = Locator.Instance.getProvider<SoundManager>() as ISoundManager;


        public DamagedPlayerState(Player player, IStateMachine<T> states, IAnimationMachine<T> animations)
        {
            Player = player;
            States = states;
            Animations = animations;
        }

        public void Enter(T entity)
        {
            Player.mind.Timer = 0;
            Player.healthScript.TakeDamage(1);
            Animations.ChangeActiveAnimation("Walking_Left");
            //Play Audio for damage sound --------- sound.Playsnd("");
        }

        public void Update(T entity)
        {
            if (Player.mind.Timer >= 2000)
            {
                States.ChangeState("BaseState");
            }
        }

        public void Exit(T entity)
        {

        }
    }
}
