@echo off

call MC7D2D StoragePouch.dll /reference:"%PATH_7D2D_MANAGED%\Assembly-CSharp.dll" ^
  Harmony\*.cs && ^
echo Successfully compiled StoragePouch.dll

pause