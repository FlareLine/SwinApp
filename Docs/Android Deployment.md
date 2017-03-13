# Android Deployment Guide

*By Alex Billson*

SwinApp is being built with Android 7.1.1 however using AppCompat and other Google libraries
the application is able to support Android Revision 15.

---

This guide assumes you have:

- Installed Visual Studio 2017
    - With Mobile Development using .NET (this includes Xamarin.Forms)
- Cloned the Repo for SwinApp

1. Open Visual Studio and go to Tools > Xamarin > Android SDK Manager

![Xamarin Install](https://s30.postimg.org/5nesglmsx/swinappdeployment1.png)

2. Ensure you have at least some version of Android SDK Tools Installed

3. Ensure you have definitely downloaded the following packages: 
    - Android 7.1.1 SDK Platform
    - Android SDK Build Tools Revision 24.0.1

![Xamarin Install 2](https://s14.postimg.org/yun9htta9/swinappdeployment2.png)

4. You should be able to build and debug the application now
--- 

## General Troubleshooting

- Restart Visual Studio after messing with Android Studio stuff if it doesn't work
- Use Intel's HAXM VM coz it's gooood
- [Call Billson](tel:0433245406)