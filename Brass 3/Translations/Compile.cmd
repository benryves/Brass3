call "%ProgramFiles%\Microsoft SDKs\Windows\v6.0\Bin\SetEnv.Cmd" > nul
mkdir ..\bin\Debug\%1 > nul
mkdir ..\bin\Release\%1  > nul
resgen Brass3.Strings.%1.txt
al /t:lib /embed:Brass3.Strings.%1.resources /culture:%1 /out:Brass.resources.dll
copy Brass.resources.dll ..\bin\Debug\%1 > nul
copy Brass.resources.dll ..\bin\Release\%1 > nul
del Brass3.Strings.%1.resources > nul
del Brass.resources.dll > nul