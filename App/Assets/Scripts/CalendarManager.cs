using System;
using System.Globalization;
using UnityEngine;
using TMPro;

public class CalendarManager : MonoBehaviour
{
	FirebaseManager theFirebaseManager;
	#region Fields

	[SerializeField]
	private HeaderManager headerManager;

	[SerializeField]
	private BodyManager bodyManager;

	[SerializeField]
	private TailManager tailManager;

	private DateTime targetDateTime;
	private CultureInfo cultureInfo;

	int year, month;

	#endregion

	
	#region Public Methods

	public void OnGoToPreviousOrNextMonthButtonClicked(string param)
	{
		targetDateTime = targetDateTime.AddMonths(param == "Prev" ? -1 : 1);
		year = targetDateTime.Year;
		month = targetDateTime.Month;
		Refresh(targetDateTime.Year, targetDateTime.Month);
	}

	#endregion

	#region Private Methods

	private void Start()
	{
		theFirebaseManager = FindObjectOfType<FirebaseManager>();
		targetDateTime = DateTime.Now;
		cultureInfo = new CultureInfo("en-US");
		year = targetDateTime.Year;
		month = targetDateTime.Month;
		Refresh(targetDateTime.Year, targetDateTime.Month);
	}

	#endregion

	#region Event Handlers

	private void Refresh(int year, int month)
	{
		headerManager.SetTitle($"{year} {cultureInfo.DateTimeFormat.GetMonthName(month)}");
		bodyManager.Initialize(year, month, OnButtonClicked);

		//tailManager.SetLegend(year, month);

	}

	private void OnButtonClicked((string day, string legend) param )
	{
		//tailManager.SetLegend($"You have clicked day {param.day}." );
		
		int dayNum = int.Parse(param.day);
		GetDateData(year, month, dayNum);

	}
	private void GetDateData(int year, int month, int day)
    {
		string dating = year.ToString() + "_" + month.ToString() + "_" + day.ToString();
		StartCoroutine(theFirebaseManager.LoadCalendarData(dating));
	}
	#endregion
}
