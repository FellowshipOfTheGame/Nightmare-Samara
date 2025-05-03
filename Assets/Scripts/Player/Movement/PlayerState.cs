using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/*Classe Abstrata na qual todas as classes dos estados se baseiam */

public abstract class PlayerState
{
    protected PlayerStateMachine stateMachine; //Maquina de estado
    protected GameObject player; //Game object do player

    //Construtor padrao para estados do player
    public PlayerState(PlayerStateMachine stateMachine, GameObject player)
    {
        this.stateMachine = stateMachine;
        this.player = player;
    }

    /*Todas as os metodos que vao ser utilizados pelas classes que herdam dessa */
    public virtual void Enter() { }
    public virtual void Exit() { }
    public virtual void Update() { }
    public virtual void FixedUpdate() { }
    public virtual float HandleInput() {
        //Retorna o 1 se o player vai pra direita, -1 se o player vai pra esquerda e se o player fica parado retorna 0
        return Input.GetAxisRaw("Horizontal");
    }
}
