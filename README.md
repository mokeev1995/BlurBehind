# :white_large_square: BlurBehind Library For WPF Apps

BlurBehind - it's a simple library for WPF(not UWP) apps that needs to use blur behind the app window.

## :clipboard: Usage

If you want to enable BlurBehind you should use something like:
```csharp
BlurWindow.EnableWindowBlur(window);
```
Where `window` is your window object(e.g. this inside your codebehind window class). 

---

If you want to turn off BlurBehind you should use something like:
```csharp
BlurWindow.DisableWindowBlur(window);
```

`window` means the same thing.

## :green_book: Windows Support

As the title said, it can be used only in 
- Windows Vista
- Windows 7
- Windows 10

Other Windows versions doesn't support effect like this. If you try to enable blur with Windows 8 or 8.1, you'll get an Exception(NotSupportedExeption).

You'll need check 
```csharp
if (BlurWindow.CanBeEnabled)
    BlurWindow.EnableWindowBlur(window);
```
or just handle this exception to avoid crashing your app. 

## :green_book: License

[Licensed under the GPLv3 license.](https://github.com/mokeev1995/BlurBehind/blob/master/LICENSE)

Copyright :copyright: 2017 Mokeev Andrey

---

> [mokeev1995.ru](http://mokeev1995.ru) &nbsp;&middot;&nbsp;
> GitHub [@mokeev1995](https://github.com/mokeev1995) &nbsp;&middot;&nbsp;
> Twitter [@mokeev1995](https://twitter.com/mokeev1995) &nbsp;&middot;&nbsp;
> VK [@mokeev1995](https://vk.com/mokeev1995) 