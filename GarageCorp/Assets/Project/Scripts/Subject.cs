using UnityEngine;
using Game.ForEditor;

namespace Game
{
    /// <summary>
    /// Класс, представляющий собой игровой объект.
    /// </summary>
    public class Subject : MonoBehaviour
    {
        #region Поля

        /// <summary>
        /// Id объекта.
        /// </summary>
        [SerializeField]
        private byte _id = 0;

        /// <summary>
        /// Скорость перемещения.
        /// </summary>
        [SerializeField]
        private float _speed = 1f;

        /// <summary>
        /// Позиция, в которую скрывается объект.
        /// </summary>
        [SerializeField, ReadOnly]
        private Transform _hidePosition = null;

        /// <summary>
        /// Позиция, в которой объект виден.
        /// </summary>
        [SerializeField, ReadOnly]
        private Transform _viewPosition = null;

        /// <summary>
        /// Метка видимости объекта.
        /// </summary>
        [SerializeField, ReadOnly]
        private bool _isVisible = false;

        /// <summary>
        /// Целевая позиция.
        /// </summary>
        [SerializeField, ReadOnly]
        private Vector3 _targetPosition = Vector3.zero;

        #endregion

        #region Свойства

        /// <summary>
        /// Id объекта.
        /// </summary>
        public byte Id
        {
            get => _id;
            set => _id = value;
        }

        /// <summary>
        /// Метка видимости объекта.
        /// </summary>
        public bool IsVisible
        {
            get => _isVisible;
            set
            {
                _isVisible = value;
                if (IsVisible)
                    _targetPosition = ViewPosition;
                else
                {
                    _targetPosition = HidePosition;
                }
            }
        }

        /// <summary>
        /// Позиция, в которую скрывается объект.
        /// </summary>
        private Vector3 HidePosition
        {
            get => _hidePosition.position;
            set => _hidePosition.position = value;
        }

        /// <summary>
        /// Позиция, в которой объект виден.
        /// </summary>
        private Vector3 ViewPosition
        {
            get => _viewPosition.position;
            set => _viewPosition.position = value;
        }

        #endregion

        #region Методы жизненного цикла

        private void Awake()
        {
            _viewPosition = GameObject.FindGameObjectWithTag("ViewPosition").transform;
            _hidePosition = GameObject.FindGameObjectWithTag("HidePosition").transform;

            IsVisible = false;
        }

        private void Update()
        {
            if (transform.position != _targetPosition)
            {
                transform.position = Vector3.Lerp(transform.position, _targetPosition, _speed);
            }
        }

        #endregion
    }
}