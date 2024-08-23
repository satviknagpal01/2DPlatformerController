using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAnimationAndEffects : MonoBehaviour
{
    [SerializeField] SpriteRenderer _sprite;
    private IPlayerInterface _player;

    private void Start()
    {
        if (_sprite == null)
        {
            if(!TryGetComponent<SpriteRenderer>(out _sprite))
            {
                Debug.LogError("Sprite renderer not found");
            }
        }
    }

    private void OnEnable()
    {
        _player = GetComponentInParent<IPlayerInterface>();
        if (_player == null)
        {
            Debug.LogError("Player not found");
            return;
        }
        else
        {
            _player.Jumped += Jumped;
            _player.Grounded += GroundedChanged;
            _player.Dashed += Dashed;
        }
    }
    private void Update()
    {
        HandleSpriteFlip();
    }
    void HandleSpriteFlip()
    {
        if (_player.PlayerVelocity.x != 0) _sprite.flipX = _player.PlayerVelocity.x < 0;
    }
    void Jumped()
    {
        print("Jumped");
    }
    void GroundedChanged(bool grounded)
    {
        print("Grouned: " + grounded);
    }
    void Dashed()
    {
        print("Dashed");
    }
}