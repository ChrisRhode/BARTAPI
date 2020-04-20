# BARTAPI
Currently implememts an ASP.NET C#.NET simple lookup of BART departure times for a route from a particular station.
Station and schedule data is pulled from BART dyamically using the BART API (https://www.bart.gov/schedules/developers/api)
The data is pulled in JSON format and deserialized into classes using NewtonSoft.JSON and JSONUtils.com
The JSON data from BART contains special characters e.g. @ that cause issues for NewtonSoft.JSON / embedding into C#.NET so
there is a thick client (Windows Forms) helper program to deal with cleaning up the JSON data (source is in PollWebServiceTest folder)

http://www.zoggoth2.com/BARTAPI/ScheduleForStation.aspx (source is in BARTAPI folder)


