using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class SceneHelper {
	public static void LoadScene( string s, bool additive = false, bool setActive = false) {
		if (s == null) {
			s = UnityEngine.SceneManagement.SceneManager.GetActiveScene ().name;
		}

		UnityEngine.SceneManagement.SceneManager.LoadScene (
			s, additive ? UnityEngine.SceneManagement.LoadSceneMode.Additive : 0);
		

		if (setActive) {
            // to mark it active we have to wait a frame for it to load.
			CallAfterDelay.Create( 0, () => {
				UnityEngine.SceneManagement.SceneManager.SetActiveScene(
					UnityEngine.SceneManagement.SceneManager.GetSceneByName( s));
			});
		}
	}

	public static AsyncOperation UnloadScene(string s) {
		return UnityEngine.SceneManagement.SceneManager.UnloadSceneAsync(s);
	}
}
