using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using TMPro;

namespace SlimUI.ModernMenu
{
	public class UISettingsManager : MonoBehaviour
	{

		public enum Platform { Desktop, Mobile };
		public Platform platform;
		// toggle buttons
		[Header("VOLUME")]
		public GameObject mobileMusictext;

		[Header("GAME SETTINGS")]
		public GameObject showhudtext;
		public GameObject tooltipstext;
		public GameObject difficultynormaltext;
		public GameObject difficultynormaltextLINE;
		public GameObject difficultyhardcoretext;
		public GameObject difficultyhardcoretextLINE;


		public void Start()
		{

		}
		// the playerprefs variable that is checked to enable hud while in game
		public void ShowHUD()
		{
			if (PlayerPrefs.GetInt("ShowHUD") == 0)
			{
				PlayerPrefs.SetInt("ShowHUD", 1);
				showhudtext.GetComponent<TMP_Text>().text = "on";
			}
			else if (PlayerPrefs.GetInt("ShowHUD") == 1)
			{
				PlayerPrefs.SetInt("ShowHUD", 0);
				showhudtext.GetComponent<TMP_Text>().text = "off";
			}
		}


		public void MobileMusicMute()
		{
			if (PlayerPrefs.GetInt("Mobile_MuteMusic") == 0)
			{
				PlayerPrefs.SetInt("Mobile_MuteMusic", 1);
				mobileMusictext.GetComponent<TMP_Text>().text = "on";
			}
			else if (PlayerPrefs.GetInt("Mobile_MuteMusic") == 1)
			{
				PlayerPrefs.SetInt("Mobile_MuteMusic", 0);
				mobileMusictext.GetComponent<TMP_Text>().text = "off";
			}
		}

		// show tool tips like: 'How to Play' control pop ups
		public void ToolTips()
		{
			if (PlayerPrefs.GetInt("ToolTips") == 0)
			{
				PlayerPrefs.SetInt("ToolTips", 1);
				tooltipstext.GetComponent<TMP_Text>().text = "on";
			}
			else if (PlayerPrefs.GetInt("ToolTips") == 1)
			{
				PlayerPrefs.SetInt("ToolTips", 0);
				tooltipstext.GetComponent<TMP_Text>().text = "off";
			}
		}

		public void NormalDifficulty()
		{
			difficultyhardcoretextLINE.gameObject.SetActive(false);
			difficultynormaltextLINE.gameObject.SetActive(true);
			PlayerPrefs.SetInt("NormalDifficulty", 1);
			PlayerPrefs.SetInt("HardCoreDifficulty", 0);
		}

		public void HardcoreDifficulty()
		{
			difficultyhardcoretextLINE.gameObject.SetActive(true);
			difficultynormaltextLINE.gameObject.SetActive(false);
			PlayerPrefs.SetInt("NormalDifficulty", 0);
			PlayerPrefs.SetInt("HardCoreDifficulty", 1);
		}

	}
}