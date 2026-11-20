using System;
using System.Collections;
using UnityEngine;
using UnityEngine.EventSystems;
using Netologia.Necro;

namespace Netologia.Necro.Units
{
    public class Unit : MonoBehaviour, IPointerEnterHandler, IPointerClickHandler, IPointerExitHandler
    {
        [SerializeField] 
        private float _moveSpeed = 5f;

        public Cell Cell { get; private set; }

        public event Action OnMoveEndCallback;

        private Coroutine _moveCoroutine;

        public void OnPointerEnter(PointerEventData eventData)
        {
            if (Cell != null)
                Cell.OnPointerEnter(eventData);
        }

        public void OnPointerClick(PointerEventData eventData)
        {
            if (Cell != null)
                Cell.OnPointerClick(eventData);
        }

        public void OnPointerExit(PointerEventData eventData)
        {
            if (Cell != null)
                Cell.OnPointerExit(eventData);
        }

        public void Move(Cell cell)
        {
            if (cell == null)
                return;

            if (_moveCoroutine != null)
                StopCoroutine(_moveCoroutine);

            _moveCoroutine = StartCoroutine(MoveCoroutine(cell));
        }

        private IEnumerator MoveCoroutine(Cell targetCell)
        {
            Vector3 targetPosition = targetCell.transform.position;
                                                                    

            while (Vector3.Distance(transform.position, targetPosition) > 0.01f)
            {
                transform.position = Vector3.MoveTowards(transform.position, targetPosition, _moveSpeed * Time.deltaTime);

                yield return null;
            }

            transform.position = targetPosition;

            if (Cell != null)
                Cell.Unit = null;

            Cell = targetCell;
            Cell.Unit = this;

            _moveCoroutine = null;

            OnMoveEndCallback?.Invoke();
        }

        public void SetCell(Cell cell)
        {
            if (Cell != null)
                Cell.Unit = null;

            Cell = cell;

            if (Cell != null)
                Cell.Unit = this;
        }

    }
}