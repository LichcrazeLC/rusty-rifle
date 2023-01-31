using System.Collections;
using UnityEngine;


namespace Agent_Behaviour.states
{
    public class FightingState : AgentState
    {
        private bool _attackCycleStarted;

        public FightingState(Agent agent, AgentStateMachine stateMachine) : base(agent, stateMachine)
        {
        }

        public override void Enter()
        {
            base.Enter();
            agent.animator.SetBool(IsInCombat, true);
        }

        public override void Exit()
        {
            base.Exit();
            _attackCycleStarted = false;
            agent.animator.SetBool(IsInCombat, false);
        }

        public override void LogicUpdate()
        {
            base.LogicUpdate();
            // if (agent.currentEnemy == null || !agent.currentEnemy.GetComponent<IUnitActivated>().IsActive() || agent.currentEnemy.CurrentHealth <= 0)
            // {
            //     stateMachine.ChangeState(agent.Idle);
            //     return;
            // }

            // agent.RotateTowards(agent.currentEnemy.transform.position);
            // if (_attackCycleStarted == false)
            // {
            //     _attackCycleStarted = true;
            //     agent.StartCoroutine(AutoAttack());
            // }
        }

        // private IEnumerator AutoAttack()
        // {
            // ElfLogger.CombatLog("Starting Auto Attack COROUTINE");
            // while (agent.currentEnemy != null && agent.currentEnemy.CurrentHealth > 0)
            // {
            //     //TODO
            //     //CREATE LOGIC TO HANDLE ANIMATION EXIT AND RE-ENTER FOR HERO WHILE ATTACKING
            //     agent.animator.SetTrigger(Attack);
            //     if (Tag.ENEMY.Compare(agent.gameObject))
            //     {
            //         ElfLogger.CombatLog("CHOP", agent.tag);
            //         var clipInfo = agent.animator.GetCurrentAnimatorClipInfo(0);
            //         var animationLength = clipInfo[0].clip.length;
            //         var interval = agent.CurrentStats.AttackInterval;
            //         if (interval < 0.1)
            //             interval = 0.1f;
            //         yield return new WaitForSeconds(animationLength + interval);
            //     }
            //     else
            //     {
            //         yield return new WaitForSeconds(0);
            //     }
            // }
        // }
    }
}