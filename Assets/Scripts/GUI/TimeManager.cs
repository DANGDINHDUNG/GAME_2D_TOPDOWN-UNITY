using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.SceneManagement;

public class TimeManager : MonoBehaviour
{
    [Header("Date & Time Settings")]
    [Range(1, 28)]
    public int dateInMonth;
    [Range(1, 4)]
    public int season;
    [Range(1, 99)]
    public int year;
    [Range(0, 24)]
    public int hour;
    [Range(0, 6)]
    public int minute;
    public DateTime DateTime;

    public static TimeManager timeInstance;

    [Header("Tick Settings")]
    public int TickMinutesIncrease = 10;
    public float TimeBetweenTicks = 1;
    private float currentTimeBetweenTicks = 0;

    public static UnityAction<DateTime> OnDateTimeChanged;
    public static UnityAction<DateTime> OnLoadedScene;

    private void Awake()
    {
        timeInstance = this;
        DateTime = new DateTime(dateInMonth, season - 1, year, hour, minute * 10);
        SceneManager.sceneLoaded += OnLoadScene;
    }

    private void OnLoadScene(Scene arg0, LoadSceneMode arg1)
    {
        OnLoadedScene?.Invoke(DateTime);
    }

    private void Start()
    {
        OnDateTimeChanged?.Invoke(DateTime);
    }

    private void Update()
    {
        currentTimeBetweenTicks += Time.deltaTime;

        if (currentTimeBetweenTicks >= TimeBetweenTicks )
        {
            currentTimeBetweenTicks = 0;
            Tick();
        }
    }

    void Tick()
    {
        AdvanceTime();
    }

    void AdvanceTime()
    {
        DateTime.AdvanceMinutes(TickMinutesIncrease);

        OnDateTimeChanged?.Invoke(DateTime);
    }
}

[System.Serializable]
public struct DateTime
{
    #region Fields
    private Days day;
    private int date;
    private int year;

    private int hour;
    private int minute;

    private Season season;

    private int totalNumDays;
    private int totalNumWeeks;
    #endregion

    #region Properties
    public Days Day => day;
    public int Date => date;

    public int Hour => hour;
    public int Minute => minute;
    public Season Season => season;
    public int Year => year;
    public int TotalNumDays => totalNumDays;
    public int TotalNumWeeks => totalNumWeeks;
    public int CurrentWeek => totalNumWeeks % 16 == 0 ? 16 : totalNumWeeks % 16;
    #endregion

    #region Constructor
    public DateTime(int date, int season, int year, int hour, int minutes)
    {
        this.day = (Days)(date % 7);
        if (day == 0) day = (Days)7;
        this.date = date + 1;
        this.season = (Season)season + 1;
        this.year = year + 1;

        this.hour = hour;
        this.minute = minutes;

        totalNumDays = date + (28 * (int)this.season) + (112 * (year - 1));

        totalNumWeeks = 1 + totalNumDays / 7;
    }

    #endregion

    #region Time Advancement
    public void AdvanceMinutes(int SecondsToAdvanceBy)
    {
        if (minute + SecondsToAdvanceBy >= 60)
        {
            minute = (minute + SecondsToAdvanceBy) % 60;
            AdvanceHour();
        }
        else
        {
            minute += SecondsToAdvanceBy;
        }
    }

    private void AdvanceHour()
    {
        if ((hour + 1) == 24)
        {
            hour = 0;
            AdvanceDay();
        }
        else
        {
            hour++;
        }
    }

    private void AdvanceDay()
    {
        if (day + 1 > (Days)7)
        {
            day = (Days)1;
            totalNumWeeks++;
        }
        else
        {
            day++;
        }

        date++;

        if (date % 29 == 0)
        {
            AdvanceSeason();
            date = 1;
        }
    }

    private void AdvanceSeason()
    {
        if (season == Season.Winter)
        {
            season = Season.Spring;
            AdvanceYear();
        }
        else season++;
    }

    private void AdvanceYear()
    {
        date = 1;
        year++;
    }
    #endregion

    #region Bool Checks
    public bool IsNight()
    {
        return hour > 18 || hour < 6;
    }

    public bool IsMorning()
    {
        return hour >= 6 || hour <= 12;
    }

    public bool IsAfternoon()
    {
        return hour > 12 || hour < 18;
    }

    public bool IsWeekend()
    {
        return day > Days.Fri ? true : false;
    }

    public bool IsParticularDay(Days _day)
    {
        return day == _day;
    }
    #endregion

    #region Key Dates
    public DateTime NewYearDay(int year)
    {
        if (year == 0) year = 1;
        return new DateTime(1, 0, year, 6, 0);
    }

    public DateTime SunmmerSolstice(int year)
    {
        if (year == 0) year = 1;
        return new DateTime(28, 1, year, 6, 0);
    }

    public DateTime PumpkinHarvest(int year)
    {
        if (year == 0) year = 1;
        return new DateTime(28, 2, year, 6, 0);
    }
    #endregion

    #region Start Of Season
    //public DateTime StartOfSeason(int season, int year)
    //{

    //}

    //public DateTime StartOfSpring(int year)
    //{

    //}

    //public DateTime StartOfSummer(int year)
    //{

    //}

    //public DateTime StartOfWinter(int year)
    //{
    //    return StartOfSeason(3, year);
    //}

    //public DateTime StartOfAutumn(int year)
    //{
    //    return StartOfSeason(2, year);
    //}


    #endregion

    #region To Strings
    public override string ToString()
    {
        return $"Date: {DateToString()} Season: {season} Time: {TimeToString()}" + 
            $"\nTotal Days: {totalNumDays} | Total Weeks: {totalNumWeeks}";
    }

    public string DateToString()
    {
        return $"{Day} {Date} {Year.ToString("D2")}";
    }

    public string TimeToString()
    {
        int adjustedHour = 0;
        if (hour == 0)
        {
            adjustedHour = 12;
        }
        else if (hour == 24)
        {
            adjustedHour = 12;
        }
        else if (hour >= 13)
        {
            adjustedHour = hour - 12;
        }
        else
        {
            adjustedHour = hour;
        }

        string AmPm = hour == 0 || hour < 12 ? "AM" : "PM";

        return $"{adjustedHour.ToString("D2")}:{minute.ToString("D2")} {AmPm}";
    }
    #endregion

}

[System.Serializable] 
public enum Days
{
    NULL = 0, 
    Mon = 1,
    Tue = 2,
    Wed = 3,
    Thu = 4,
    Fri = 5,
    Sat = 6,
    Sun = 7
}

[System.Serializable] 
public enum Season
{
    Spring = 0,
    Summer = 1, 
    Autumn = 2, 
    Winter = 3
}



