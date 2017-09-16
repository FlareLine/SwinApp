# SwinApp Analytics Specification

_Revision 1 written by Alex Billson_

## Foreword

Whilst not necessarily implemented in its entirety on both a client and server
side, SwinApp comes with telemtry functionality for recording various analytics
in relation to how the user interacts with the application.

This document will exaplin the structure of the analytics data as well as how
an endpoint could be configured to receive this data if such functionality was desired in future.

## The Workflow of SwinApp's Analytics

All analytics functionality is in the suitably named  `Analytics.cs` file 
(located in the `Library` folder). 

The workflow of `Analytics` is very straightforward, the entire process working
around logging `AppEvent`'s. An `AppEvent` (located in the same folder as 
`Analytics`) is a relatively passive data structure which can be comparable to 
Android's `Intent` but rather than acting as a means of working with the device's
functionality, it aims to take note of actions that occur in the application.

This means that essentially `Analytics` acts as a singleton which can perform
tasks relating to 3 different scenarios:

![workflow of Analytics](https://raw.githubusercontent.com/FlareLine/SwinApp/dev-billson/Docs/Workflow.png)

Analytics is always available to use throughout the application, resulting in a 
robust and powerful tool for noting anything at any time when the application is in
use.

## The Technology Behind Analytics

`Analytics` is at heart a singleton wrapper for an SQLite connection. The "log" 
itself is just a locally saved SQLite file. 

## What makes an AppEvent

The structure of the `AppEvent` class consists of 3 major fields:

- Type
    - The type of event that is being logged
    - In the application side of things this is parsed from an `EventType` enum, however the final representation is that of a string
- Info
    - A string used to describe the event that has occurs
    - Like type, its final representation is just a string however it has no limitation on how it is inputted
- TimeStamp
    - The time this event occured
    - Is serialized as a string

The only pieces of functionality attached to `AppEvent` directly are the `Serialize`
and `Deserialize` methods.