using NaughtyAttributes;
using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

public class PlayerAnimationAndEffects : MonoBehaviour
{
    [SerializeField] SpriteRenderer _sprite;
    [SerializeField] Animator _animator;

    [AnimatorParam("_animator")]
    public int animatorSpeedkey;
    [AnimatorParam("_animator")]
    public int jumpkey;
    [AnimatorParam("_animator")]
    public int groundedkey;

    private IPlayerInterface _player;

    private void Awake()
    {
        if (_sprite == null)
        {
            if(!TryGetComponent<SpriteRenderer>(out _sprite))
            {
                Debug.LogError("Sprite renderer not found");
            }
        }
        if(_animator == null)
        {
            if(!TryGetComponent<Animator>(out _animator))
            {
                Debug.LogError("Animator not found");
            }
        }
        _player = GetComponentInParent<IPlayerInterface>();
        if (_player == null)
        {
            Debug.LogError("Player script not found in parent");
        }
    }

    private void OnEnable()
    {
        if(_player != null)
        {
            _player.Jumped += Jumped;
            _player.Grounded += GroundedChanged;
            _player.Dashed += Dashed;
        }
    }
    private void OnDisable()
    {
        if (_player != null)
        {
            _player.Jumped -= Jumped;
            _player.Grounded -= GroundedChanged;
            _player.Dashed -= Dashed;
        }
    }
    private void Update()
    {
        if(_player == null) return;

        HandleSpriteFlip();
        HandleMoveAnimation();
    }
    void HandleMoveAnimation()
    {
        float absVelocity = Mathf.Abs(_player.PlayerVelocity.x);
        _animator.SetFloat(animatorSpeedkey, Mathf.Lerp(0, 1, absVelocity));
    }
    void HandleSpriteFlip()
    {
        if (_player.PlayerVelocity.x != 0) _sprite.flipX = _player.PlayerVelocity.x < 0;
    }
    void Jumped()
    {
        _animator.SetTrigger(jumpkey);
        _animator.ResetTrigger(groundedkey);
    }
    void GroundedChanged(bool grounded)
    {
        if(grounded)
            _animator.SetTrigger(groundedkey);
    }
    void Dashed()
    {

    }
}