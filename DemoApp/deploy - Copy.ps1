dotnet publish -c release --self-contained false -r win10-x64 -o ./release_artifact_fm_dep /p:PublishSingleFile=true

./release_artifact/demoapp.exe