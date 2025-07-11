using System.Collections.Generic;
using UnityEngine;

public class World
{
	public enum Enum
	{
		BEGINNER
	}

	public List<Level> list = new List<Level>();

	internal static World getFromEnum(Enum world)
	{
		if (world != 0)
		{
		}
		return new WorldBeginner();
	}

	public List<Level> Get()
	{
		if (list.Count == 0)
		{
			Fill();
		}
		return list;
	}

	public void SetBestPassed(int lvl)
	{
		if (lvl > bestPassed())
		{
			PlayerPrefs.SetInt("best_passed_" + Key(), lvl);
		}
	}

	public int bestPassed()
	{
		int num = (!(this is WorldBeginner)) ? PlayerPrefs.GetInt("best_passed_" + Key(), 0) : Mathf.Max(PlayerPrefs.GetInt("best_passed_" + Key(), 0), PlayerPrefs.GetInt("best_level_passed", 0));
		return (num <= Count()) ? num : Count();
	}

	internal bool NotYetUnlocked(int lvl)
	{
		return lvl > bestPassed() + 1;
	}

	internal bool AlreadyPassed(int lvl)
	{
		return lvl <= bestPassed();
	}

	public virtual string Key()
	{
		return string.Empty;
	}

	public Level Get(int _id)
	{
		if (_id - 1 >= Count())
		{
			return null;
		}
		return Get()[_id - 1];
	}

	public int Count()
	{
		return Get().Count;
	}

	public virtual void Fill()
	{
	}
}
