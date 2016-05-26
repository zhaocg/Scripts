using UnityEngine;
using System.Collections;
using VRStandardAssets.Utils;
public class PlayerActions : MonoBehaviour {

    public Player player;

    [SerializeField] private VRInput m_vrInput;

    void OnEnable()
    {
        m_vrInput.OnSwipe += Cast;
    }

    void OnDisable()
    {
        m_vrInput.OnSwipe -= Cast;
    }

    void Cast(VRInput.SwipeDirection dir)
    {
        if(dir == VRInput.SwipeDirection.RIGHT)
        {
            CastRightSpells();
        }
        if(dir == VRInput.SwipeDirection.LEFT)
        {
            CastLeftSpells();
        }
    }

    /// <summary>
    /// 释放左手法术
    /// </summary>
    public void CastLeftSpells()
    {
        player.animations.HandAttack(0.5f);
    }


    /// <summary>
    /// 释放右手法术
    /// </summary>
    public void CastRightSpells()
    {
        player.animations.HandAttack(-0.5f);
    }
}
