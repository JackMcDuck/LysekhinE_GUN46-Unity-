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
			////todo comment: зачем нужны эти проверки?
			/// чтобы проверить, что есть данные для реплея и если их нет, выключить компонент
			if (!TryGetComponent(out _save) || _save.Records.Count == 0)
			{
				Debug.LogError("Records incorrect value", this);
				//todo comment: Для чего выключается этот компонент?
				// чтобы не сработал Update
				enabled = false;
			}
		}

		private void Update()
		{
			var curr = _save.Records[_index];
			//todo comment: Что проверяет это условие (с какой целью)? 
			// проверяет, пора ли переходить к следующему элементу, ведь они расположены по времени
			if (Time.time > curr.Time)
			{
				_prev = curr;
				_index++;
				//todo comment: Для чего нужна эта проверка?
				// чтобы не выйти за границы списка и не словить исключение
				if (_index >= _save.Records.Count)
				{
					enabled = false;
					Debug.Log($"<b>{name}</b> finished", this);
				}
			}
			//todo comment: Для чего производятся эти вычисления (как в дальнейшем они применяются)?
			// для того, чтобы понять где находится точка текущего времени между предыдущим и текущим состояниями
			// условно если время реальное 2f, предыдущий 1f, а текущий 3f, то мы получим 0.5f, то есть мы находимся 
			// ровно на условном полпути
			var delta = (Time.time - _prev.Time) / (curr.Time - _prev.Time);
			//todo comment: Зачем нужна эта проверка?
			// если curr.Time == _prev.Time, то будет деление на 0 и на выходе будет NaN. Чтобы все дальше работало, присваем ноль
			if (float.IsNaN(delta)) delta = 0f;
			//todo comment: Опишите, что происходит в этой строчке так подробно, насколько это возможно
			// лерп это линейная интерполяция (вроде). Объясню как понимаю. Есть две точки, если между ними
			// провести линию, то метод вернет точку на этой линии. расположение зависит от третьего аргумента
			// если он 0, то вернет стартовую позицию, если 1 то конечную, ну и соответственно любое число с запятой
			// от 0 до 1 вернет точку, которая будет находиться относительно двух других так же, как это число
			// с запятой находится относительно 0 и 1. 
			transform.position = Vector3.Lerp(_prev.Position, curr.Position, delta);
		}
	}
}