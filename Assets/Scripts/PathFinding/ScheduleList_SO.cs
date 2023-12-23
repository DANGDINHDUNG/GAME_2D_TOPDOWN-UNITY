using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Schedule Lists/Schedules")]
public class ScheduleList_SO : ScriptableObject
{
    [SerializeField] private List<ScheduleList> scheduleList;

    public List<ScheduleList> ScheduleList => scheduleList;
}

[System.Serializable]
public struct ScheduleList
{
    public string ScheduleName;
    public int Hour;
    public int Minutes;
    public Days Day;
    public Weather Weather; 
    public Season Season;
    public Location Location;
    public Animation AnimationAtDestination;

    public ScheduleList(string scheduleName, int hour, int minutes, Days day, Weather weather, Season season, Location location, Animation animation)
    {
        ScheduleName = scheduleName;
        Hour = hour;
        Minutes = minutes;
        Day = day;
        Weather = weather;
        Season = season;
        Location = location;
        AnimationAtDestination = animation;
    }
}

[System.Serializable]
public struct Location
{
    public float X;
    public float Y;

    public Location(float x, float y)
    {
        X = x;
        Y = y;
    }
}

