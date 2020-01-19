using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Game;
using Game.ForEditor;

namespace Game.Controllers
{
    public class ObjectManager : MonoBehaviour
    {
        [SerializeField]
        private Subject[] _objects = null;

        [SerializeField, ReadOnly]
        private int _curObjectId = 0;

        private int CurObjectId
        {
            get => _curObjectId;
            set
            {
                _curObjectId = (int)Mathf.PingPong(value, _objects.Length - 1f);
                SetObject(_curObjectId);
            }
        }

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

        private void CheckInput(SwipeDirection direction)
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

        private void Start()
        {
            SwipeController.OnSwipe += CheckInput;
            SetObject(CurObjectId);
        }
    }
}