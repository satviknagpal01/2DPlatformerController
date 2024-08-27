using NaughtyAttributes;
using UnityEngine;

public class PlayerAnimationAndEffects : MonoBehaviour
{
    [SerializeField] SpriteRenderer _sprite;
    [SerializeField] Transform _camTarget;
    [SerializeField] Animator _animator;
    [SerializeField] CameraShakeStats dashShake;

    [AnimatorParam("_animator")]
    public int animatorSpeedkey;
    [AnimatorParam("_animator")]
    public int jumpkey;
    [AnimatorParam("_animator")]
    public int groundedkey;

    public float _cameraTargetSwitchTime = 1f;
    public float _cameraTargetSwitchSpeed = 10f;

    float _goingBackTime;
    Vector3 _camTargetPos;
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
        _camTargetPos = _camTarget.localPosition;
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
        if (_player.PlayerVelocity.x != 0)
        {
            _sprite.flipX = _player.PlayerVelocity.x < 0;
            HandleCameraTarget();            
        }
    }
    void HandleCameraTarget()
    {
        _goingBackTime += _player.PlayerVelocity.x * Time.deltaTime;
        _goingBackTime = Mathf.Clamp(_goingBackTime, -_cameraTargetSwitchTime, _cameraTargetSwitchTime);
        if (Mathf.Abs(_goingBackTime) >= _cameraTargetSwitchTime)
        {
            Vector3 targetPos = Vector3.MoveTowards(_camTarget.localPosition, _camTargetPos * _player.PlayerVelocity.x, _cameraTargetSwitchSpeed * Time.deltaTime);
            _camTarget.localPosition = targetPos;
        }
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
        CameraShake.instance.ShakeDirectional(_player.PlayerVelocity, dashShake);
    }
}