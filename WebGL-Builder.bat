set UNITY_EXECUTABLE="D:\Unity\Editor\2021.3.35f1\Editor\Unity.exe"
set PROJECT_PATH="D:\Unity\Projects\Drop-Puzzle"
set BUILD_PATH="D:\Unity\Projects\Builds"

%UNITY_EXECUTABLE% -batchmode -quit -projectPath %PROJECT_PATH% -executeMethod BuildPipeline.BuildPlayer -buildTarget WebGL

pause