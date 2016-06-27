using UnityEngine;
using System.Collections;

public class Locomotion
{
    private Animator m_Animator = null;
    
    private int m_SpeedId = 0;
    private int m_AgularSpeedId = 0;
    private int m_DirectionId = 0;

    public float m_SpeedDampTime = 0.1f;
    public float m_AnguarSpeedDampTime = 0.25f;
    public float m_DirectionResponseTime = 0.2f;
    
    public Locomotion(Animator animator)
    {
        m_Animator = animator;

        m_SpeedId = Animator.StringToHash("Speed");
        m_AgularSpeedId = Animator.StringToHash("Angular");
        m_DirectionId = Animator.StringToHash("Direction");
    }

    public void Do(float speed, float direction)
    {
        AnimatorStateInfo state = m_Animator.GetCurrentAnimatorStateInfo(0);

        bool inTransition = m_Animator.IsInTransition(0);
        bool inIdle = state.IsName("Base Layer.Idle");
        bool inTurn = state.IsName("Base Layer.TurnOnSpot");
        bool inLocomotion = state.IsName("Base Layer.Locomotion");

        float speedDampTime = inIdle ? 0 : m_SpeedDampTime;
        float angularSpeedDampTime = inLocomotion || inTransition ? m_AnguarSpeedDampTime : 0;
        float directionDampTime = inTurn || inTransition ? 1000000 : 0;

        float angular = direction / m_DirectionResponseTime;
        
        m_Animator.SetFloat(m_SpeedId, speed, speedDampTime, Time.deltaTime);
        m_Animator.SetFloat(m_AgularSpeedId, angular, angularSpeedDampTime, Time.deltaTime);
        m_Animator.SetFloat(m_DirectionId, direction, directionDampTime, Time.deltaTime);
    }	
}
