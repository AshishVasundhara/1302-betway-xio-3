internal class Intent
{
	public static int level_id = 1;

	public static World world = new WorldBeginner();

	public static Level GetLevel()
	{
		return world.Get(level_id);
	}
}
