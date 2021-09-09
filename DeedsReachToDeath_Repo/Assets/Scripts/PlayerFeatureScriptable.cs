using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[CreateAssetMenu(fileName ="PlayerFeatures",menuName ="SSG/PlayerFeature")]
public class PlayerFeatureScriptable : ScriptableObject
{
    public float forwardSpeed;
    public float leftRightSpeed;
    public float touchSensitivity;
}
