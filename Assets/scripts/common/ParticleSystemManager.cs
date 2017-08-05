using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class ParticleSystemManager
{
	public static int maxPoolSize = 10;
	private static Dictionary<string, ParticleSystem[]> particleSystemPool;

	public static void Init() {
		ReloadPSFromFolder ("particle_system");
	}

	public static void ReloadPSFromFolder(string folderName) {
		ParticleSystem[] psCollection = Resources.LoadAll<ParticleSystem> (folderName);

		if(psCollection.Length > 0)
			particleSystemPool = new Dictionary<string, ParticleSystem[]>();

		foreach (ParticleSystem ps in psCollection) {
			if (!particleSystemPool.ContainsKey (ps.name)) {

				ParticleSystem[] psArray = new ParticleSystem[maxPoolSize];

				for (int index = 0; index < maxPoolSize; index++) {
					psArray [index] = GameObject.Instantiate (ps, null);
				}

				particleSystemPool.Add (ps.name, psArray);
			} else {
				Debug.LogWarningFormat ("ParticleSystemManager - Init - Duplicated particle systems on folder: '{0}'", folderName);
			}
		}
	}

	public static ParticleSystem GetParticleInstance(string name, int index) {
		return particleSystemPool[name][index % maxPoolSize];
	}
}