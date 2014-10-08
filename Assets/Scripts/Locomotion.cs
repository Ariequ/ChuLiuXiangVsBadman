using UnityEngine;
using System.Collections;

public class Locomotion
{
    private Animator m_Animator = null;
    
    private int m_SpeedId = 0;

    public float m_SpeedDampTime = 0.1f;
    public float m_AnguarSpeedDampTime = 0.25f;
    public float m_DirectionResponseTime = 0.2f;
    
    public Locomotion(Animator animator)
    {
        m_Animator = animator;

        m_SpeedId = Animator.StringToHash("Speed");     
    }

    public void Do(float speed, Vector3 direction)
    {
        AnimatorStateInfo state = m_Animator.GetCurrentAnimatorStateInfo(0);

        bool inIdle = state.IsName("Idle");
        float speedDampTime = inIdle ? 0 : m_SpeedDampTime;

        m_Animator.SetFloat(m_SpeedId, speed, speedDampTime, Time.deltaTime);	
    }	
}
