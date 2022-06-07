using UnityEngine ;
using UnityEngine.UI ;
using TMPro;
using UnityEngine.Audio; //to have access to audio


public class SettingsUI : MonoBehaviour {
   [Header ("UI References :")]
   [Space]
   [SerializeField] private Toggle uiVibrationToggle ;
   [SerializeField] private Slider uiMusicSlider ;
   [SerializeField] private Slider uiSoundsSlider ;
   [SerializeField] private TMP_InputField uiPlayerNameInputField ;
    public AudioMixer theMixer;


   private void Awake () {
   
   

      //Update UI with saved values in player prefs:--------------------------------------
      uiVibrationToggle.isOn = Settings.VibrationEnabled ;
      uiMusicSlider.value = Settings.MusicVolume ;
      uiSoundsSlider.value = Settings.SoundsVolume ;
      uiPlayerNameInputField.text = Settings.PlayerName ;

      // Add UI elements listeners :------------------------------------------------------
      uiVibrationToggle.onValueChanged.AddListener (OnVibrationToggleChange) ;
      uiMusicSlider.onValueChanged.AddListener (OnMusicSliderChange) ;
      uiSoundsSlider.onValueChanged.AddListener (OnSoundsSliderChange) ;
      uiPlayerNameInputField.onValueChanged.AddListener (OnPlayerNameInputFieldChange) ;
   }


   // UI Events: -------------------------------------------------------------------------
   private void OnVibrationToggleChange (bool value) {
      Settings.VibrationEnabled = value ;
   }

   private void OnMusicSliderChange (float value) {
      Settings.MusicVolume = value ;
   }

   private void OnSoundsSliderChange (float value) {
      Settings.SoundsVolume = value ;
   }

   private void OnLanguagesDropDownChange (int value) {
      Settings.SelectedLanguage = value ;
   }

   private void OnPlayerNameInputFieldChange (string value) {
      Settings.PlayerName = value ;
   }


   // -------------------------------------------------------------------------------------
   




   // -------------------------------------------------------------------------------------
   private void OnDestroy () {
      //Remove Listeners:

      // Remove UI elements listeners :
      uiVibrationToggle.onValueChanged.RemoveListener (OnVibrationToggleChange) ;
      uiMusicSlider.onValueChanged.RemoveListener (OnMusicSliderChange) ;
      uiSoundsSlider.onValueChanged.RemoveListener (OnSoundsSliderChange) ;
      uiPlayerNameInputField.onValueChanged.RemoveListener (OnPlayerNameInputFieldChange) ;
   }
}
