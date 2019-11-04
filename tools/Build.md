## Build tesseract.exe

Install MSVC sdk

```
PS> git clone https://github.com/microsoft/vcpkg
PS> .\bootstrap-vcpkg.bat
PS> git apply tesseract.patch
PS> .\vcpkg install tesseract:x64-windows-static
```


## Update training datas

Call python script [tessdata.py](tessdata.py)

```
PS> python tessdata.py
```

```
usage: tessdata [-h] [-b] [-f] [--all] [--dir DIR]

Get training datas from tesseract

optional arguments:
  -h, --help  show this help message and exit
  -b, --best  For people willing to trade a lot of speed for slightly better
              accuracy.
  -f, --fast  These are a speed/accuracy compromise as to what offered the
              best value for money in speed vs accuracy.
  --all       Get all training datas
  --dir DIR   tessadata directory

```


## Build NuGet package

```
PS> nuget.exe pack -Prop Configuration=Release
```