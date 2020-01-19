using UnityEngine;
using Game.ForEditor;

namespace Game.Controllers
{
    /// <summary>
    /// Класс, управляющий появлением объектов.
    /// </summary>
    public class ObjectManager : MonoBehaviour
    {
        #region Поля

        /// <summary>
        /// Пул объектов.
        /// </summary>
        [SerializeField]
        private Subject[] _objects = null;

        /// <summary>
        /// Id выбранного объекта.
        /// </summary>
        [SerializeField, ReadOnly]
        private int _curObjectId = 0;

        #endregion

        #region Свойства

        /// <summary>
        /// Id выбранного объекта.
        /// </summary>
        private int CurObjectId
        {
            get => _curObjectId;
            set
            {
                _curObjectId = (int)Mathf.PingPong(value, _objects.Length - 1f);
                SetObject(_curObjectId);
            }
        }

        #endregion

        #region Методы

        /// <summary>
        /// Метод, устанавливающий видимость объекта.
        /// </summary>
        private void SetObject(int id)
        {
            foreach (var obj in _objects)
            {
                if (obj.Id == id)
                    obj.IsVisible = true;
                else
                {
                    obj.IsVisible = false;
                }
            }
        }

        /// <summary>
        /// Слушатель события OnSwipe.
        /// </summary>
        private void SwipeListener(SwipeDirection direction)
        {
            switch (direction)
            {
                case SwipeDirection.Left:
                    CurObjectId--;
                    Debug.Log("Влево");
                    break;
                case SwipeDirection.Right:
                    CurObjectId++;
                    Debug.Log("Вправо");
                    break;
            }
        }

        #endregion

        #region Методы жизненного цикла

        private void Start()
        {
            SwipeController.OnSwipe += SwipeListener;
            SetObject(CurObjectId);
        }

        private void OnDisable()
        {
            SwipeController.OnSwipe -= SwipeListener;
        }

        #endregion
    }
}