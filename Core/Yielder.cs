using System.Collections;
using System.Collections.Generic;

namespace Ax.Engine.Core
{
	public static class Yielder
	{
		internal static List<Coroutine> coroutines = new List<Coroutine>();

		public static Coroutine StartCoroutine(IEnumerator routine)
		{
			Coroutine coroutine = new Coroutine(routine);
			coroutine.routine.MoveNext();
			coroutines.Add(coroutine);

			return coroutine;
		}

		internal static void ProcessCoroutines()
		{
			for (int i = 0; i < coroutines.Count;)
			{
				Coroutine coroutine = coroutines[i];
				if (coroutine.MoveNext())
				{
					++i;
				}
				else if (coroutines.Count > 1)
				{
					coroutines[i] = coroutines[coroutines.Count - 1];
					coroutines.RemoveAt(coroutines.Count - 1);
				}
				else
				{
					coroutines.Clear();
					break;
				}
			}
		}
	}
}
