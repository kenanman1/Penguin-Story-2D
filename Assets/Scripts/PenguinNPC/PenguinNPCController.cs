﻿using UnityEngine;

public class PenguinNPCController : MonoBehaviour
{
    [SerializeField] private float attackTimer = 4f;

    private BaseAnimation penguinAnimantion;
    private PenguinNPCAttack penguinNPCAttack;
    private NPCMovenment penguinNPCMovenment;

    private void Awake()
    {
        penguinAnimantion = GetComponent<BaseAnimation>();
        penguinNPCAttack = GetComponent<PenguinNPCAttack>();
        penguinNPCMovenment = GetComponent<NPCMovenment>();
    }

    public void Attack()
    {
        penguinNPCMovenment.StopMovement();
        penguinAnimantion.Attack(attackTimer);
    }

    public void NotAttack()
    {
        penguinNPCMovenment.ResumeMovement();
    }
}
