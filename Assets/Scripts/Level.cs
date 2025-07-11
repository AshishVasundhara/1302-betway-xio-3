public class Level
{
	public int[,] grid;

	public int end;

	public int start;

	public Level(int _start, int _end, int[,] _grid)
	{
		start = _start;
		end = _end;
		grid = _grid;
	}

	public int Count()
	{
		int num = 0;
		for (int i = 0; i < 8; i++)
		{
			for (int j = 0; j < 8; j++)
			{
				if (grid[i, j] != 0)
				{
					num++;
				}
			}
		}
		return num;
	}
}
