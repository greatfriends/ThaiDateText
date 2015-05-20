@echo off
Packages\xunit.runner.console.2.0.0\tools\xunit.console ^
	GFDN.ThaiDateTextFacts\bin\Release\GreatFriends.ThaiDateTextFacts.dll ^
	-parallel all ^
	-html Result.html ^
	-nologo -quiet
@echo on 