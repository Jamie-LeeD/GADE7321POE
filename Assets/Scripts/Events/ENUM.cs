using UnityEngine;
public enum GameEventType
{
    SprintStart,
    SprintStop,
    Jump,
    StaminaChanged,
    DialogueStart,
    DialogueEnd,
    PlayerDied,
    RequestRespawn,
    RestartGame,
    LivesChanged,
    ScoreChanged,
    GameOver,
    PlaySfx
}
public enum EnemyType
{
    Patrolling,
    Stationary,
    BossPatrolling
}