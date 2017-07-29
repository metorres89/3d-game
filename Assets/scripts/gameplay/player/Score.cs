using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScoreData
{
	public int totalEnemies;
	public int killedEnemies;

	public int totalHostages;
	public int rescuedHostages;
	public int killedHostages;

	public ScoreData ()
	{
		totalEnemies = 0;
		killedEnemies = 0;

		totalHostages = 0;
		rescuedHostages = 0;
		killedHostages = 0;
	}

	public ScoreData(int tEnemies, int tHostages) {
		totalEnemies = tEnemies;
		totalHostages = tHostages;
	}

}