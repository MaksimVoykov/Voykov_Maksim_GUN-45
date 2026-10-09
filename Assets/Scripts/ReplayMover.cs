using System;
using UnityEngine;

namespace DefaultNamespace
{
	[RequireComponent(typeof(PositionSaver))]
	public class ReplayMover : MonoBehaviour
	{
		private PositionSaver _save;

		private int _index;
		private PositionSaver.Data _prev;
		private float _duration;

		private void Start()
		{
            //todo comment: зачем нужны эти проверки?
            //чтобы убедиться, что ReplayMover вообще может воспроизводить записанные позиции
            if (!TryGetComponent(out _save) || _save.Records.Count == 0)
			{
				Debug.LogError("Records incorrect value", this);
                //todo comment: Для чего выключается этот компонент?
                //отключаем, чтобы программа не пыталась обработать отсутствующие данные
                enabled = false;
			}
		}

		private void Update()
		{
			var curr = _save.Records[_index];
            //todo comment: Что проверяет это условие (с какой целью)? 
            //наступил ли момент времени, записанный в текущей точке
            if (Time.time > curr.Time)
			{
				_prev = curr;
				_index++;
                //todo comment: Для чего нужна эта проверка?
                //чтобы определить, закончились ли записанные точки
                if (_index >= _save.Records.Count)
				{
					enabled = false;
					Debug.Log($"<b>{name}</b> finished", this);
				}
			}
            //todo comment: Для чего производятся эти вычисления (как в дальнейшем они применяются)?
            //здесь вычисляется доля пройденного времени между предыдущей и текущей записанными точками
            var delta = (Time.time - _prev.Time) / (curr.Time - _prev.Time);
            //todo comment: Зачем нужна эта проверка?
            //проверяет не получилось ли Nan
            //знаменатель может стать нулём, если curr.Time и _prev.Time будут одинаковыми
            if (float.IsNaN(delta)) delta = 0f;
            //todo comment: Опишите, что происходит в этой строчке так подробно, насколько это возможно
            // transform.position — это мировая позиция текущего GameObject
            // _prev.Position — начальная позиция
            // curr.Position — конечная позиция
            // delta — насколько далеко нужно продвинуться от начальной к конечной
            // Vector3.Lerp вычисляет точку, расположенную между двумя позициями
            transform.position = Vector3.Lerp(_prev.Position, curr.Position, delta);
		}
	}
}