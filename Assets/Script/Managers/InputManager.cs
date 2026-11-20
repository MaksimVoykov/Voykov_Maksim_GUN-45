using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using Zenject;

public class InputManager : MonoBehaviour
{
    [SerializeField]
    private Image _restartFill;

    [SerializeField]
    private float _restartTime = 2f;

    private Controls _controls;
    private float _restartTimer;
    private bool _isHolding;

    [Inject]
    public void Construct(Controls controls)
    {
        _controls = controls;
    }

    private void OnEnable()
    {
        _controls.Game.Restart.started += OnRestartStarted;
        _controls.Game.Restart.canceled += OnRestartCanceled;
        _controls.Game.Restart.performed += OnRestartPerformed;

        _controls.Enable();
    }

    private void OnDisable()
    {
        _controls.Game.Restart.started -= OnRestartStarted;
        _controls.Game.Restart.canceled -= OnRestartCanceled;
        _controls.Game.Restart.performed -= OnRestartPerformed;

        _controls.Disable();
    }

    private void Update()
    {
        if (!_isHolding)
            return;

        _restartTimer += Time.deltaTime;
        _restartFill.fillAmount = _restartTimer / _restartTime;
    }

    private void OnRestartStarted(InputAction.CallbackContext context)
    {
        _isHolding = true;
        _restartTimer = 0f;
        _restartFill.fillAmount = 0f;
    }

    private void OnRestartCanceled(InputAction.CallbackContext context)
    {
        _isHolding = false;
        _restartTimer = 0f;
        _restartFill.fillAmount = 0f;
    }

    private void OnRestartPerformed(InputAction.CallbackContext context)
    {
        _isHolding = false;
        _restartFill.fillAmount = 1f;

        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }
}