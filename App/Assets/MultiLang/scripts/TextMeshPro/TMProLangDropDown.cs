using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;

public class TMProLangDropDown : MonoBehaviour
{
	[SerializeField] string[] myLangs;
	TMP_Dropdown drp;
	int index;
	private UIScreenManager theUI;

    private void Start()
    {
		theUI = FindObjectOfType<UIScreenManager>();
    }
    void Awake ()
	{
		drp = this.GetComponent <TMP_Dropdown> ();
		int v = PlayerPrefs.GetInt ("_language_index", 0);
		drp.value = v;

		drp.onValueChanged.AddListener (delegate {
			index = drp.value;
			PlayerPrefs.SetInt ("_language_index", index);
			PlayerPrefs.SetString ("_language", myLangs [index]);
			Debug.Log ("language changed to " + myLangs [index]);
			//apply changes
			ApplyLanguageChanges ();
		});
	}

	void ApplyLanguageChanges ()
	{
		PlayerPrefs.SetFloat("Screen",1);
		SceneManager.LoadScene (0);
		
	}

	void OnDestroy ()
	{
		drp.onValueChanged.RemoveAllListeners ();
	}
}
