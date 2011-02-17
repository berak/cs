if '' == '%1' goto notgood
	for %%i in (%1\Form1.* %1\*.cs %1\*.sln %1\*.csproj %1\Properties\*.cs %1\Properties\*.resx %1\Properties/*.setting) do git add %%i
:notgood
	echo startdir needed.
