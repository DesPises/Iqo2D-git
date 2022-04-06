using UnityEngine;

// Class helping to use Sniper sound methods from animation keys

public class SniperReceiver : MonoBehaviour
{
    public void SvdReloadSound()
    {
        Sniper.Instance.SvdReloadSound();
    }

    public void RunSound()
    {
        Sniper.Instance.RunSound();
    }
}
