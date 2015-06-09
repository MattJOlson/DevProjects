all: weather football

weather: weather.dll weather-main.cs
	mcs -t:exe -r:weather.dll weather-main.cs

football: football.dll football-main.cs
	mcs -t:exe -r:football.dll football-main.cs

weather.dll: weather.cs
	mcs -t:library weather.cs

weather-test.dll: weather.dll weather-test.cs
	mcs -t:library -pkg:mono-nunit -r:weather.dll weather-test.cs

football.dll: football.cs
	mcs -t:library football.cs

football-test.dll: football.dll football-test.cs
	mcs -t:library -pkg:mono-nunit -r:football.dll football-test.cs

test: weather-test football-test

weather-test: weather-test.dll
	nunit-console -nologo -noresult weather-test.dll

football-test: football-test.dll
	nunit-console -nologo -noresult football-test.dll
