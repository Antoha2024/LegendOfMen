using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SceneService : MonoBehaviour
{
    [field: SerializeField] public UnitView PlayerView { get; private set; }
	[field: SerializeField] public float PlayerMoveSpeed { get; private set; } = 10;
}
