using UnityEngine;

public static class GameData {
	private static int _metals = 0;

	//Static Constructor to load data from playerPrefs
	static GameData ( ) {
		_metals = PlayerPrefs.GetInt ( "Metals", 0 );
	}

	public static int Metals {
		get{ return _metals; }
		set{ PlayerPrefs.SetInt ( "Metals", (_metals = value) ); }
	}

	
}
