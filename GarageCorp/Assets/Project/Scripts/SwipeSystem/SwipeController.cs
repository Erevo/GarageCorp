using UnityEngine;
using System.Collections;
using System.Collections.Generic;

namespace Game.Controllers
{
    /// <summary>
    /// Управление свайпами.
    /// </summary>
    public class SwipeController : MonoBehaviour
    {
        /// <summary>
        /// Состояние прикасновения.
        /// </summary>
        private bool _isDragging;

        /// <summary>
        /// Состояние платформы.
        /// </summary>
        private bool _isMobilePlatform;

        /// <summary>
        /// Начальная точка свайпа.
        /// </summary>
        private Vector2 _tapPoint;

        /// <summary>
        /// Расстояние свайпа.
        /// </summary>
        private Vector2 _swipeDelta;

        /// <summary>
        /// Минимально расстояние свайпа.
        /// </summary>
        [SerializeField]
        private float _minSwipeDelta = 0;

        /// <summary>
        /// Делегат свайпа.
        /// </summary>
        /// <param name="direction">Направление свайпа.</param>
        public delegate void OnSwipeInput(SwipeDirection direction);

        /// <summary>
        /// Событие свайпа.
        /// </summary>
        public static event OnSwipeInput OnSwipe;

        /// <summary>
        /// Установка режима работы.
        /// </summary>
        private void Awake()
        {
#if UNITY_EDITOR || UNITY_STANDALONE
            _isMobilePlatform = false;
#else
            _isMobilePlatform = true;
#endif
        }

        /// <summary>
        /// Метод обработки фрейма.
        /// </summary>
        private void Update()
        {
            if (!_isMobilePlatform)
            {
                if (Input.GetMouseButtonDown(0))
                {
                    _isDragging = true;
                    _tapPoint = Input.mousePosition;
                }
                else if (Input.GetMouseButtonUp(0))
                    ResetSwipe();
            }
            else if (Input.touchCount > 0)
            {
                switch (Input.touches[0].phase)
                {
                    case TouchPhase.Began:
                        _isDragging = true;
                        _tapPoint = Input.touches[0].position;
                        break;
                    case TouchPhase.Ended:
                        ResetSwipe();
                        break;
                    case TouchPhase.Canceled:
                        ResetSwipe();
                        break;
                }
            }

            CalculateSwipe();
        }

        /// <summary>
        /// Обработчик свайпа.
        /// </summary>
        private void CalculateSwipe()
        {
            _swipeDelta = Vector2.zero;

            if (_isDragging)
            {
                if (!_isMobilePlatform && Input.GetMouseButton(0))
                    _swipeDelta = (Vector2)Input.mousePosition - _tapPoint;
                else if (Input.touchCount > 0)
                    _swipeDelta = Input.touches[0].position - _tapPoint;
            }

            if (_swipeDelta.magnitude > _minSwipeDelta)
            {
                if (OnSwipe != null)
                {
                    if (Mathf.Abs(_swipeDelta.x) > Mathf.Abs(_swipeDelta.y))
                        OnSwipe(_swipeDelta.x < 0 ? SwipeDirection.Left : SwipeDirection.Right);
                    else
                        OnSwipe(_swipeDelta.y > 0 ? SwipeDirection.Up : SwipeDirection.Down);
                }

                ResetSwipe();
            }
        }

        /// <summary>
        /// Ресет значений.
        /// </summary>
        private void ResetSwipe()
        {
            _isDragging = false;
            _tapPoint = _swipeDelta = Vector2.zero;
        }
    }
}