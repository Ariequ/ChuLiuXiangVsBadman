using UnityEngine;

public class MathUtils
{
	public static Quaternion LookRotationXZ (Vector3 direction)
	{
		if (direction != Vector3.zero)
		{
			return Quaternion.LookRotation(new Vector3(direction.x, 0, direction.z));
		}
		else
		{
			return Quaternion.Euler(Vector3.zero);
		}
	}

    public static float XZDistance(Vector3 from, Vector3 to)
    {
        return Mathf.Sqrt((from.x - to.x) * (from.x - to.x) + (from.z - to.z) * (from.z - to.z));
    }

    public static Vector3 XZVector(Vector3 from)
    {
        Vector3 result = new Vector3(from.x, 0f, from.z);
        return result;
    }

    public static int ChooseOneFromTwo(int one, int two)
    {
        if (Random.Range(0, 1.0f)< 0.5f)
        {
            return one;
        }
        return two;
    }
}

