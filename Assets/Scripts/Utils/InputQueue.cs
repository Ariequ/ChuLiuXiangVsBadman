using UnityEngine;

using System;
using System.Text;

public class InputQueue
{
	public const int EMPTY_INPUT = -1;

	private int _maxLength;

	private int _firstIndex;

	private int _lastIndex;

	private int[] _queue;

	public InputQueue (int maxLength)
	{
		_maxLength = maxLength;
		_queue = new int[_maxLength];

		_firstIndex = _lastIndex = 0;
		
		for (int i = 0; i < _maxLength; ++i)
		{
			_queue[i] = EMPTY_INPUT;
		}
	}

	public void AddInput(int input)
	{
		if (_lastIndex < _maxLength)
		{
			_queue[_lastIndex++] = input;
//			Debug.Log(_lastIndex);
		}
	}

	public int GetInput()
	{
		if (_firstIndex == _maxLength || _queue[_firstIndex] == EMPTY_INPUT)
		{
			_firstIndex = _lastIndex = 0;
			return EMPTY_INPUT;
		}

		int input = _queue[_firstIndex];
		_queue[_firstIndex] = EMPTY_INPUT;
		_firstIndex++;
		return input;
	}

	public override string ToString ()
	{
		StringBuilder sb = new StringBuilder();

		for (int i = 0; i < _maxLength; ++i)
		{
			sb.Append(_queue[i] + ", ");
		}

		return sb.ToString();
	}
}

