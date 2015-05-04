@echo off
Packages\xunit.runner.console.2.0.0\tools\xunit.console GFDN.ThaiDateTextFacts\bin\debug\GFDN.ThaiDateTextFacts.dll -parallel all -html Result.html -nologo -quiet
@echo on 