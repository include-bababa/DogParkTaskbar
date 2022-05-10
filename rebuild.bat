setlocal
pushd %~dp0

"C:\Program Files\Microsoft Visual Studio\2022\Community\MSBuild\Current\Bin\MSBuild.exe" DogParkTaskbar.sln /p:Configuration=Release /t:Rebuild

robocopy DogParkTaskbar\bin\Release\net472\ dist\bin\ *.exe *.dll /MIR

pushd dist
tar -a -c -f DogParkTaskbar.zip bin
popd

popd
endlocal