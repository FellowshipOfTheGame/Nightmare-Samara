using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class EnemyStates : MonoBehaviour {
    protected StatesController enemy;
    protected Detection detection;

    protected virtual void Awake() {
        enemy = GetComponent<StatesController>();
        detection = GetComponent<Detection>();
    }

    public virtual void Enter() { this.enabled = true; }
    public virtual void Exit() { this.enabled = false; }
}
