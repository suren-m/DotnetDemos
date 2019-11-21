dotnet publish -c release --self-contained true -r win10-x64 -o ./release_artifact /p:PublishSingleFile=true

./release_artifact/demoapp.exe