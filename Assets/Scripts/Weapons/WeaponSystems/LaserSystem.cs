using UnityEngine;

public class LaserSystem : WeaponSystem
{
    // Editor Fields
    public float InaccuracyInDegrees;


    private void Awake() => base.OnAwake();
    private void FixedUpdate() => base.OnFixedUpdate();

    public override void PostInstantiation(GameObject laser)
    {
        float inaccuracyAdjustment = Random.Range(-InaccuracyInDegrees / 2f, InaccuracyInDegrees / 2f);

        var eulers = laser.transform.rotation.eulerAngles;
        laser.transform.rotation = Quaternion.Euler(eulers.x, eulers.y, eulers.z + inaccuracyAdjustment);

        AudioManager.Instance.PlayTrigger(AudioTrigger.Laser_Player);
    }
}
